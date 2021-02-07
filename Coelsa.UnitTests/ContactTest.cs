using Microsoft.Extensions.DependencyInjection;
using Coelsa.Models;
using Coelsa.Services;
using NUnit.Framework;
using System.Threading.Tasks;
using Coelsa.Repositories;

namespace Coelsa.UnitTests
{
    public class ContactTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task InsertContact()
        {
            //arrage
            var contact = new ContactModel
            {
                FirstName = "First Name Test",
                LastName = "First Name Test",
                Company = "Company Test",
                PhoneNumber = "+54-111-111"

            };

            //act
            var result = await IoC.ServiceProvider.GetService<IContactService>().InsertContact(contact);
            var contactExists = await IoC.ServiceProvider.GetService<IContactRepository>().ExistsAsync(result);

            //assert
            Assert.IsTrue(contactExists);
        }
    }
}