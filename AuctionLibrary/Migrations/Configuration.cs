using AuctionLibrary.Models;
using System;
using System.Data.Entity.Migrations;

namespace AuctionLibrary.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AuctionLibrary.Storage.AuctionDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AuctionLibrary.Storage.AuctionDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            ContactInfo c1 = new ContactInfo("Kim", "12345678", "kim@eksempel.dk");
            ContactInfo c2 = new ContactInfo("Mathias", "87654321", "mathias@eksempel.dk");
            ContactInfo c3 = new ContactInfo("Anne Lisbeth Møller Johansen-Jensen", "24681012", "anne@eksempel.dk");
            ContactInfo c4 = new ContactInfo("Bob Allan Aagaard Abildgaard-Pedersen", "12108642", "bob@eksempel.dk");

            SalesSupply s1 = new SalesSupply(MetalType.Guld, 50, new DateTime(2020, 05, 10, 13, 00, 00), c1);
            SalesSupply s2 = new SalesSupply(MetalType.Sølv, 30, new DateTime(2020, 05, 30, 17, 00, 00), c1);
            SalesSupply s3 = new SalesSupply(MetalType.Platin, 100, new DateTime(2020, 05, 23, 08, 30, 00), c2);
            SalesSupply s4 = new SalesSupply(MetalType.Palladium, 80, new DateTime(2020, 06, 02, 15, 30, 00), c2);
            SalesSupply s5 = new SalesSupply(MetalType.Sølv, 300, new DateTime(2020, 05, 30, 17, 00, 00), c3);
            SalesSupply s6 = new SalesSupply(MetalType.Platin, 150, new DateTime(2020, 05, 23, 08, 30, 00), c3);
            SalesSupply s7 = new SalesSupply(MetalType.Guld, 500, new DateTime(2020, 06, 10, 14, 00, 00), c4);
            SalesSupply s8 = new SalesSupply(MetalType.Sølv, 380, DateTime.Now.AddMinutes(10), c4);

            context.ContactInfoes.AddOrUpdate(c => c.Id, c1);
            context.ContactInfoes.AddOrUpdate(c => c.Id, c2);
            context.ContactInfoes.AddOrUpdate(c => c.Id, c3);
            context.ContactInfoes.AddOrUpdate(c => c.Id, c4);

            context.SalesSupplies.AddOrUpdate(s => s.Id, s1);
            context.SalesSupplies.AddOrUpdate(s => s.Id, s2);
            context.SalesSupplies.AddOrUpdate(s => s.Id, s3);
            context.SalesSupplies.AddOrUpdate(s => s.Id, s4);
            context.SalesSupplies.AddOrUpdate(s => s.Id, s5);
            context.SalesSupplies.AddOrUpdate(s => s.Id, s6);
            context.SalesSupplies.AddOrUpdate(s => s.Id, s7);
            context.SalesSupplies.AddOrUpdate(s => s.Id, s8);
        }
    }
}
