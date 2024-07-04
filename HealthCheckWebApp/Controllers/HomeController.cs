using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HealthCheckWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace HealthCheckWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;

    public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> HealthCheckData()
    {
        var response = await _httpClient.GetFromJsonAsync<HealthCheckData[]>("healthchecks");
        return View(response);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
