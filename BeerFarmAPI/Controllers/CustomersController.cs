using BeerFarmAPI.Models;
using BeerFarmAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerFarmAPI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomersServices customersServices;

        public CustomersController()
        {
            customersServices = new CustomersServices();
        }

        [HttpGet]
        [Authorize]
        public string GetAll()
        {
            var json = JsonConvert.SerializeObject(customersServices.GetAllCustomers().Select(cus => new
            {
                ID = cus.ID,
                Email = cus.Email,
                Name = cus.Name,
                Phone = cus.Phone
            }));
            return json;
        }

        [HttpPost]
        [Authorize(Roles = "readingApi")]
        public string Post(Customer cus)
        {
            return customersServices.AddCustomer(cus);
        }

        [HttpGet]
        [Authorize(Roles = "readingApi")]
        public string GetCustomerById(int id)
        {
            var cus = customersServices.GetByID(id);
            if (cus == null)
            {
                return HttpNotFound().ToString();
            }

            var json = JsonConvert.SerializeObject(new
            {
                ID = cus.ID,
                Email = cus.Email,
                Name = cus.Name,
                Phone = cus.Phone
            });
            return json;
        }

        [HttpPut]
        [Authorize(Roles = "readingApi")]
        public bool Put(Customer cus)
        {
            return customersServices.UpdateCustomer(cus);
        }

        [HttpDelete]
        [Authorize(Roles = "readingApi")]
        public bool Delete(int id)
        {
            return customersServices.Delete(id);
        }
    }
}