using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LoanManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        
        DropDownList dropDownListObj = new DropDownList();
        // GET: Home
        public ActionResult Index( )
        {
            ViewBag.Message = "My First MVC";

           //DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();

            return View();


        }
        public ActionResult Resiter()
        {
            ViewBag.Message = "My First MVC";

            //DataTable dtFundNameDropDownList = dropDownListObj.FundNameDropDownList();

            return View();
   
        }
    }
}