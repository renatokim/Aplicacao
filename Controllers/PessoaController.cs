using Prometheus;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Aplicacao.Models;

namespace Aplicacao.Controllers;

public class PessoaController : Controller
{
    private static readonly Counter contPessoas = Metrics.CreateCounter("http_requests_total", "Total de requisições");
    private static readonly Gauge JobsInQueue = Metrics.CreateGauge("myapp_jobs_queued", "Number of jobs waiting for processing in the queue.");
    private static readonly Summary RequestSizeSummary = Metrics.CreateSummary("myapp_request_size_bytes", "Summary of request sizes (in bytes) over last 10 minutes.", new SummaryConfiguration
        {
            Objectives = new[]
            {
                new QuantileEpsilonPair(0.5, 0.01),
                new QuantileEpsilonPair(0.8, 0.05),
                new QuantileEpsilonPair(0.90, 0.8),
                new QuantileEpsilonPair(0.99, 0.9),
            }
        });

    public IActionResult Index()
    {
        contPessoas.Inc();
        return View();
    }

    public IActionResult GaugeIncrement()
    {
        Random randNum = new Random();
        var random = randNum.Next(10);

        ViewData["Random"] = random;

        JobsInQueue.Inc(random);
       return View();
    }

    public IActionResult GaugeDecrement()
    {
        Random randNum = new Random();
        var random = randNum.Next(10);

        ViewData["Random"] = random;

        JobsInQueue.Dec(random);
       return View();
    }    

    public IActionResult Summary()
    {
        Random randNum = new Random();
        var random = randNum.Next(10);

        ViewData["Random"] = random;

        RequestSizeSummary.Observe(random);
        
       return View();
    }     
}
