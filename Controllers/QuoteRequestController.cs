using System;
using Microsoft.AspNetCore.Mvc;
using L_Connect.Models.ViewModels.Quote;

namespace L_Connect.Controllers
{
    public class QuoteRequestController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            var model = new QuoteRequestViewModel();
            return View(model);
        }

       [HttpPost]
public IActionResult Create(QuoteRequestViewModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    decimal basePricePerKg = 10;
    decimal distance = GetDistance(model.Source, model.Destination);
    decimal estimatedPrice = model.Weight * basePricePerKg * distance;

    decimal? discount = null;
    if (model.Weight >= 10)
    {
        discount = 10;
        estimatedPrice *= 0.9M; // Apply 10% discount
    }

    var quoteResponse = new QuoteResponseViewModel
    {
        CustomerName = model.CustomerName,
        Email = model.Email,
        Source = model.Source,
        Destination = model.Destination,
        Weight = Math.Round(model.Weight, 2), // Round to 2 decimal places
        BasePricePerKg = Math.Round(basePricePerKg, 2),
        Distance = Math.Round(distance, 2),
        EstimatedPrice = Math.Round(estimatedPrice, 2),
        Discount = discount
    };

    return View("QuoteResponse", quoteResponse);
}

        private decimal GetDistance(string source, string destination)
        {
            if (source == "USA" && destination == "Canada") return 500;
            if (source == "USA" && destination == "Brazil") return 7500;
            if (source == "China" && destination == "Canada") return 12000;
            if (source == "Brazil" && destination == "China") return 18000;
            return 1000; // Default value
        }
    }
}
