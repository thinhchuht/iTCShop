using iTCShop.Data;
using iTCShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace iTCShop.Controllers.Response
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeUserController : Controller
    {
        private readonly iTCShopDbContext _dbContext;

        public AuthorizeUserController(iTCShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("/get")]
        public IActionResult GetAll()
        {

            var data = _dbContext.AuthorizeUsers.ToList();
            return Ok(data);
        }

        [HttpPost("/add")]
        public IActionResult Add(AuthorizeUser user)
        {
            _dbContext.AuthorizeUsers.Add(user);
            _dbContext.SaveChanges();
            return Ok(user);
        }
    }
}
