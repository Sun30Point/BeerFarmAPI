using BeerFarmAPI.Entity;
using BeerFarmAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerFarmAPI.Services
{
    public class CustomersServices
    {
        private BeerFarmDBEntities db;

        public CustomersServices()
        {
            this.db = new BeerFarmDBEntities();
        }

        public IEnumerable<Customers> GetAllCustomers()
        {
            return db.Customers.ToList().Where(customer=>customer.IsActive!=false);
        }

        public Customers GetByID(int id)
        {
            var res = db.Customers.Find(id);
            if (res.IsActive != false) {
                return res;
            }
            return null;
        }

        public string AddCustomer(Customer cus)
        {
            string idNumber = "";
            try
            {
                Entity.Customers customer = db.Customers.Add(new Entity.Customers()
                {
                    Name = cus.Name,
                    Phone = cus.Phone,
                    IsActive = true
                });
                db.SaveChanges();
                idNumber = customer.ID.ToString();

            }
            catch (Exception ex)
            {

                idNumber = ex.Message;
            }

            return idNumber;
        }

        public bool UpdateCustomer(Customer customer)
        {
            var storedCustmer = db.Customers.Find(customer.ID);

            if (customer == null || storedCustmer == null) return false;

            storedCustmer.Name = customer.Name;
            storedCustmer.Phone = customer.Phone;

            db.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return false;
            }

            customer.IsActive = false;
            db.SaveChanges();
            return true;
        }
    }
}