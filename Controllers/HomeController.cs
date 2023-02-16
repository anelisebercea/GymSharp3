﻿using GymSharp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using GymSharp.Data;
using GymSharp.Models.GymViewModels;


namespace GymSharp.Controllers
{
    public class HomeController : Controller
    {
        //private readonly GymContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(/*GymContext context,*/ ILogger<HomeController> logger)
        {
            //_context = context;
            _logger = logger;
        }

        public IActionResult Index()
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
}