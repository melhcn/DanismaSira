using DanismaSira.Models.ViewModels;
using DataAccessLayer.DbContexts;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DanismaSira.Controllers
{
    public class RegistrationController : Controller
    {

        private readonly ApplicationDbContext _db;

        public RegistrationController(ApplicationDbContext db)
        {
            _db = db;


        }
        public IActionResult Index()
        {
            var registrations = _db.Registrations.
                Include(x=>x.Department).
                Include(x => x.User).
                Where(x => x.IsDeleted != true).ToList();

            var viewModel = new RegistrationListVM()
            {
                Registrations = registrations
            };




            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateRegistration()
        {
            
            var departmentList = _db.Departments.Where(x => x.IsDeleted != true).ToList();

            var viewModel = new RegistrationVM()

            {
                Departments = departmentList
            };


            return View(viewModel);
        }
        [HttpPost]

        public IActionResult CreateRegistration(RegistrationVM createRegistration)

        {
            var existingUser = _db.Users.FirstOrDefault(x => x.TcOrItuNumber == createRegistration.User.TcOrItuNumber);

            if (existingUser != null)
            {

                var newRegistration = new Registration()
                {
                    DepartmentId = createRegistration.DepartmentId,
                    UserId = existingUser.UserId,
                };

                _db.Registrations.Add(newRegistration);
            }
            else
            {

                var newUser = new User()
                {
                    Name = createRegistration.User?.Name,
                    Surname = createRegistration.User?.Surname,
                    TcOrItuNumber = createRegistration.User?.TcOrItuNumber,
                    IsItuMember = createRegistration.User?.IsItuMember,
                };

                var newRegistration = new Registration()
                {
                    DepartmentId = createRegistration.DepartmentId,
                    User = newUser,
                };

                _db.Users.Add(newUser);
                _db.Registrations.Add(newRegistration);
            }

            _db.SaveChanges();
            return RedirectToAction("Index");

        }






        
        public IActionResult DeleteRegistration(int id)
        {
            var registration = _db.Registrations.FirstOrDefault(x => x.RegistrationId == id);

            if (registration != null)
            {
                registration.IsDeleted = true;
                _db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }


    }
}

        
