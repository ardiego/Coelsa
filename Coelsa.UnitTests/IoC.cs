using Coelsa.Common;
using Coelsa.Models;
using Coelsa.Repositories;
using Coelsa.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;

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
                    
                    //Configuration
                    var jsonConfig = JsonConvert.DeserializeObject<SettingModel>(File.ReadAllText("./appsettings.json"));
                    ServiceCollection.Configure<SettingModel>(options => options.SqlConnection = jsonConfig.SqlConnection);

                    //Logger
                    ServiceCollection.AddSingleton<NLogger>();

                    //Repository
                    ServiceCollection.AddScoped<IContactRepository, ContactRepository>();

                    //Service
                    ServiceCollection.AddTransient<IContactService, ContactService>();

                }
                return _ServiceCollection;
            }
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
