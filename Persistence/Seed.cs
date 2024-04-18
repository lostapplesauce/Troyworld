using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any() && !context.Activities.Any())
            {
                var users = new List<AppUser>{
                    new AppUser{DisplayName = "Bob", UserName="bob", Email="bob@test.com"},
                    new AppUser{DisplayName = "Sarah", UserName="Sarah", Email="sarah@test.com"},
                    new AppUser{DisplayName = "Tom", UserName="tom", Email="tom@test.com"}
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }


                if (context.Activities.Any()) return;

                var activities = new List<Activity>
            {
                new Activity
                {
                    Title = "Past Travel 1",
                    Date = DateTime.UtcNow.AddMonths(-5),
                    Description = "Travels 5 months ago",
                    Category = "travel",
                    Location = "Ireland",
                    Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = true                            
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[0],
                                IsHost = false                            
                            },
                        }
                },
                new Activity
                {
                    Title = "Past Travel 2",
                    Date = DateTime.UtcNow.AddMonths(-13),
                    Description = "Travels 13 months ago",
                    Category = "travel",
                    Location = "Portugal",
                    Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = true                            
                            }
                        }
                },
                new Activity
                {
                    Title = "Furture Travel 11",
                    Date = DateTime.UtcNow.AddMonths(+5),
                    Description = "Travels 5 months away",
                    Category = "travel",
                    Location = "Thailand",
                    Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[0],
                                IsHost = true                            
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false                            
                            },
                        }
                }
            };

                await context.Activities.AddRangeAsync(activities);
                await context.SaveChangesAsync();
            }
        }
    }
}