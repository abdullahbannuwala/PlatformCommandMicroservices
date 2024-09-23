using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

                var platforms = grpcClient?.ReturnAllPlatforms() ?? new List<Platform>();

                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms);
            }
        }

        private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
        {
            Console.WriteLine("Seeding new platforms...");

            if (platforms != null && platforms.Any())
            {
                foreach (var plat in platforms)
                {
                    repo.CreatePlatform(plat);
                }
                repo.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> No platforms to seed.");
            }
        }
    }

}
