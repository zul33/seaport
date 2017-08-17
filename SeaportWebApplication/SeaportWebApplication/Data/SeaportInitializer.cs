using SeaportWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SeaportWebApplication.Data
{
    public class SeaportInitializer : DropCreateDatabaseIfModelChanges<SeaportContext>
    {
        protected override void Seed(SeaportContext context)
        {
            var piers = new List<Pier>
            {
                new Pier { Name = "Pier 1" },
                new Pier { Name = "Pier 2" },
                new Pier { Name = "Pier 3" },
                new Pier { Name = "Pier 4" },
                new Pier { Name = "Pier 5" }
            };

            piers.ForEach(p => context.Piers.Add(p));
            context.SaveChanges();

            var ships = new List<Ship>
            {
                new Ship { Name = "Albatros" },
                new Ship { Name = "Leora" },
                new Ship { Name = "Northild" },
                new Ship { Name = "Finnlay" },
                new Ship { Name = "Serena" },
                new Ship { Name = "Vanity" },
                new Ship { Name = "Yamato" },
                new Ship { Name = "Juan" },
                new Ship { Name = "Benjioro" },
                new Ship { Name = "Izar" }
            };

            ships.ForEach(s => context.Ships.Add(s));
            context.SaveChanges();                        
        }
    }
}