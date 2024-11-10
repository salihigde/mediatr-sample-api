using System;
using System.Threading.Tasks;
using MediatrSample.Api.Handlers.Command;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediatrSample.Api.UnitTests.Handlers;

[TestClass]
public class CreateCustomerTests : TestBase
{
    private CustomerRequestValidator _validator;
    private string _longCustomerName;
    private string _longEmail;

    [TestInitialize]
    public void Init()
    {
        _validator = new CustomerRequestValidator();
        _longCustomerName = string.Join(string.Empty, Fixture.CreateMany<char>(81));
        _longEmail = string.Join(string.Empty, Fixture.CreateMany<char>(121));
    }

    [TestMethod]
    public async Task ShouldCreateCustomerSuccessfully_WhenCustomerRequestIsValid()
    {
        var request = Fixture.Build<CustomerRequest>()
            .With(x => x.Name, "Salih Igde")
            .With(x => x.Email, "salihigde@gmail.com")
            .Create();

        var result = await Mediator.Send(request);

        result.Should().NotBeNull();
    }

    [TestMethod]
    public void ShouldThrowArgumentNullException_RequestIsNull()
    {
        CustomerRequest customerRequest = null;
        Action createCustomerAction = () => Mediator.Send(customerRequest);

        createCustomerAction.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void ShouldHaveErrorWhenNameIsNull() => _validator.TestValidate(new CustomerRequest() { Name = null })
        .ShouldHaveValidationErrorFor(x => x.Name);

    [TestMethod]
    public void ShouldHaveErrorWhenNameIsEmpty() => _validator.TestValidate(new CustomerRequest() { Name = "" })
        .ShouldHaveValidationErrorFor(x => x.Name);

    [TestMethod]
    public void ShouldNotHaveErrorWhenNameIsValid() => _validator
        .TestValidate(new CustomerRequest() { Name = "Salih Igde" }).ShouldNotHaveValidationErrorFor(x => x.Name);

    [TestMethod]
    public void ShouldHaveErrorWhenNameExceedsMaxLength() => _validator
        .TestValidate(new CustomerRequest() { Name = _longCustomerName }).ShouldHaveValidationErrorFor(x => x.Name);

    [TestMethod]
    public void ShouldHaveErrorWhenEmailIsNull() => _validator.TestValidate(new CustomerRequest() { Email = null })
        .ShouldHaveValidationErrorFor(x => x.Email);

    [TestMethod]
    public void ShouldHaveErrorWhenEmailIsEmpty() => _validator.TestValidate(new CustomerRequest() { Email = "" })
        .ShouldHaveValidationErrorFor(x => x.Email);

    [TestMethod]
    public void ShouldHaveErrorWhenEmailExceedsMaxLength() => _validator
        .TestValidate(new CustomerRequest() { Email = _longEmail }).ShouldHaveValidationErrorFor(x => x.Name);

    [TestMethod]
    public void ShouldNotHaveErrorWhenEmailIsValid() => _validator
        .TestValidate(new CustomerRequest() { Email = "salihigde@gmail.com" })
        .ShouldNotHaveValidationErrorFor(x => x.Email);
}