using System;
using System.Threading.Tasks;
using MediatrSampleApi.Handlers.Command;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediatrSampleApi.UnitTest.Handlers
{
    [TestClass]
    public class CreateCustomerTests : TestBase
    {
        private CustomerRequestValidator validator;
        private string longCustomerName;
        private string longEmail;

        [TestInitialize]
        public void Init()
        {
            validator = new CustomerRequestValidator();
            longCustomerName = string.Join(string.Empty, fixture.CreateMany<char>(81));
            longEmail = string.Join(string.Empty, fixture.CreateMany<char>(121));
        }

        [TestMethod]
        public async Task ShouldCreateCustomerSuccessfully_WhenCustomerRequestIsValid()
        {
            var request = fixture.Build<CustomerRequest>()
               .With(x => x.Name, "Salih Igde")
               .With(x => x.Email, "salihigde@gmail.com")
               .Create();

            var result = await mediator.Send(request);

            result.Should().NotBeNull();
        }

        [TestMethod]
        public void ShouldThrowArgumentNullException_RequestIsNull()
        {
            CustomerRequest customerRequest = null;
            Action createCustomerAction = () => mediator.Send(customerRequest);

            createCustomerAction.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ShouldHaveErrorWhenNameIsNull() => validator.ShouldHaveValidationErrorFor(x => x.Name, null as string);

        [TestMethod]
        public void ShouldHaveErrorWhenNameIsEmpty() => validator.ShouldHaveValidationErrorFor(x => x.Name, "");

        [TestMethod]
        public void ShouldNotHaveErrorWhenNameIsValid() => validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Salih Igde");

        [TestMethod]
        public void ShouldHaveErrorWhenNameExceedsMaxLength() => validator.ShouldHaveValidationErrorFor(x => x.Name, longCustomerName);

        [TestMethod]
        public void ShouldHaveErrorWhenEmailIsNull() => validator.ShouldHaveValidationErrorFor(x => x.Email, null as string);

        [TestMethod]
        public void ShouldHaveErrorWhenEmailIsEmpty() => validator.ShouldHaveValidationErrorFor(x => x.Email, "");

        [TestMethod]
        public void ShouldHaveErrorWhenEmailExceedsMaxLength() => validator.ShouldHaveValidationErrorFor(x => x.Name, longEmail);

        [TestMethod]
        public void ShouldNotHaveErrorWhenEmailIsValid() => validator.ShouldNotHaveValidationErrorFor(x => x.Email, "salihigde@gmail.com");
    }
}
