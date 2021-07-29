using CarShopWebProject.Data;
using CarShopWebProject.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopWebProject.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<GameShopDbContext>();

            data.Database.Migrate();

            SeedCategories(data);
            SeedPlatform(data);

            return app;
        }

        private static void SeedCategories(GameShopDbContext db)
        {
            if (db.Category.Any())
            {
                return;
            }

            db.Category.AddRange(new[]
            {
                new Category {Name = "Action"},
                new Category {Name = "Action-adventure"},
                new Category {Name = "Adventure"},
                new Category {Name = "Role-playing"},
                new Category {Name = "Simulation "},
                new Category {Name = "Strategy  "},
                new Category {Name = "Sports "},
                new Category {Name = "Puzzle "}
            });

            db.SaveChanges();
        }

        private static void SeedPlatform(GameShopDbContext db)
        {
            if (db.Platform.Any())
            {
                return;
            }

            db.Platform.AddRange(new[]
            {
                new Platform {Name = "Steam Games"},
                new Platform {Name = "PSN"},
                new Platform {Name = "Xbox"},
                new Platform {Name = "Nintendo"},
                new Platform {Name = "Uplay Games "},
                new Platform {Name = "Origin Games  "},
                new Platform {Name = "Epic Games "}
            });

            db.SaveChanges();
        }
    }
}
