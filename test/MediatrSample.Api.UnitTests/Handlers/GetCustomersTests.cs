using System;
using System.Linq;
using System.Threading.Tasks;
using MediatrSample.Api.Handlers.Command;
using MediatrSample.Api.Handlers.Query;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediatrSample.Api.UnitTests.Handlers;

[TestClass]
public class GetCustomersTests : TestBase
{
    [TestMethod]
    public async Task ShouldGetTwoCustomers_WhenTwoRecordsAreAdded()
    {
        var firstItem = Fixture.Build<CustomerRequest>()
            .With(x => x.Name, "Salih Igde")
            .With(x => x.Email, "salihigde@gmail.com")
            .Create();

        var secondItem = Fixture.Build<CustomerRequest>()
            .With(x => x.Name, "Salih Igde2")
            .With(x => x.Email, "salihigde2@gmail.com")
            .Create();

        await Mediator.Send(firstItem);
        await Mediator.Send(secondItem);

        var results = await Mediator.Send(new CustomerListRequest());

        results.FirstOrDefault(x => x.Email == "salihigde@gmail.com").Should().NotBeNull();
        results.FirstOrDefault(x => x.Email == "salihigde2@gmail.com").Should().NotBeNull();
    }

    [TestMethod]
    public void ShouldThrowArgumentNullException_RequestIsNull()
    {
        CustomerListRequest customerRequest = null;
        Action createCustomerAction = () => Mediator.Send(customerRequest);

        createCustomerAction.Should().Throw<ArgumentNullException>();
    }
}