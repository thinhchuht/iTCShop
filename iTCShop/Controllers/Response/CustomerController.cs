namespace iTCShop.Controllers.Response
{

    public class CustomerController(ICustomerServices customerServices) : Controller
    {
        //public async Task<IActionResult> GetAllCustomers()
        //{

        //    return RedirectToAction("HomeAdminCustomers", "Admin");
        //}
        public ActionResult RegisterCustomer()
        {
            if (HttpContext.Session.GetCustomer() != null)
            {
                HttpContext.Session.Clear();
                TempData.PutResponse(ResponseModel.FailureResponse("You have been logged out of the current account."));

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(CustomerRequest customerRequest)
        {
            try
            {
                var customer = new Customer(customerRequest.Name, customerRequest.Email, customerRequest.UserName, customerRequest.Password, "0"+customerRequest.Phone, customerRequest.Address, customerRequest.DateOfBirth);
                var rs = await customerServices.AddCustomer(customer);
                if(rs.IsSuccess())
                ViewBag.RegRs = "Register sucessfully. Go to login.";
                TempData.PutResponse(rs);
                return View("RegisterCustomer");
            }
            catch
            {
                TempData.PutResponse(ResponseModel.ExceptionResponse());
                return View();
            }
        }

        public async Task<IActionResult> UpdateStatus(string id)
        {
            var customer = await customerServices.GetCustomerById(id);
            if (customer.Status == CustomerStatus.Available) customer.Status = CustomerStatus.Banned;
            else customer.Status = CustomerStatus.Available;
            var rs = customerServices.UpdateCustomer(customer);
            if (!rs.IsSuccess()) TempData.PutResponse(rs);
            var customers = await customerServices.GetAll();
            TempData.Put("customers", customers);
            return RedirectToAction("HomeAdminCustomers", "Admin");
        }
        public ActionResult GetCustomerInfo()
        {
            var customer = HttpContext.Session.GetCustomer();
            if (customer == null) return RedirectToAction("Login", "Login");
            else return View(customer);
        }

        public async Task<IActionResult> Edit()
        {
            var customer = await customerServices.GetCustomerById(HttpContext.Session.GetCustomer().ID);
            return View(customer);
        }

        public async Task<IActionResult> Search(string search, string sort)
        {
            var customers = await customerServices.GetAll();
            var customerLst = new List<Customer>();
            customerLst = sort switch
            {
                "ID" => customers.Where(p => p.ID.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList(),
                "Name" => customers.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList(),
                "Phone" => customers.Where(p => p.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList(),
                _ => customers.Where(p => p.UserName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList(),
            };
            TempData.Put("customers", customerLst);
            TempData["sort"] = sort;
            TempData["search"] = search;
            return RedirectToAction("HomeAdminCustomers", "Admin"); ;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer updatedCustomer)
        {
            var customer         = await customerServices.GetCustomerById(HttpContext.Session.GetCustomer().ID);
            customer.Name        = updatedCustomer.Name;
            customer.Email       = updatedCustomer.Email;
            customer.Password    = updatedCustomer.Password;
            customer.Phone       = "0"+updatedCustomer.Phone;
            customer.Address     = updatedCustomer.Address;
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
