using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HangfireDemo.Models;
using Hangfire;
using HangfireDemo.Services;
using Microsoft.Extensions.Logging;

namespace HangfireDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly MyBackgroundJob _myBackgroundJob;

    public HomeController(ILogger<HomeController> logger,IBackgroundJobClient backgroundJobClient, MyBackgroundJob myBackgroundJob)
    {
        _backgroundJobClient = backgroundJobClient;
        _myBackgroundJob = myBackgroundJob;
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Schedule a background job
        _backgroundJobClient.Enqueue(() => _myBackgroundJob.Execute());

        return View();
    }
}
