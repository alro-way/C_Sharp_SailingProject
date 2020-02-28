using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C_Sharp_SailingTemplate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
// for password validation login
using Microsoft.AspNetCore.Identity;
//dropmenu with countries
using System.Globalization;


namespace C_Sharp_SailingTemplate.Controllers
{
    public class AdminController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public AdminController(MyContext context)
        {
            dbContext = context;
        }
        


// Login 
        [HttpGet("adminH49L0LWow239")]
        public IActionResult AdminIndex()
        {

             //Countries in dropmenu
            List<string> CountryList = new List<string>() ;
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    CountryList.Add(R.EnglishName);
                }
            }
            CountryList.Sort();
            Console.Write("----------------------------------------");
            System.Console.WriteLine(CountryList.Count());
            ViewBag.CountryList = CountryList;
            return View("AdminIndex");
        }

        [HttpPost("admin")]
        public IActionResult CreateUser(User newUser) 
        {
            UserWrapper Wrapper = new UserWrapper();
            Wrapper.NewUser = newUser;
            if (ModelState.IsValid) 
            {
                User oldUser = dbContext.Users
                    .FirstOrDefault(u => u.Email == newUser.Email);
                if (oldUser != null) 
                {
                    ModelState.AddModelError("NewUser.Email", "User with such email already exsits!");
                    return View("AdminIndex", Wrapper);
                }
                PasswordHasher < User > Hasher = new PasswordHasher < User > ();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                dbContext.Add(newUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                return RedirectToAction("Dashboard");
            }
            return View("AdminIndex");
        }

        public IActionResult LoginUser(Login newLogin) 
        {
            UserWrapper Wrapper = new UserWrapper();
            Wrapper.NewLogin = newLogin;
            if (ModelState.IsValid) 
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == newLogin.LoginEmail);
                if (userInDb == null) 
                {
                    ModelState.AddModelError("NewLogin.LoginEmail", "User with such email does not exist");
                    return View("AdminIndex", Wrapper);
                }
                var hasher = new PasswordHasher < Login > ();
                var result = hasher.VerifyHashedPassword(newLogin, userInDb.Password, newLogin.LoginPassword);
                if (result == 0) 
                {
                    ModelState.AddModelError("NewLogin.LoginPassword", "Invalid Password");
                    return View("AdminIndex", Wrapper);
                }
                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                int ? temp = HttpContext.Session.GetInt32("UserId");
                Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                Console.WriteLine(temp);
                ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
                Console.Write("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                return RedirectToAction("Dashboard");
            }
            return View("AdminIndex");
        }

        // Check Success or not success
        [HttpGet("success")]
        public IActionResult Success() 
        {
            var goSessions = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            Console.Write(goSessions);
            return View("Success");
        }

        [HttpGet("logout")]
        public IActionResult Logout() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("AdminIndex");
        }

//  DASHBOARD

        [HttpGet("dash")]
        public IActionResult Dashboard() 
        {
            int ? sessionUserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            if (sessionUserId == null)
                return RedirectToAction("Success");
            User thisUser = dbContext.Users
                    .FirstOrDefault(u=>u.UserId == sessionUserId);
            ViewBag.UserName = thisUser.FirstName;

            List<Route> AllRoutes = dbContext.Routes
                .OrderBy(r => r.ArrivalDate)
                .ToList();
            ViewBag.AllRoutes = AllRoutes;

           
            return View("Dashboard");
        }

// CREATE nEW ROUTE
        [HttpPost("create/route")]
        public IActionResult CreateRoute(Route newRoute) 
        {
            List<Route> AllRoutes = dbContext.Routes
                .OrderBy(r => r.ArrivalDate)
                .ToList();
            int ? SessionUserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            if (SessionUserId == null)
                return RedirectToAction("Success");

            if (ModelState.IsValid) 
            {
                dbContext.Routes.Add(newRoute);
                dbContext.SaveChanges();
                
                 ViewBag.AllRoutes = AllRoutes;
                return RedirectToAction("Success");
            }
            
            ViewBag.AllRoutes = AllRoutes;
            return View("Dashboard");
        }


        [HttpGet("delete/{RouteId}")]
        public IActionResult Delete(int RouteId) 
        {
            Console.Write("----------------1-------------------");
            int ? sessionUserId = HttpContext.Session.GetInt32("UserId");
            if (sessionUserId == null)
                return RedirectToAction("Success");
            Route routeToDelete = dbContext.Routes
                .SingleOrDefault(w => w.RouteId == RouteId);
            dbContext.Routes.Remove(routeToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }


       
    }
}
