using Coelsa.Repositories;
using Coelsa.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coelsa.UnitTests
{
    public class IoC
    {
        public static IConfiguration Configuration { get; set; }


        private static ServiceCollection _ServiceCollection;
        private static readonly ServiceProvider _ServiceProvider;


        public static ServiceCollection ServiceCollection
        {
            get
            {
                if (_ServiceCollection == null)
                {
                    _ServiceCollection = new ServiceCollection();

                    //load configuration
                    LoadConfiguration();

                    //Repository
                    ServiceCollection.AddScoped<IContactRepository, ContactRepository>();

                    //Service
                    ServiceCollection.AddTransient<IContactService, ContactService>();

                }
                return _ServiceCollection;
            }
        }

        public static void LoadConfiguration()
        {
            //var myConfiguration = new Dictionary<string, string>
            //                    {
            //                        {"MongoSettings:Connection", "mongodb://diego.ardila:Optimus%232020@localhost:27017/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=false"},
            //                        {"MongoSettings:DatabaseName", "Contactes"}
            //                    };
            //Configuration = new ConfigurationBuilder()
            //.AddInMemoryCollection(myConfiguration)
            //.Build();
            Configuration = new ConfigurationBuilder()
           .AddJsonFile("./appsettings.json", true, true)
           .Build();
        }

        public static ServiceProvider ServiceProvider
        {
            get
            {
                return _ServiceProvider ?? ServiceCollection.BuildServiceProvider();
            }
        }


    }

}
