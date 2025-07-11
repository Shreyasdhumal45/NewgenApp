using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewgenApp.DataContext;
using NewgenApp.Models;

namespace NewgenApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly NewgenWebDBContext db;

        public EmployeeController (NewgenWebDBContext dbContext)
      
        {
            this.db = dbContext;
        }

        public async Task<IActionResult>Index()
        {
            var EmployeeList = await db.Employees.Include(d => d.Department). ToListAsync();
            return View(EmployeeList);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await db.Departments.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {

            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            await db.Employees.AddAsync(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id > 0)
            {
             
                var employee = await db.Employees.FindAsync(id);
              
                    ViewBag.Departments = await db.Departments.ToListAsync();
                    return View(employee);
                
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {

            if (ModelState.IsValid)
            {
               
                db.Employees.Update(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            ViewBag.Departments = await db.Departments.ToListAsync();
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            if (id > 0)
            {
                var employee = await db.Employees.Include(d => d.Department).FirstOrDefaultAsync(d => d.EMPId == id);
                if (employee != null)
                {
                    return View(employee);
                }
                return NotFound();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            if (id > 0)
            {
                var employee = await db.Employees.FindAsync(id);
                if (employee != null)
                {
                    return View(employee);
                }
                return NotFound();
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteDept(int id)
        {
            if (id > 0)
            {
                var employee = await db.Employees.FirstOrDefaultAsync(d => d.EMPId == id);

                if (employee != null)
                {
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }

            return NotFound();
        }


    }
}
