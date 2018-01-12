﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballDeployment.Data;
using FootballDeployment.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootballDeployment.Controllers
{
    public class TeamController : Controller
    {
        private FootballDbContext _context;

        public TeamController(FootballDbContext context)
        {
            _context = context;
        }

        public IActionResult TeamInfo(string teamName)
        {
            // get all players of the passed team from database
            List<Player> players = _context.Players.ToList();

            return View(players);
        }

        [HttpGet]
        public IActionResult AddPlayer()
        {
            // return view of add player
            return View();
        }

        [HttpPost]
        public IActionResult AddPlayer(Player player)
        {
            // add player to database
            _context.Players.Add(player);
            // save player
            _context.SaveChanges();

            return RedirectToAction("TeamInfo");
        }

        [HttpGet]
        public IActionResult UpdatePlayer(int id)
        {
            // return view of update player with id
            return View(_context.Players.FirstOrDefault(p => p.Id == id));
        }

        [HttpPost]
        public IActionResult UpdatePlayer(Player p)
        {
            // update player
            _context.Players.Update(p);
            _context.SaveChanges();

            return RedirectToAction("TeamInfo", new {p.Team});
        }

        [HttpGet]
        public IActionResult DeletePlayer(int id)
        {
            // return view of specified player to delete
            return View(_context.Players.First(p => p.Id == id));
        }

        [HttpPost]
        public IActionResult DeletePlayer(Player p)
        {
            // remove player from the database
            _context.Remove(p);
            _context.SaveChanges();

            return RedirectToAction("TeamInfo", new { p.Team });
        }
    }
}
