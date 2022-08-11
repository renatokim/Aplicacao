using Prometheus;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Aplicacao.Models;

namespace Aplicacao.Controllers;

public class PessoaController : Controller
{
    private static readonly Counter counter = Metrics.CreateCounter("http_requests_total", "Total de requisições");
    private static readonly Gauge gauge = Metrics.CreateGauge("myapp_jobs_queued", "Number of jobs waiting for processing in the queue.");
    private static readonly Histogram histogram = Metrics
        .CreateHistogram("myapp_order_value_usd", "Histogram of received order values (in USD).",
        new HistogramConfiguration
        {
            Buckets = Histogram.LinearBuckets(start: 10, width: 10, count: 10)
        });
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

    public IActionResult HistogramView()
    {
        Random randNum = new Random();
        var random = randNum.Next(100);

        ViewData["Random"] = random;

        histogram.Observe(random);
        return View();
    }

    public IActionResult Index()
    {
        counter.Inc();
        return View();
    }

    public IActionResult GaugeIncrement()
    {
        Random randNum = new Random();
        var random = randNum.Next(10);

        ViewData["Random"] = random;

        gauge.Inc(random);
       return View();
    }

    public IActionResult GaugeDecrement()
    {
        Random randNum = new Random();
        var random = randNum.Next(10);

        ViewData["Random"] = random;

        gauge.Dec(random);
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
