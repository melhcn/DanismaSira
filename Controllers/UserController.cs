using DanismaSira.Models.ViewModels;
using DataAccessLayer.DbContexts;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Entities;

namespace DanismaSira.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;


        }

        public IActionResult Index()
        {
            var users = _db.Users.Where(x => x.IsDeleted != true).ToList();

            var viewModel = new UserListVM()
            {
                Users = users
            };




            return View(viewModel);
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]

        public IActionResult CreateUser(UserVM createUser)

        {

            //kontrol konulduğunda çalışmıyor if (ModelState.IsValid)    
            var newUser = new User()
                {
                    Name = createUser.Name,
                    Surname = createUser.Surname,
                    TcOrItuNumber = createUser.TcOrItuNumber,
                    IsItuMember = createUser.IsItuMember,


                };
                _db.Users.Add(newUser);
                _db.SaveChanges();
                return RedirectToAction("Index");
            
            

        }

        public IActionResult DeleteUser(int id)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == id);

            if (user != null)
            {
                user.IsDeleted = true;
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }




    }
}
