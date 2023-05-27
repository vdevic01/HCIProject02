using HCIProject02.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIProject02.Core.Persistance
{
    public class DatabaseContextSeed
    {
        public static void Seed(DatabaseContext context)
        {
            var attraction01 = new Attraction { Address = "address 01", Name = "attraction01", Description = "desc", Latitude = 1, Longitude = 1, ImagePath = null };
            var attraction02 = new Attraction { Address = "address 02", Name = "attraction02", Description = "desc", Latitude = 1, Longitude = 1, ImagePath = null };
            var hotel01 = new Hotel { Address = "address 03", Name = "Hotel Name", ImagePath=null};
            List<Attraction> attractions = new List<Attraction>();
            attractions.Add(attraction01);
            attractions.Add(attraction02);
            var arrangement = new Arrangement
            {
                Attractions = attractions,
                Name = "Arrangement01",
                DepartureTime = DateTime.Now,
                ReturnTime = DateTime.Now,
                Description = "desc",
                ImagePath = "../../../Resources/Images/Dynamic/Arrangements/belgrade.jpg",
                Price = 112.1,
                IsAvailable = true,
                Hotel = hotel01,
                TripPlan = "trip_plan"
            };

            List<Arrangement> arrs = new List<Arrangement>();
            arrs.Add(arrangement);

            attraction01.Arrangements = arrs;
            attraction02.Arrangements = arrs;

            context.Attractions.Add(attraction01);
            context.Attractions.Add(attraction02);
            context.Hotels.Add(hotel01);
            context.Arrangments.Add(arrangement);
            context.SaveChanges();
        }
    }
}
