using BeerFarmAPI.Entity;
using BeerFarmAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BeerFarmAPI.Services
{
    public class UnitsServices
    {
        private readonly BeerFarmDBEntities db;

        public UnitsServices()
        {
            this.db = new BeerFarmDBEntities();
        }

        public IEnumerable<Units> GetAllUnits()
        {
            return db.Units.ToList().Where(unit =>unit.IsActive!=false);
        }

        public Units GetById(int id)
        {
            var res = db.Units.Find(id);
            if (res.IsActive != false)
            {
                return res;
            }
            return null;
        }

        public string AddUnit(Unit unit)
        {
            string idNumber = "";
            try
            {
                Entity.Units units = db.Units.Add(new Entity.Units()
                {
                    Name = unit.Name,
                    Symbol = unit.Symbol,
                    Description = unit.Description,
                    IsActive = true
                });
                db.SaveChanges();
                idNumber = units.ID.ToString();

            }
            catch (Exception ex)
            {

                idNumber = ex.Message;
            }

            return idNumber;
        }

        public bool UpdateUnit(Unit unit)
        {
            var storedUnit = db.Units.Find(unit.ID);

            if (unit == null || storedUnit == null) return false;

            storedUnit.Name = unit.Name;
            storedUnit.Symbol = unit.Symbol;
            storedUnit.Description = unit.Description;

            db.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var unit = db.Units.Find(id);
            if (unit == null)
            {
                return false;
            }

            unit.IsActive = false;
            db.SaveChanges();
            return true;
        }
    }
}