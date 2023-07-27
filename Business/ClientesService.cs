using BCrypt.Net;
using Business.Interfaces;
using Entities.Db;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteSmartHint.Models;

namespace Business
{
    public class ClientesService : IClientesService
    {
        private readonly TesteSmartHintContext _context;
        private readonly BaseBLL _baseBLL;


        public ClientesService(TesteSmartHintContext context)
        {
            _context = context;
            _baseBLL = new BaseBLL();
        }

        public bool AdicionarCliente(Clientes cliente, ModelStateDictionary ModelState)
        {
            try
            {
                // Verifique se o e-mail já existe
                if (_context.Clientes.Any(c => c.Email == cliente.Email))
                {
                    ModelState.AddModelError("", "Este e-mail já está cadastrado para outro Cliente");
                    return false;
                }

                // Verifique se o CPF/CNPJ já existe
                if (_context.Clientes.Any(c => c.Documento == cliente.Documento))
                {
                    ModelState.AddModelError("", "Este CPF/CNPJ já está cadastrado para outro Cliente");
                    return false;
                }

                // Verifique se a Inscrição Estadual já existe
                if (_context.Clientes.Any(c => c.InscricaoEstadual == cliente.InscricaoEstadual))
                {
                    ModelState.AddModelError("", "Esta Inscrição Estadual já está cadastrada para outro Cliente");
                    return false;
                }

                var salt = _baseBLL.GerarSalt();
                cliente.Senha = _baseBLL.CriptografarSenha(cliente.Senha, salt);
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return false;
            }
        }


        public Clientes ObterClientePorId(int id)
        {
            return _context.Clientes.FirstOrDefault(c => c.Id == id);
        }

        public List<Clientes> ObterTodosClientes()
        {
            return _context.Clientes.ToList();
        }

        public async Task<bool> EditarClientes(Clientes clientes)
        {
            try
            {
                _context.Clientes.Update(clientes);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Clientes> FiltroClientes(string filtroNome, string filtroEmail, string filtroTelefone, DateTime? filtroDataCadastro, bool? filtroBloqueado)
        {
            // Aplicar filtros
            var clientesQuery = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(filtroNome))
            {
                clientesQuery = clientesQuery.Where(c => c.Nome.Contains(filtroNome));
            }

            if (!string.IsNullOrEmpty(filtroEmail))
            {
                clientesQuery = clientesQuery.Where(c => c.Email.Contains(filtroEmail));
            }

            if (!string.IsNullOrEmpty(filtroTelefone))
            {
                clientesQuery = clientesQuery.Where(c => c.Telefone.Contains(filtroTelefone));
            }

            if (filtroDataCadastro.HasValue)
            {
                clientesQuery = clientesQuery.Where(c => c.DataCadastro.Date == filtroDataCadastro.Value.Date);
            }

            if (filtroBloqueado.HasValue)
            {
                clientesQuery = clientesQuery.Where(c => c.Bloqueado == filtroBloqueado.Value);
            }

            return clientesQuery.ToList();
        }
        // Outros métodos para manipular clientes (excluir, atualizar, recuperar, etc.)
    }
}
