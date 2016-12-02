using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace SampleWebApi.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SampleDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<SampleDbContext>>()))
            {
                
                if(context.People.Any())
                {
                    return;
                }

                context.People.AddRange(
                    new Person
                    {
                        FirstName = "tim",
                        LastName = "johnson"
                    },
                    new Person
                    {
                        FirstName = "Bob",
                        LastName = "Waters"
                    }
                    );

                context.SaveChanges();

            }
        }
    }
}
