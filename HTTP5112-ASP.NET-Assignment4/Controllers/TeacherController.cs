using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using HTTP5112Assignment4.Models;
using System.Diagnostics;

namespace HTTP5112Assignment4.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeacher(SearchKey);
            ViewBag.SearchKey = SearchKey;
            return View(Teachers);
        }

        //GET : /Tecaher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            IEnumerable<Class> Classes = controller.ListClasses(id);
            ViewBag.classes = Classes;

            return View(NewTeacher);
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();
        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, string HireDate, string Salary)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form
             
            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            DateTime HireDateDT;
            if (!DateTime.TryParse(HireDate, out HireDateDT))
            {
                NewTeacher.HireDate = null;
            }
            if (!float.TryParse(Salary, out NewTeacher.Salary))
            {
                NewTeacher.Salary = float.NaN;
            }

            if (NewTeacher.TeacherFname == null || NewTeacher.TeacherLname == null || NewTeacher.EmployeeNumber == null || NewTeacher.HireDate == null || Salary == null)
            {
                return View(NewTeacher);
            }
            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
    }
}