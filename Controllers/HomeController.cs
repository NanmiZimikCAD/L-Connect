using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using L_Connect.Models;
// HomeController manages public-facing pages including landing page and general information
namespace L_Connect.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    //FAQ page




    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    //faq page
    public IActionResult FAQ()
    {
        return View();
    }

    //support page
    public IActionResult Support()
    {
        return View();
    }
     
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
