using ControleFinanceiro.Application.Conta.Dto;
using ControleFinanceiro.Application.Conta.Validator;
using ControleFinanceiro.Application.Transacao.Dto;
using ControleFinanceiro.Domain.Conta.Agreggates;
using ControleFinanceiro.Repository.Repository;

namespace ControleFinanceiro.Application.Conta
{
    public class UsuarioService
    {
        private UsuarioRepository _usuarioRepository { get; set; }
        private ContaRepository _contaRepository { get; set; }
        private TransacaoRepository _transacaoRepository { get; set; }

        private UsuarioValidator _isValidUsuario = new UsuarioValidator();

        public UsuarioService(UsuarioRepository usuarioRepository, TransacaoRepository transacaoRepository, ContaRepository contaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _transacaoRepository = transacaoRepository;
            _contaRepository = contaRepository;
        }

        public UsuarioDto Obter(Guid id)
        {
            var usuarioObj = _usuarioRepository.ObterUsuarioById(id);
            var usuario = new UsuarioDto
            {
                Id = usuarioObj.Id,
                Nome = usuarioObj.Nome,
                Email = usuarioObj.Email,
                Telefone = usuarioObj.Telefone,
                FlAtivo = usuarioObj.FlAtivo,
                Conta = new ContaDto
                {
                    Id = usuarioObj.Conta.Id,
                    Saldo = usuarioObj.Conta.Saldo
                }
            };

            var listTransacoes = _transacaoRepository.ListarTransacoesPorConta(usuario.Conta.Id);
            var transacoes = listTransacoes.Select(t => new TransacaoDto
            {
                Id = t.Id,
                Valor = t.Valor,
                Tipo = t.Tipo,
                Descricao = t.Descricao,
                DataMovimentacao = t.DataMovimentacao
            }).ToList();
            usuario.Conta.Transacoes = transacoes;
            
            return usuario;
        }

        public List<UsuarioDto> Listar()
        {
            var list = _usuarioRepository.ObterUsuarios();
            var usuarios = list.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                Telefone = u.Telefone,
                FlAtivo = u.FlAtivo,
                Conta = new ContaDto
                {
                    Id = u.Conta.Id,
                    Saldo = u.Conta.Saldo
                }
            }).ToList();

            foreach (var item in usuarios)
            {
                var listTran = _transacaoRepository.ListarTransacoesPorConta(item.Conta.Id);
                item.Conta.Transacoes = listTran.Select(t => new TransacaoDto
                {
                    Id = t.Id,
                    Valor = t.Valor,
                    Tipo = t.Tipo,
                    Descricao = t.Descricao,
                    DataMovimentacao = t.DataMovimentacao
                }).ToList();
            }

            return usuarios;
        }

        public UsuarioDto Inserir(UsuarioRequestDto usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException("Usuário não pode ser nulo");
            var isValid = _isValidUsuario.ValidaUsuarioRequest(usuario);

            if (string.IsNullOrEmpty(isValid))
                throw new ArgumentNullException(isValid);

            var usuarioObj = new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                FlAtivo = usuario.FlAtivo
            };

            var usuarioInserido = _usuarioRepository.InserirUsuario(usuarioObj);

            var usuarioDto = new UsuarioDto
            {
                Id = usuarioInserido.Id,
                Nome = usuarioInserido.Nome,
                Email = usuarioInserido.Email,
                Telefone = usuarioInserido.Telefone,
                FlAtivo = usuarioInserido.FlAtivo
            };

            var conta = _contaRepository.InserirConta(usuarioDto.Id);
            
            usuarioDto.Conta = new ContaDto
            {
                Id = conta.Id,
                Saldo = conta.Saldo
            };

            return usuarioDto;
        }

        public bool Editar(UsuarioEditarRequestDto usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException("Usuário não pode ser nulo");

            var isValid = _isValidUsuario.ValidaUsuarioEditarRequest(usuario);

            if (string.IsNullOrEmpty(isValid))
                throw new ArgumentNullException(isValid);
            
            var usuarioObj = new Usuario
            {   
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                FlAtivo = usuario.FlAtivo
            };

            _usuarioRepository.EditarUsuario(usuarioObj);

           return true;
        }

        public bool Excluir(Guid Id)
        {
            _usuarioRepository.ExcluirUsuario(Id);

            return true;
        }
    }
}
