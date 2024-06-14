using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Usuarios;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Usuarios;
using UrnaEletronica.Servico.Dtos.Usuarios;
using UrnaEletronica.Servico.Servicos.Contratos.Usuarios;

namespace UrnaEletronica.Servico.Servicos.Implementacoes.Usuarios
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUsuarioPersistencia _usuarioPersistencia;

        public UsuarioServico(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IMapper mapper, IUsuarioPersistencia usuarioPersistencia)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _usuarioPersistencia = usuarioPersistencia;
        }
        public async Task<SignInResult> CompararSenhaUsuarioAsync(UsuarioUpdateDto usuarioUpdateDto, string password)
        {
            try
            {
                var usuario = await _userManager.Users.SingleOrDefaultAsync(usuario => usuario.UserName.ToLower() == usuarioUpdateDto.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(usuario, password, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao validar conta e senha. Erro: {ex.Message}");
            }
        }

        public async Task<UsuarioUpdateDto> CreateUsuario(UsuarioDto usuarioDto)
        {
            try
            {
                if (usuarioDto.UserName.Contains("Admin"))
                {
                    usuarioDto.IsAdmin = true;
                }

                var usuario = _mapper.Map<Usuario>(usuarioDto);
                var usuarioCriado = await _userManager.CreateAsync(usuario, usuarioDto.Password);

                if (usuarioCriado.Succeeded)
                {
                    return _mapper.Map<UsuarioUpdateDto>(usuario);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao criar a conta. Erro: {ex.Message}");
            }
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync()
        {
            try
            {
                var usuarios = await _usuarioPersistencia.GetAllUsuariosAsync();

                if (usuarios == null) return null;

                return _mapper.Map<UsuarioDto[]>(usuarios);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao buscar a todas as contas. Erro: {ex.Message}");
            }
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllUsuariosByNomeAsync(string nome)
        {
            try
            {
                var usuarios = await _usuarioPersistencia.GetAllUsuariosByNomeAsync(nome);

                if (usuarios == null) return null;

                return _mapper.Map<UsuarioDto[]>(usuarios);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao buscar a conta por nome. Erro: {ex.Message}");
            }
        }

        public async Task<UsuarioDto> GetUsuarioByIdAsync(int usuarioId)
        {
            try
            {
                var usuario = await _usuarioPersistencia.GetUsuarioByIdAsync(usuarioId);

                if (usuario == null) return null;

                return _mapper.Map<UsuarioDto>(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao buscar a conta por ID. Erro: {ex.Message}");
            }
        }

        public async Task<UsuarioUpdateDto> GetUsuarioByUserNameAsync(string userName)
        {
            try
            {
                var usuario = await _usuarioPersistencia.GetUsuarioByUserNameAsync(userName);

                if (usuario == null) return null;

                return _mapper.Map<UsuarioUpdateDto>(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao buscar a conta por nome de Usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UsuarioUpdateDto> UpdateUsuario(UsuarioUpdateDto usuarioUpdateDto)
        {
            try
            {
                var usuario = await _usuarioPersistencia.GetUsuarioByUserNameAsync(usuarioUpdateDto.UserName);
                if (usuario == null) return null;

                _mapper.Map(usuarioUpdateDto, usuario);

                if(usuarioUpdateDto.Password != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    await _userManager.ResetPasswordAsync(usuario, token, usuarioUpdateDto.Password);
                }

                _usuarioPersistencia.Update<Usuario>(usuario);

                if (await _usuarioPersistencia.SaveChangeAsync())
                {
                    var usuarioRetorno = await _usuarioPersistencia.GetUsuarioByUserNameAsync(usuario.UserName);
                    return _mapper.Map<UsuarioUpdateDto>(usuarioRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao alterar contas e token. Erro: {ex.Message}");
            }
        }

        public async Task<bool> VerificarUsuarioExisteAsync(string userName)
        {
            try
            {
                return await _userManager.Users.AnyAsync(user => user.UserName.ToLower() == userName.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao verificar se a conta existe. Erro: {ex.Message}");
            }
        }
    }
}
