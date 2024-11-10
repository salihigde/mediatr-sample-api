using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatrSample.Api.Handlers.Command;
using MediatrSample.Api.Handlers.Query;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediatrSample.Api.UnitTests.Handlers;

[TestClass]
public class GetCustomerWithOrdersTests: TestBase
{
    private CustomerWithOrdersRequestValidator _validator;

    [TestInitialize]
    public void Init()
    {
        _validator = new CustomerWithOrdersRequestValidator();
    }

    [TestMethod]
    public async Task ShouldGetACustomerWithTwoOrders_WhenOneCustomerRecordWithTwoOrdersAreAdded()
    {
        var firstItem = Fixture.Build<CustomerRequest>()
            .With(x => x.Name, "Salih Igde")
            .With(x => x.Email, "salihigde@gmail.com")
            .Create();

        var customerResult = await Mediator.Send(firstItem);

        var firstOrderRequest = Fixture.Build<OrderRequest>()
            .With(x => x.CustomerId, customerResult.Id)
            .With(x => x.Price, 200)
            .Create();

        var secondOrderRequest = Fixture.Build<OrderRequest>()
            .With(x => x.CustomerId, customerResult.Id)
            .With(x => x.Price, 500)
            .Create();

        await Mediator.Send(firstOrderRequest);
        await Mediator.Send(secondOrderRequest);

        var result = await Mediator.Send(new CustomerWithOrdersRequest { CustomerId = customerResult.Id });

        result.Name.Should().Be("Salih Igde");
        result.Email.Should().Be("salihigde@gmail.com");
        result.Orders.Select(x => x.Price).ToList().Should().BeEquivalentTo(new List<decimal> { 200, 500 });
    }

    [TestMethod]
    public void ShouldThrowArgumentNullException_RequestIsNull()
    {
        CustomerWithOrdersRequest customerRequest = null;
        Action createCustomerAction = () => Mediator.Send(customerRequest);

        createCustomerAction.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void ShouldHaveErrorWhenCustomerIdIsEmpty() => _validator.TestValidate(new CustomerWithOrdersRequest() { CustomerId = Guid.Empty }).ShouldHaveValidationErrorFor(x => x.CustomerId);
}