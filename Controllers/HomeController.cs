﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using gsu_math.Models;

namespace gsu_math.Controllers;

public class HomeController : Controller
{
    private readonly MyDbContext _context;
    public HomeController( MyDbContext context)
    {
        _context = context;    
    }

    public IActionResult Index()
    {

        return View(_context.Blog.Where(s => s.is_active == true).OrderByDescending(p=>p.AtCreated).Take(3).ToList());
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
