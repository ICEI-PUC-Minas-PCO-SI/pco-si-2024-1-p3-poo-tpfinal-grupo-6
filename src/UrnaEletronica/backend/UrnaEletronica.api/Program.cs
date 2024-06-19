using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using UrnaEletronica.Dominio.Modelos.Usuarios;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Candidatos;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Cidades;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Usuarios;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Cidades;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Usuarios;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Candidatos;
using UrnaEletronica.Servico.Servicos.Contratos.Candidatos;
using UrnaEletronica.Servico.Servicos.Contratos.Cidades;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;
using UrnaEletronica.Servico.Servicos.Implementacoes.Candidatos;
using UrnaEletronica.Servico.Servicos.Implementacoes.Cidades;
using UrnaEletronica.Servico.Servicos.Implementacoes.Usuarios;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Log;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Log;
using UrnaEletronica.Servico.Servicos.Contratos.Log;
using UrnaEletronica.Servico.Servicos.Implementacoes.Log;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//Injeção de Dependencia do contexto
builder.Services
    .AddDbContext<UrnaEletronicaContexto>(
        context =>
        {
            var configuration = builder.Configuration.GetConnectionString("DefaultConnection");
            context.UseMySql(configuration, ServerVersion.AutoDetect(configuration));
            context.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    );

//Injeção de dependencia do IdentityFrameWork
builder.Services
    .AddIdentityCore<Usuario>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 4;
    })
    .AddRoles<Funcao>()
    .AddRoleManager<RoleManager<Funcao>>()
    .AddSignInManager<SignInManager<Usuario>>()
    .AddRoleValidator<RoleValidator<Funcao>>()
    .AddEntityFrameworkStores<UrnaEletronicaContexto>()
    .AddDefaultTokenProviders();

//Injeção de Autenticação
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

//Injeção de Controllers
builder.Services
    .AddControllers()
    // Já leva os enum convertidos na query
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    })
    // Eliminar loop infinito da estrutura
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


//Injeção do Mapeamento automático de campos (Dto)
builder.Services
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//Injeção das Persistências
builder.Services
    .AddScoped<IUsuarioPersistencia, UsuarioPersistencia>()
    .AddScoped<ICidadePersistencia, CidadePersistencia>()
    .AddScoped<ISharedPersistencia, SharedPersistencia>()
    .AddScoped<ICandidatoPersistencia, CandidatoPersistencia>()
    .AddScoped<ILogVotosBatchPersistencia, LogVotosBatchPersistencia>()
    .AddScoped<ILogVotosErrosPersistencia, LogVotosErrosPersistencia>();

//Injeção dos Serviços
builder.Services
    .AddScoped<IUsuarioServico, UsuarioServico>()
    .AddScoped<ICidadeServico, CidadeServico>()
    .AddScoped<ITokenServico, TokenServico>()
    .AddScoped<ICandidatoServico, CandidatoServico>()
    .AddScoped<IProcessarVotosBatchServico, ProcessarVotosBatchServico>();

//Injeçao do Cors(Segurança)
builder.Services
    .AddCors();


builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "UrnaEletronica.API", Version = "v1", Description = "API responsável por implementar as funcionalidades de backend da Urna Eletrônica " });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando Beares. Entre com 'Bearer [espa o] em seguida coloque seu token.
                                    Exemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
} );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UrnaEletronica.API v1"));

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(cors => cors
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.MapControllers();

app.Run();
