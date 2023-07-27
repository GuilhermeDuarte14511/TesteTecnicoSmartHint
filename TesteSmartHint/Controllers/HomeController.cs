using Business;
using Business.Interfaces;
using Entities.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TesteSmartHint.Models;

namespace TesteSmartHint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITesteSmartHintContext _context;
        private readonly IClientesService _clientesService;

        public HomeController(ILogger<HomeController> logger, ITesteSmartHintContext context, IClientesService clientesService)
        {
            _logger = logger;
            _context = context;
            _clientesService = clientesService;
        }


        public IActionResult Index(string filtroNome, string filtroEmail, string filtroTelefone, DateTime? filtroDataCadastro, bool? filtroBloqueado, bool isFiltro)
        {
            try
            {
                if (isFiltro)
                {
                    var clientesFiltro = _clientesService.FiltroClientes(filtroNome, filtroEmail, filtroTelefone, filtroDataCadastro, filtroBloqueado);
                    return View(clientesFiltro);
                }
                else
                {
                    var clientes = _context.Clientes.Take(20).ToList();
                    return View(clientes);
                }
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public IActionResult CadastrarClientes()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarClientes(Clientes clientes)
        {
            try
            {
                bool cadastro = _clientesService.AdicionarCliente(clientes, ModelState);
                if (cadastro)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Adicionar uma mensagem de erro à ViewData ou TempData
                    ModelState.AddModelError("", "Ocorreu um erro ao tentar cadastrar o cliente. Por favor, tente novamente.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar cliente");
                ModelState.AddModelError("", "Ocorreu um erro ao tentar cadastrar o cliente. Por favor, tente novamente.");
            }

            // Retornar a view "CadastrarClientes" com o modelo clientes sempre que não houver redirecionamento.
            return View("CadastrarClientes", clientes);
        }



        public IActionResult EditarCadastro(int id)
        {
            var cliente = _clientesService.ObterClientePorId(id);
            if (cliente == null)
            {
                return RedirectToAction("Index"); // Caso o cliente não seja encontrado, redireciona à página de listagem
            }

            return View("EditarCadastro", cliente);

        }

        [HttpPost]
        public async Task<IActionResult> SalvarCliente(Clientes cliente)
        {
            try
            {
                // Atualizar os dados do cliente no banco de dados
                bool UpdateCliente = await _clientesService.EditarClientes(cliente);

                // Redirecionar para a página de listagem de clientes após a atualização
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar as alterações do cliente");
                ModelState.AddModelError("", "Ocorreu um erro ao salvar as alterações do cliente. Por favor, tente novamente.");
            }

            // Se houver algum erro de validação, retornar a mesma página com os erros
            return View("EditarCadastro", cliente);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
