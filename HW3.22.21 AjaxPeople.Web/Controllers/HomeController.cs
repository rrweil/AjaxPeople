using HW3._22._21_AjaxPeople.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HW3._22._21_AjaxPeople.Data;

namespace HW3._22._21_AjaxPeople.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Person person)
        {
            var db = new PersonDb(@"Data Source=.\sqlexpress;Initial Catalog=PracticingJoin;Integrated Security=true;");
            db.Add(person);
            return Json(person);
        }

        public IActionResult GetAll()
        {
            var db = new PersonDb(@"Data Source=.\sqlexpress;Initial Catalog=PracticingJoin;Integrated Security=true;");
            List<Person> ppl = db.GetAll();
            return Json(ppl);
        }

        public IActionResult GetPersonById(int id)
        {
            var db = new PersonDb(@"Data Source=.\sqlexpress;Initial Catalog=PracticingJoin;Integrated Security=true;");
            var person = db.GetPersonById(id);
            return Json(person);
        }

        [HttpPost]
        public IActionResult UpdatePerson(Person p)
        {
            var db = new PersonDb(@"Data Source=.\sqlexpress;Initial Catalog=PracticingJoin;Integrated Security=true;");
            db.UpdatePerson(p);
            return Json(p);
        }

        [HttpPost]
        public IActionResult DeletePerson(int id)
        {
            var db = new PersonDb(@"Data Source=.\sqlexpress;Initial Catalog=PracticingJoin;Integrated Security=true;");
            db.DeletePerson(id);
            return Json(id);
        }
    }
}
