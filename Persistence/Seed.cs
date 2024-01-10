using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context){
            if(context.Activities.Any()) return;

            var activities = new List<Activity>{
                new Activity{
                    Title = "Past Travel 1",
                    Date = DateTime.UtcNow.AddMonths(-5),
                    Description = "Travels 5 months ago",
                    Category = "travel",
                    Location = "Ireland"
                },
                new Activity{
                    Title = "Past Travel 2",
                    Date = DateTime.UtcNow.AddMonths(-13),
                    Description = "Travels 13 months ago",
                    Category = "travel",
                    Location = "Portugal"
                },
                new Activity{
                    Title = "Furture Travel 1",
                    Date = DateTime.UtcNow.AddMonths(+5),
                    Description = "Travels 5 months away",
                    Category = "travel",
                    Location = "Thailand"
                }
            };
            await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}