using Microsoft.AspNetCore.Mvc;
using MongoDB.Models;
using MongoDB.Services;

namespace MongoDB.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _usersService;

        public UsersController(UserService usersService) =>
            _usersService = usersService;
        public async Task <IActionResult> Index()
        {
            var users = await _usersService.GetAsync();
            return View(users);
        }
        public async Task<IActionResult> Add()
        { 
            return View(new User());
        }
        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            await _usersService.CreateAsync(user);            
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Dell()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Dell(string id)
        {
            var user = await _usersService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _usersService.RemoveAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, User updatedUser)
        {
            var user = await _usersService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            updatedUser.Id = user.Id;

            await _usersService.UpdateAsync(id, updatedUser);

            return RedirectToAction("Index");
        }
    }
}
