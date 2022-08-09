using Prometheus;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Aplicacao.Models;

namespace Aplicacao.Controllers;

public class HomeController : Controller
{
    private static readonly Counter ContHome = Metrics.CreateCounter("home_requisicoes_total", "Total de requisições Home");
    private static readonly Counter ContPrivace = Metrics.CreateCounter("privacy_requisicoes_total", "Total de requisições Home");

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ContHome.Inc();
        return View();
    }

    public IActionResult Counter()
    {
        ContHome.Inc();
        return View();
    }    

    public IActionResult Privacy()
    {
        ContPrivace.Inc();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
