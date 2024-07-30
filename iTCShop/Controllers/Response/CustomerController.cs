﻿using iTCShop.Extensions;
using Newtonsoft.Json;

namespace iTCShop.Controllers.Response
{

    public class CustomerController(ICustomerServices customerServices, ICartService cartService) : Controller
    {
        //public async Task<IActionResult> GetAllCustomers()
        //{
                
        //    return RedirectToAction("HomeAdminCustomers", "Admin");
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
                var customer = new Customer(customerRequest.Name, customerRequest.Email, customerRequest.UserName, customerRequest.Password, customerRequest.Phone, customerRequest.Address, customerRequest.DateOfBirth);
                cartService.CreateCart(customer.ID);
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
        public async Task<IActionResult> UpdateStatus(string id)
        {
            var customer = await customerServices.GetCustomerById(id);
            if (customer.Status == CustomerStatus.Available) customer.Status = CustomerStatus.Banned;
            else customer.Status = CustomerStatus.Available;
            var rs = customerServices.UpdateCustomer(customer);
            if (rs.IsSuccess())
            {
                var customers = await customerServices.GetAll();
                TempData["customers"] = JsonConvert.SerializeObject(customers);
                return RedirectToAction("HomeAdminCustomer", "Admin");
            }
            return BadRequest(rs);
        }
        public ActionResult GetCustomerInfo()
        {
            var customer = HttpContext.Session.GetObjectFromJson<Customer>("user");
            if (customer == null) return RedirectToAction("Login", "Login");
            else return View(customer);
        }

        public async Task<IActionResult> Edit()
        {
            var customer = await customerServices.GetCustomerById(HttpContext.Session.GetObjectFromJson<Customer>("user").ID);
            return View(customer);
        }

        public async Task<IActionResult> Search(string search, string sort)
        {
            var customers = await customerServices.GetAll();
            var customerLst = new List<Customer>();
                switch (sort)
                {
                    case "ID":
                        customerLst = customers.Where(p => p.ID.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case "Name":
                        customerLst = customers.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    default:
                        customerLst = customers.Where(p => p.UserName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                }
                TempData["customers"] = JsonConvert.SerializeObject(customerLst);
                return RedirectToAction("HomeAdminCustomers", "Admin"); ;
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Customer updatedCustomer)
        {
            var customer = await customerServices.GetCustomerById(HttpContext.Session.GetObjectFromJson<Customer>("user").ID);
            customer.Name = updatedCustomer.Name;
            customer.Email = updatedCustomer.Email;
            customer.Password = updatedCustomer.Password;
            customer.Phone = updatedCustomer.Phone;
            customer.Address = updatedCustomer.Address;
            customer.DateOfBirth = updatedCustomer.DateOfBirth;

            var rs = customerServices.UpdateCustomer(customer);
            HttpContext.Session.SetObjectAsJson("user", customer);
            if (rs.IsSuccess()) return RedirectToAction("GetCustomerInfo");
            else
            {
                ViewBag.Fail = "UpdateFailed";
                return View("Edit", customer);
            }
        }
    }
}
