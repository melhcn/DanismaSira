using DanismaSira.Models.ViewModels;
using DataAccessLayer.DbContexts;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DanismaSira.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DepartmentController(ApplicationDbContext db)
        {
            _db = db;


        }
        public IActionResult Index()
        {

            var departments = _db.Departments.Where(x=>x.IsDeleted!=true).ToList();
            var viewModel = new DepartmentListVM()
            {
                Departments = departments,
            };
            



            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateDepartment()
        {
            return View();
        }
        [HttpPost]

        public IActionResult CreateDepartment(DepartmentVM createDepartment)

        {
            if (ModelState.IsValid)
            {
                var newDepartment = new Department()
                {
                    departmentName = createDepartment.DepartmentId,
                    departmentDescription = createDepartment.departmentDescription,
                    


                };
                _db.Departments.Add(newDepartment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(createDepartment);

        }


        public IActionResult DeleteDepartment(int id)
        {
            var department = _db.Departments.FirstOrDefault(x => x.departmentId == id);

            if (department != null)
            {
                department.IsDeleted = true;
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
