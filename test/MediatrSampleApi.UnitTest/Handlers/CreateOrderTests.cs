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
    public class CreateOrderTests : TestBase
    {
        private OrderRequestValidator validator;

        [TestInitialize]
        public void Init()
        {
            validator = new OrderRequestValidator();
        }

        [TestMethod]
        public async Task ShouldCreateOrderSuccessfully_WhenCustomerRequestIsValid()
        {
            var customerRequest = fixture.Build<CustomerRequest>()
               .With(x => x.Name, "Salih Igde")
               .With(x => x.Email, "salihigde@gmail.com")
               .Create();

            var customerResult = await mediator.Send(customerRequest);

            var orderRequest = fixture.Build<OrderRequest>()
                .With(x => x.CustomerId, customerResult.Id)
                .With(x => x.Price, 200)
                .Create();

            var result = await mediator.Send(orderRequest);

            result.Should().NotBeNull();
        }

        [TestMethod]
        public void ShouldThrowArgumentNullException_RequestIsNull()
        {
            OrderRequest customerRequest = null;
            Action createCustomerAction = () => mediator.Send(customerRequest);

            createCustomerAction.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ShouldHaveErrorWhenPriceIsNegative() => validator.ShouldHaveValidationErrorFor(x => x.Price, -20);

        [TestMethod]
        public void ShouldHaveErrorWhenPriceIsZero() => validator.ShouldHaveValidationErrorFor(x => x.Price, 0);

        [TestMethod]
        public void ShouldNotHaveErrorWhenPriceIsGreaterThanZero() => validator.ShouldNotHaveValidationErrorFor(x => x.Price, 50);

        [TestMethod]
        public void ShouldHaveErrorWhenCustomerIdIsEmpty() => validator.ShouldHaveValidationErrorFor(x => x.CustomerId, Guid.Empty);

        [TestMethod]
        public void ShouldNotHaveErrorWhenCustomerIdIsValid() => validator.ShouldNotHaveValidationErrorFor(x => x.CustomerId, Guid.NewGuid());
    }
}
