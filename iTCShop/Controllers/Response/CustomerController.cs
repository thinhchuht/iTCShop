using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Web;

namespace iTCShop.Controllers.Response
{

    public class CustomerController(ICustomerServices customerServices) : Controller
    {
        //[HttpGet("get-all")]
        //public async Task<IActionResult> GEtAllCustomer()
        //{

        //    var rs = await customerServices.GetAll();
        //    return Ok(rs);
        //}

        public ActionResult RegisterCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(CustomerRequest customerRequest)
        {
            try
            {
                var customer = new Customer(customerRequest.Name, customerRequest.Email, customerRequest.Password, customerRequest.Phone, customerRequest.Address, customerRequest.DateOfBirth);

                var rs = await customerServices.AddCustomer(customer);
                if (rs.IsSuccess())
                {
                    ViewBag.RegRs = "Đăng kí thành công. Đăng nhập ngay";
                    ViewBag.isReg = true;
                    return View("RegisterCustomer");

                }
                else
                {
                    ViewBag.RegRs = "Đăng kí thất bại. Hãy thử lại";
                    return View("RegisterCustomer");
                }

            }
            catch
            {
                return View();
            }

        }

        public ActionResult LoginCustomer()
        {
            return View();
        }
        [HttpPost] 
        public async Task<ActionResult> LoginCustomer(string email, string password)
        {
         var customer =  await customerServices.CheckCustomerAccount(email, password);
            if (customer != null)
            {
              
                return RedirectToAction("HomePage","Home");
            }
            else { return View(); }
        }
    }
}
