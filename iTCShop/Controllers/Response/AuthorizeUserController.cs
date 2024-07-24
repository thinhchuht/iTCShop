namespace iTCShop.Controllers.Response
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeUserController(iTCShopDbContext _dbContext, IAdminServices adminServices) : Controller
    {
        [HttpGet("/get")]
        public IActionResult GetAll()
        {
            try
            {
                var data = _dbContext.AuthorizeUsers.ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }
        [HttpPost("add-admin")]
        public async Task<IActionResult> AddAdmin(string userName,string password)
        {
            return Ok(await adminServices.AddAdmin(userName, password));
        }

        [HttpPost("/add")]
        public IActionResult Add([FromBody] string Role)
        {
            var user = new AuthorizeUser();
            user.Role = Role;
            _dbContext.AuthorizeUsers.Add(user);
            _dbContext.SaveChanges();
            return Ok(user);
        }
    }
}
