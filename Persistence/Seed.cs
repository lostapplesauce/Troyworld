using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context){
            if(context.Activities.Any()) return;

            var activities = new List<Activity>{
                new Activity{
                    Title = "Travel 1",
                    Date = DateTime.UtcNow.AddMonths(-5),
                    Description = "Travels 5 months ago",
                    Location = "Ireland"
                },
                new Activity{
                    Title = "Travel 2",
                    Date = DateTime.UtcNow.AddMonths(-13),
                    Description = "Travels 13 months ago",
                    Location = "Portugal"
                },
                new Activity{
                    Title = "Furture Travel 1",
                    Date = DateTime.UtcNow.AddMonths(+5),
                    Description = "Travels 5 months away",
                    Location = "Thailand"
                }
            };
            await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}