using BeerFarmAPI.Entity;
using BeerFarmAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerFarmAPI.Services
{
    public class CardsServices
    {
        private readonly BeerFarmDBEntities db;

        public CardsServices()
        {
            this.db = new BeerFarmDBEntities();
        }

        public IEnumerable<Cards> GetAllCards()
        {
            return db.Cards.ToList().Where(card=>card.IsActive!=false);
        }

        public Cards GetByID(int id)
        {
            var res = db.Cards.Find(id);
            if (res.IsActive != false) {
                return res;
            }
            return null;
        }

        public string AddCard(Card card)
        {
            string idNumber = "";
            try
            {
                Entity.Cards cards = db.Cards.Add(new Entity.Cards()
                {
                    Guid = card.Guid,
                    Total = card.Total,
                    IsActive = true
                });
                db.SaveChanges();
                idNumber = cards.ID.ToString();

            }
            catch (Exception ex)
            {

                idNumber = ex.Message;
            }

            return idNumber;
        }

        public bool UpdateCard(Card card)
        {
            var storedCard = db.Cards.Find(card.ID);

            if (card == null || storedCard == null) return false;

            storedCard.Guid = card.Guid;
            storedCard.Total = card.Total;

            db.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var card = db.Cards.Find(id);
            if (card == null)
            {
                return false;
            }

            card.IsActive = false;
            db.SaveChanges();
            return true;
        }
    }
}