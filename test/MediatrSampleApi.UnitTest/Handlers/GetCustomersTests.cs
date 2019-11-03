using System;
using System.Linq;
using System.Threading.Tasks;
using MediatrSampleApi.Handlers.Command;
using MediatrSampleApi.Handlers.Query;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediatrSampleApi.UnitTest.Handlers
{
    [TestClass]
    public class GetCustomersTests: TestBase
    {
        [TestMethod]
        public async Task ShouldGetTwoCustomers_WhenTwoRecordsAreAdded()
        {
            var firstItem = fixture.Build<CustomerRequest>()
               .With(x => x.Name, "Salih Igde")
               .With(x => x.Email, "salihigde@gmail.com")
               .Create();

            var secondItem = fixture.Build<CustomerRequest>()
               .With(x => x.Name, "Salih Igde2")
               .With(x => x.Email, "salihigde2@gmail.com")
               .Create();

            await mediator.Send(firstItem);
            await mediator.Send(secondItem);

            var results = await mediator.Send(new CustomerListRequest());

            results.FirstOrDefault(x => x.Email == "salihigde@gmail.com").Should().NotBeNull();
            results.FirstOrDefault(x => x.Email == "salihigde2@gmail.com").Should().NotBeNull();
        }

        [TestMethod]
        public void ShouldThrowArgumentNullException_RequestIsNull()
        {
            CustomerListRequest customerRequest = null;
            Action createCustomerAction = () => mediator.Send(customerRequest);

            createCustomerAction.Should().Throw<ArgumentNullException>();
        }
    }
}
