﻿using Stampit.Webapp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Stampit.Webapp.Controllers
{
    public class KioskModeController : Controller
    {
        private const string SESSION_STATE = "SessionState";

        public ActionResult Index()
        {
            var sessionState = Session[SESSION_STATE] as SessionState;
            if (sessionState == null)
            {
                sessionState = new SessionState
                {
                    Model = new List<ProductViewModel>
                    {
                        new ProductViewModel { Name = "Pizza", Count = 0 },
                        new ProductViewModel { Name = "Kebab", Count = 0 }
                    },
                    SelectedViewModel = null
                };
                Session[SESSION_STATE] = sessionState;
            }

            return View(sessionState.Model);
        }

        public ActionResult SetCount(int count)
        {
            var sessionState = Session[SESSION_STATE] as SessionState;
            if (sessionState == null || sessionState.SelectedViewModel == null) return RedirectToAction("Index");

            sessionState.SelectedViewModel.Count = count;
            Session[SESSION_STATE] = sessionState;

            return View("Index", sessionState.Model);
        }

        public ActionResult SelectProductViewModel(string selectedProduct)
        {
            var sessionState = Session[SESSION_STATE] as SessionState;
            if (sessionState == null || sessionState.Model == null) return RedirectToAction("Index");

            sessionState.SelectedViewModel = sessionState.Model.Where(vm => vm.Name == selectedProduct).FirstOrDefault();
            Session[SESSION_STATE] = sessionState;
            return View("Index", sessionState.Model);
        }
    }

    public class SessionState
    {
        public IEnumerable<ProductViewModel> Model { get; set; }
        public ProductViewModel SelectedViewModel { get; set; }
    }
}