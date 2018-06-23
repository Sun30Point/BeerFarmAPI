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
    public class BarrelsController : Controller
    {
        private readonly BarrelsServices barrelsServices;

        public BarrelsController()
        {
            barrelsServices = new BarrelsServices();
        }

        [Authorize]
        [HttpGet]
        public string GetAll()
        {
            var json = JsonConvert.SerializeObject(barrelsServices.GetAllBarrels().Select(s => new
            {
                ID = s.ID,
                Name = s.Name,
                Description = s.Description
            }));
            return json;
        }

        
        [HttpGet]
        [Authorize(Roles = "readingApi")]
        public string GetBarrelById(int id)
        {
            var barrel = barrelsServices.GetById(id);
            if (barrel == null)
            {
                return HttpNotFound().ToString();
            }

            var json = JsonConvert.SerializeObject(new
            {
                ID = barrel.ID,
                Name = barrel.Name,
                Description = barrel.Description
            });
            return json;
        }

        [HttpPost]
        [Authorize(Roles = "readingApi")]
        public string Post(Barrel barrel)
        {
            return barrelsServices.AddBarrel(barrel);
        }

        [HttpPut]
        [Authorize(Roles = "readingApi")]
        public bool Put(Barrel barrel)
        {
            return barrelsServices.UpdateBarrel(barrel);
        }

        [HttpDelete]
        [Authorize(Roles = "readingApi")]
        public bool Delete(int id)
        {
            return barrelsServices.Delete(id);
        }
    }
}