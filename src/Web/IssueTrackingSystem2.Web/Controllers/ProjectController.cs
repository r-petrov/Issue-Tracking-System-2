using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackingSystem2.Web.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project/Details/5
        [HttpGet]
        public ActionResult Details(string id)
        {
            return View();
        }
    }
}