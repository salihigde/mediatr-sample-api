using System;
using System.Threading.Tasks;
using MediatrSample.Api.Handlers.Command;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediatrSample.Api.UnitTests.Handlers;

[TestClass]
public class CreateOrderTests : TestBase
{
    private OrderRequestValidator _validator;

    [TestInitialize]
    public void Init()
    {
        _validator = new OrderRequestValidator();
    }

    [TestMethod]
    public async Task ShouldCreateOrderSuccessfully_WhenCustomerRequestIsValid()
    {
        var customerRequest = Fixture.Build<CustomerRequest>()
            .With(x => x.Name, "Salih Igde")
            .With(x => x.Email, "salihigde@gmail.com")
            .Create();

        var customerResult = await Mediator.Send(customerRequest);

        var orderRequest = Fixture.Build<OrderRequest>()
            .With(x => x.CustomerId, customerResult.Id)
            .With(x => x.Price, 200)
            .Create();

        var result = await Mediator.Send(orderRequest);

        result.Should().NotBeNull();
    }

    [TestMethod]
    public void ShouldThrowArgumentNullException_RequestIsNull()
    {
        OrderRequest customerRequest = null;
        Action createCustomerAction = () => Mediator.Send(customerRequest);

        createCustomerAction.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void ShouldHaveErrorWhenPriceIsNegative() => _validator.TestValidate(new OrderRequest() { Price = -20 })
        .ShouldHaveValidationErrorFor(x => x.Price);

    [TestMethod]
    public void ShouldHaveErrorWhenPriceIsZero() => _validator.TestValidate(new OrderRequest() { Price = 0 })
        .ShouldHaveValidationErrorFor(x => x.Price);

    [TestMethod]
    public void ShouldNotHaveErrorWhenPriceIsGreaterThanZero() => _validator
        .TestValidate(new OrderRequest() { Price = 50 }).ShouldNotHaveValidationErrorFor(x => x.Price);

    [TestMethod]
    public void ShouldHaveErrorWhenCustomerIdIsEmpty() => _validator
        .TestValidate(new OrderRequest() { CustomerId = Guid.Empty }).ShouldHaveValidationErrorFor(x => x.CustomerId);

    [TestMethod]
    public void ShouldNotHaveErrorWhenCustomerIdIsValid() => _validator
        .TestValidate(new OrderRequest() { CustomerId = Guid.NewGuid() })
        .ShouldNotHaveValidationErrorFor(x => x.CustomerId);
}