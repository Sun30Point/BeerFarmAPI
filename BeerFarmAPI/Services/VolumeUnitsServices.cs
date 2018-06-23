using BeerFarmAPI.Entity;
using BeerFarmAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerFarmAPI.Services
{
    public class VolumeUnitsServices
    {
        private readonly BeerFarmDBEntities db;

        public VolumeUnitsServices()
        {
            this.db = new BeerFarmDBEntities();
        }

        public IEnumerable<VolumeUnits> GetAllUnits()
        {
            return db.VolumeUnits.ToList().Where(volumeUnit => volumeUnit.IsActive!=false);
        }

        public VolumeUnits GetById(int id)
        {
            var res = db.VolumeUnits.Find(id);
            if (res.IsActive != false) {
                return res;
            }
            return null;
        }

        public string AddVolumeUnit(VolumeUnit volumeUnit)
        {
            string idNumber = "";
            try
            {
                Entity.VolumeUnits volumeUnits = db.VolumeUnits.Add(new Entity.VolumeUnits()
                {
                    Name = volumeUnit.Name,
                    Symbol = volumeUnit.Symbol,
                    Description = volumeUnit.Description,
                    IsActive = true
                });
                db.SaveChanges();
                idNumber = volumeUnits.ID.ToString();

            }
            catch (Exception ex)
            {

                idNumber = ex.Message;
            }

            return idNumber;
        }

        public bool UpdatevolumeUnit(VolumeUnit volumeUnit)
        {
            var storedVolumeUnit = db.Units.Find(volumeUnit.ID);

            if (volumeUnit == null || storedVolumeUnit == null) return false;

            storedVolumeUnit.Name = volumeUnit.Name;
            storedVolumeUnit.Symbol = volumeUnit.Symbol;
            storedVolumeUnit.Description = volumeUnit.Description;

            db.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var volumeUnit = db.VolumeUnits.Find(id);
            if (volumeUnit == null)
            {
                return false;
            }

            volumeUnit.IsActive = false;
            db.SaveChanges();
            return true;
        }
    }
}