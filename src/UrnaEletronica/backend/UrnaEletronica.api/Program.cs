using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using BibCorpPrevenir2.api.Util.Services.Interfaces.Contracts.Uploads;
using BibCorpPrevenir2.api.Util.Services.Interfaces.Implementations.Uploads;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UrnaEletronica.Dominio.Modelos.Eleicoes.Executivos;
using UrnaEletronica.Dominio.Modelos.Eleicoes.Legislativos;
using UrnaEletronica.Dominio.Modelos.Usuarios;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Candidatos;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Cidades;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Coligacoes;
using UrnaEletronica.Persistencia.Interfaces.Contratos.LogsVotosBatchs;
using UrnaEletronica.Persistencia.Interfaces.Contratos.ParametrosEleicoes;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Partidos;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Resultados;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Usuarios;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Candidatos;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Cidades;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Coligacoes;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.ConfigEleicoes;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.LogsVotosBatchs;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Partidos;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Resultados;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Usuarios;
using UrnaEletronica.Servico.Servicos.Contratos.Candidatos;
using UrnaEletronica.Servico.Servicos.Contratos.Cidades;
using UrnaEletronica.Servico.Servicos.Contratos.Coligacoes;
using UrnaEletronica.Servico.Servicos.Contratos.Eleicoes;
using UrnaEletronica.Servico.Servicos.Contratos.Log;
using UrnaEletronica.Servico.Servicos.Contratos.ParametrosEleicoes;
using UrnaEletronica.Servico.Servicos.Contratos.Partidos;
using UrnaEletronica.Servico.Servicos.Contratos.Resultados;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;
using UrnaEletronica.Servico.Servicos.Implementacoes.Candidatos;
using UrnaEletronica.Servico.Servicos.Implementacoes.Cidades;
using UrnaEletronica.Servico.Servicos.Implementacoes.Coligacoes;
using UrnaEletronica.Servico.Servicos.Implementacoes.Eleicoes;
using UrnaEletronica.Servico.Servicos.Implementacoes.LogsVotosBatchs;
using UrnaEletronica.Servico.Servicos.Implementacoes.ParametrosEleicoes;
using UrnaEletronica.Servico.Servicos.Implementacoes.Partidos;
using UrnaEletronica.Servico.Servicos.Implementacoes.Resultados;
using UrnaEletronica.Servico.Servicos.Implementacoes.Usuarios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//Inje��o de Dependencia do contexto
builder.Services
    .AddDbContext<UrnaEletronicaContexto>(
        context =>
        {
            var configuration = builder.Configuration.GetConnectionString("DefaultConnection");
            context.UseMySql(configuration, ServerVersion.AutoDetect(configuration));
            context.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    );

//Inje��o de dependencia do IdentityFrameWork
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

//Inje��o de Autentica��o
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

//Inje��o de Controllers
builder.Services
    .AddControllers()
    // J� leva os enum convertidos na query
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    })
    // Eliminar loop infinito da estrutura
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


//Inje��o do Mapeamento autom�tico de campos (Dto)
builder.Services
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registra as classes derivadas
builder.Services.AddSingleton<EleicaoExecutivo>();
builder.Services.AddSingleton<EleicaoLegislativo>();

//Inje��o das Persist�ncias
builder.Services
    .AddScoped<ICandidatoPersistencia, CandidatoPersistencia>()
    .AddScoped<ICidadePersistencia, CidadePersistencia>()
    .AddScoped<IColigacaoPersistencia, ColigacaoPersistencia>()
    .AddScoped<ILogVotosBatchPersistencia, LogVotosBatchPersistencia>()
    .AddScoped<ILogVotosErrosPersistencia, LogVotosErrosPersistencia>()
    .AddScoped<IParametroEleicaoPersistencia, ParametroEleicoePersistencia>()
    .AddScoped<IPartidoPersistencia, PartidoPersistencia>()
    .AddScoped<IResultadoPersistencia, ResultadoPersistencia>()
    .AddScoped<ISharedPersistencia, SharedPersistencia>()
    .AddScoped<IUsuarioPersistencia, UsuarioPersistencia>()
    ;

//Inje��o dos Servi�os
builder.Services
    .AddScoped<ICandidatoServico, CandidatoServico>()
    .AddScoped<ICidadeServico, CidadeServico>()
    .AddScoped<IColigacaoServico, ColigacaoServico>()
    .AddScoped<IEleicaoExecutivaServico, EleicaoExecutivaServico>()
    .AddScoped<IParametroEleicaoServico, ParametroEleicaoServico>()
    .AddScoped<IPartidoServico, PartidoServico>()
    .AddScoped<IProcessarVotosBatchServico, ProcessarVotosBatchServico>()
    .AddScoped<IResultadoServico, ResultadoServico>()
    .AddScoped<ITokenServico, TokenServico>()
    .AddScoped<IUploadService, UploadService>()
    .AddScoped<IUsuarioServico, UsuarioServico>()
    ;

//Inje�ao do Cors(Seguran�a)
builder.Services
    .AddCors();


builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "UrnaEletronica.API", Version = "v1", Description = "API respons�vel por implementar as funcionalidades de backend da Urna Eletr�nica " });

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

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
    RequestPath = new PathString("/Resources")
});

app.MapControllers();

app.Run();
