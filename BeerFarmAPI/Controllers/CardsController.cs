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
    public class CardsController : Controller
    {
        private readonly CardsServices cardsServices;

        public CardsController()
        {
            cardsServices = new CardsServices();
        }

        [HttpGet]
        [Authorize]
        public string GetAll()
        {
            var json = JsonConvert.SerializeObject(cardsServices.GetAllCards().Select(s => new
            {
                ID = s.ID,
                Guid = s.Guid,
                Total = s.Total
            }));
            return json;
        }

        [HttpGet]
        [Authorize(Roles = "readingApi")]
        public string GetCardById(int id)
        {
            var card = cardsServices.GetByID(id);
            if (card == null)
            {
                return HttpNotFound().ToString();
            }

            var json = JsonConvert.SerializeObject(new
            {
                ID = card.ID,
                Guid = card.Guid,
                Total = card.Total
            });
            return json;
        }

        [HttpPost]
        [Authorize(Roles = "readingApi")]
        public string Post(Card card)
        {
            return cardsServices.AddCard(card);
        }

        [HttpPut]
        [Authorize(Roles = "readingApi")]
        public bool Put(Card card)
        {
            return cardsServices.UpdateCard(card);
        }

        [HttpDelete]
        [Authorize(Roles = "readingApi")]
        public bool Delete(int id)
        {
            return cardsServices.Delete(id);
        }

    }
}