using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatrSampleApi.Handlers.Command;
using MediatrSampleApi.Handlers.Query;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediatrSampleApi.UnitTest.Handlers
{
    [TestClass]
    public class GetCustomerWithOrdersTests: TestBase
    {
        private CustomerWithOrdersRequestValidator validator;

        [TestInitialize]
        public void Init()
        {
            validator = new CustomerWithOrdersRequestValidator();
        }

        [TestMethod]
        public async Task ShouldGetACustomerWithTwoOrders_WhenOneCustomerRecordWithTwoOrdersAreAdded()
        {
            var firstItem = fixture.Build<CustomerRequest>()
               .With(x => x.Name, "Salih Igde")
               .With(x => x.Email, "salihigde@gmail.com")
               .Create();

            var customerResult = await mediator.Send(firstItem);

            var firstOrderRequest = fixture.Build<OrderRequest>()
                .With(x => x.CustomerId, customerResult.Id)
                .With(x => x.Price, 200)
                .Create();

            var secondOrderRequest = fixture.Build<OrderRequest>()
                .With(x => x.CustomerId, customerResult.Id)
                .With(x => x.Price, 500)
                .Create();

            await mediator.Send(firstOrderRequest);
            await mediator.Send(secondOrderRequest);

            var result = await mediator.Send(new CustomerWithOrdersRequest { CustomerId = customerResult.Id });

            result.Name.Should().Be("Salih Igde");
            result.Email.Should().Be("salihigde@gmail.com");
            result.Orders.Select(x => x.Price).ToList().Should().BeEquivalentTo(new List<decimal> { 200, 500 });
        }

        [TestMethod]
        public void ShouldThrowArgumentNullException_RequestIsNull()
        {
            CustomerWithOrdersRequest customerRequest = null;
            Action createCustomerAction = () => mediator.Send(customerRequest);

            createCustomerAction.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ShouldHaveErrorWhenCustomerIdIsEmpty() => validator.ShouldHaveValidationErrorFor(x => x.CustomerId, Guid.Empty);
    }
}
