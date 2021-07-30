using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using Test2Dev.Enums;
using Test2Dev.Models;

namespace Test2Dev.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Employees_Load(DatatablesServerModel table)
        {
            bool sortColumnDir = table.Order.FirstOrDefault().Dir == "asc" ? true : false;
            int recordsTotal = 0;
            var data = new List<Employee>();
            using (var db = new EmployeeDBContext())
            {
                var li = db.Employees.Select(x => x);

                var Columna = Convert.ToInt32(table.Order.FirstOrDefault().Column);
                var arra = table.Columns.ToArray();
                if (sortColumnDir) li = li.OrderBy(arra[Columna].Name);
                else li = li.OrderBy(arra[Columna].Name + " DESC");

                recordsTotal = li.Count();
                data = li.Skip(table.Start).Take(table.Length).ToList();
            }
            return Json(new { draw = table.Draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
        [HttpPost]
        public JsonResult Add_Employee(Employee model)
        {
            if (ModelState.IsValid)
            {
                var ok = 0;
                using (var db = new EmployeeDBContext())
                {
                    if (db.Employees.Where(x => x.RFC == model.RFC).Any())
                    {
                        return Json(new { error = true, msg = "RFC Duplicated" });
                    }
                    db.Employees.Add(model);
                    try
                    {
                        ok = db.SaveChanges();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                return Json(new { error = !(ok > 0), msg="" });
            }
            else
            {
                return Json(new { error = true, msg="" });
            }
            
        }
        [HttpPost]
        public JsonResult Details_Employee(int id)
        {
            var employee = new Employee();
            using (var db = new EmployeeDBContext())
            {
                employee = db.Employees.Find(id);
            }
            return Json(employee);

        }
        [HttpPost]
        public JsonResult Eddit_Employee(Employee model)
        {
            var ok = 0;
            using (var db = new EmployeeDBContext())
            {

                var aux = db.Employees.Find(model.ID);
                if (aux.RFC != model.RFC && db.Employees.Where(x => x.RFC == model.RFC).Any())
                {
                    return Json(new { error = true, msg = "RFC Duplicated" });
                }
                aux.Name = model.Name;
                aux.LastName = model.LastName;
                aux.RFC = model.RFC;
                aux.BornDate = model.BornDate;
                aux.Status = model.Status;

                try
                {
                    ok = db.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return Json(new { error = !(ok > 0), msg ="" });
        }
        [HttpPost]
        public JsonResult Delete_Employee(int id)
        {
            var ok = 0;
            using (var db = new EmployeeDBContext())
            {
                var aux = db.Employees.Find(id);
                db.Employees.Remove(aux);
                try
                {
                    ok = db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Json(ok > 0);
        }
    }
}