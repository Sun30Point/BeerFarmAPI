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
    public class UnitsController : Controller
    {
        private readonly UnitsServices unitsServices;

        public UnitsController()
        {
            unitsServices = new UnitsServices();
        }

        [HttpGet]
        [Authorize]
        public string GetAll()
        {
            var json = JsonConvert.SerializeObject(unitsServices.GetAllUnits().Select(s => new
            {
                ID = s.ID,
                Name = s.Name,
                Symbol = s.Symbol,
                Description = s.Description
            }));
            return json;
        }

        [HttpGet]
        [Authorize(Roles = "readingApi")]
        public string GetUnitById(int id)
        {
            var unit = unitsServices.GetById(id);
            if (unit == null)
            {
                return HttpNotFound().ToString();
            }

            var json = JsonConvert.SerializeObject(new
            {
                ID = unit.ID,
                Name = unit.Name,
                Symbol = unit.Symbol,
                Description = unit.Description
            });
            return json;
        }

        [HttpPost]
        [Authorize(Roles = "readingApi")]
        public string Post(Unit unit)
        {
            return unitsServices.AddUnit(unit);
        }

        [HttpPut]
        [Authorize(Roles = "readingApi")]
        public bool Put(Unit unit)
        {
            return unitsServices.UpdateUnit(unit);
        }

        [HttpDelete]
        [Authorize(Roles = "readingApi")]
        public bool Delete(int id)
        {
            return unitsServices.Delete(id);
        }
    }
}