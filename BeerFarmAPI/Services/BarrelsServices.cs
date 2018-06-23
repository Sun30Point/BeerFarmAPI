using BeerFarmAPI.Entity;
using BeerFarmAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerFarmAPI.Services
{
    public class BarrelsServices
    {
        private readonly BeerFarmDBEntities db;

        public BarrelsServices()
        {
            this.db = new BeerFarmDBEntities();
        }

        public IEnumerable<Barrels> GetAllBarrels()
        {
            return db.Barrels.ToList().Where(card=>card.IsActive!=false);
        }

        public Barrels GetById(int id)
        {
            var res = db.Barrels.Find(id);
            if (res.IsActive != false) {
                return res;
            }
            return null;
        }

        public string AddBarrel(Barrel barrel)
        {
            string idNumber = "";
            try
            {
                Entity.Barrels barrels = db.Barrels.Add(new Entity.Barrels()
                {
                    Name = barrel.Name,
                    Description = barrel.Description,
                    IsActive = true
                });
                db.SaveChanges();
                idNumber = barrels.ID.ToString();

            }
            catch (Exception ex)
            {

                idNumber = ex.Message;
            }

            return idNumber;
        }

        public bool UpdateBarrel(Barrel barrel)
        {
            var storedBarrel = db.Barrels.Find(barrel.ID);

            if (barrel == null || storedBarrel == null) return false;

            storedBarrel.Name = barrel.Name;
            storedBarrel.Description = barrel.Description;

            db.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var barrel = db.Barrels.Find(id);
            if (barrel == null)
            {
                return false;
            }

            barrel.IsActive = false;
            db.SaveChanges();
            return true;
        }
    }
}