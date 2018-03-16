namespace EagleEye2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EagleEye2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EagleEye2.Models.EagleEye2Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EagleEye2.Models.EagleEye2Context context)
        {
            context.Users.AddOrUpdate(x => x.Id,
                new User() { Id = 1, Name = "Bret S.", Department = "Development", AccessLevel = 1 },
                new User() { Id = 2, Name = "Caleb C.", Department = "Management", AccessLevel = 1 },
                new User() { Id = 3, Name = "Walter L.", Department = "Marketing", AccessLevel = 2 },
                new User() { Id = 4, Name = "Nathaniel B.", Department = "Human Resources", AccessLevel = 2 },
                new User() { Id = 5, Name = "Aaron J.", Department = "Finanace", AccessLevel = 3 },
                new User() { Id = 6, Name = "Travis P.", Department = "Sarcasm", AccessLevel = 3 }
                );

            context.Stores.AddOrUpdate(x => x.Id,
                new Store() { Id = 1, Name = "Starbucks", Location = "Kansas City"},
                new Store() { Id = 2, Name = "Best Buy", Location = "Lee's Summit" },
                new Store() { Id = 3, Name = "The Club", Location = "Kansas City" },
                new Store() { Id = 4, Name = "Suspicious Store", Location = "Beijing" },
                new Store() { Id = 5, Name = "Apple", Location = "Kansas City" }
                );

            context.Cards.AddOrUpdate(x => x.Id,
                new Card() { Id = 1234, UserId = 1 },
                new Card() { Id = 5678, UserId = 2 },
                new Card() { Id = 9012, UserId = 3 },
                new Card() { Id = 3456, UserId = 4 },
                new Card() { Id = 7890, UserId = 5 },
                new Card() { Id = 1357, UserId = 6 }
                );

            context.Transactions.AddOrUpdate(x => x.Id,
                new Transaction() { Id = 1, Amount = 12.88, CardId = 1234, StoreId = 1 },
                new Transaction() { Id = 2, Amount = 99.98, CardId = 1234, StoreId = 2 },
                new Transaction() { Id = 3, Amount = 256.78, CardId = 5678, StoreId = 3 },
                new Transaction() { Id = 4, Amount = 45.88, CardId = 3456, StoreId = 5 },
                new Transaction() { Id = 5, Amount = 10.99, CardId = 9012, StoreId = 1 },
                new Transaction() { Id = 6, Amount = 763.01, CardId = 1357, StoreId = 4 },
                new Transaction() { Id = 7, Amount = 1.99, CardId = 7890, StoreId = 1 },
                new Transaction() { Id = 8, Amount = 202.97, CardId = 1234, StoreId = 1 },
                new Transaction() { Id = 9, Amount = 4500.68, CardId = 3456, StoreId = 5 },
                new Transaction() { Id = 10, Amount = 86.08, CardId = 5678, StoreId = 3 }
                );
        }
    }
}
