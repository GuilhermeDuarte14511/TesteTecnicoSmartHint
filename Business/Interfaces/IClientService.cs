using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteSmartHint.Models;

namespace Business.Interfaces
{
    public interface IClientesService
    {
        bool AdicionarCliente(Clientes cliente, ModelStateDictionary ModelState);
        Clientes ObterClientePorId(int id);
        List<Clientes> ObterTodosClientes();
        Task<bool> EditarClientes(Clientes clientes);
        List<Clientes> FiltroClientes(string filtroNome, string filtroEmail, string filtroTelefone, DateTime? filtroDataCadastro, bool? filtroBloqueado);
        // Adicione aqui os outros métodos públicos de ClientesService que você precisa
    }
}
