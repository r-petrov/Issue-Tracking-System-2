using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTrackingSystem2.Services.Data.Label;
using IssueTrackingSystem2.Services.Mapping;
using IssueTrackingSystem2.Web.ViewModels.Label;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackingSystem2.Web.Controllers
{
    public class LabelController : Controller
    {
        private readonly ILabelService labelService;

        public LabelController(ILabelService labelService)
        {
            this.labelService = labelService;
        }

        // GET: Label
        public ActionResult List()
        {
            var labelListServiceModels = this.labelService.All();
            var labelListViewModels = labelListServiceModels.To<LabelListViewModel>();

            return this.View(labelListViewModels);
        }

        // GET: Label/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Label/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Label/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: Label/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Label/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: Label/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Label/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }
    }
}