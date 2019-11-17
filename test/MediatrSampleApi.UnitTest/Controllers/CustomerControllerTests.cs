using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatrSampleApi.Controllers;
using MediatrSampleApi.Handlers.Command;
using MediatrSampleApi.Handlers.Contracts;
using MediatrSampleApi.Handlers.Query;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyTested.AspNetCore.Mvc;
using MediatrSampleApi.Exceptions;

namespace MediatrSampleApi.UnitTest.Controllers
{
    [TestClass]
    public class CustomerControllerTests : TestBase
    {
        Mock<IMediator> mockMediator;
        CustomerController customerController;

        [TestInitialize]
        public void Init()
        {
            mockMediator = new Mock<IMediator>();
            customerController = new CustomerController(mockMediator.Object);
        }

        [TestMethod]
        public async Task GetCustomersAsync_ShouldReturnExpectedCustomers()
        {
            var customers = fixture.CreateMany<CustomerResponse>(2).ToList();

            mockMediator
                .Setup(m => m.Send(It.IsAny<CustomerListRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customers);

            var actionResult = await customerController.GetCustomersAsync();

            var result = actionResult as OkObjectResult;
            result.Value.Should().BeOfType<List<CustomerResponse>>();
            result.Value.Should().Be(customers);
        }

        [TestMethod]
        public async Task GetCustomersWithOrdersAsync_ShouldReturnExpectedCustomerWithOrders()
        {
            var customerWithOrders = fixture.Create<CustomerWithOrdersResponse>();

            mockMediator
                .Setup(m => m.Send(It.IsAny<CustomerWithOrdersRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customerWithOrders);

            var actionResult = await customerController.GetCustomerWithOrdersAsync(fixture.Create<Guid>());

            var result = actionResult as OkObjectResult;
            result.Value.Should().BeOfType<CustomerWithOrdersResponse>();
            result.Value.Should().Be(customerWithOrders);
        }

        [TestMethod]
        public void CreateCustomerAsync_ShouldThrowValidationException_WhenCustomerExists()
        {
            var customerRequest = fixture.Create<CustomerRequest>();

            mockMediator.Setup(m => m.Send(It.IsAny<DoesCustomerExistsRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            Func<Task> createCustomerAction = async () => await customerController.CreateCustomerAsync(customerRequest);

            createCustomerAction.Should().Throw<ValidationException>().WithMessage("Customer already exists");
        }

        [TestMethod]
        public async Task CreateCustomerAsync_ShouldAddCustomer_WhenCustomerDoesNotExists()
        {
            var customerRequest = fixture.Create<CustomerRequest>();
            var customerCreateResponse = fixture.Build<CustomerCreateResponse>()
                .With(x => x.Id, Guid.NewGuid())
                .Create();

            mockMediator.Setup(m => m.Send(It.IsAny<DoesCustomerExistsRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            mockMediator.Setup(m => m.Send(It.IsAny<CustomerRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customerCreateResponse);

            var actionResult = await customerController.CreateCustomerAsync(customerRequest);

            var result = actionResult as CreatedResult;

            result.Value.Should().BeOfType<CustomerCreateResponse>();
            result.Value.Should().Be(customerCreateResponse);
        }

        [TestMethod]
        [Ignore("Ignored untill MyTested NuGet Package starts to support .Net Core 3.0")]
        public void GetCustomersAsync_ShouldMapCorrectRoute()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/customer/list")
                .To<CustomerController>(x => x.GetCustomersAsync());
        }

        [TestMethod]
        [Ignore("Ignored untill MyTested NuGet Package starts to support .Net Core 3.0")]
        public void GetCustomersAsync_ShouldHaveHttpGetAttribute()
        {
            MyController<CustomerController>
               .Instance()
               .WithDependencies(mockMediator.Object)
               .Calling(c => c.GetCustomersAsync())
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Get));
        }

        [TestMethod]
        [Ignore("Ignored untill MyTested NuGet Package starts to support .Net Core 3.0")]
        public void GetCustomerWithOrdersAsync_ShouldMapCorrectRoute()
        {
            var customerId = fixture.Create<Guid>();
            MyRouting
                .Configuration()
                .ShouldMap($"api/customer/{customerId}/details")
                .To<CustomerController>(x => x.GetCustomerWithOrdersAsync(customerId));
        }

        [TestMethod]
        [Ignore("Ignored untill MyTested NuGet Package starts to support .Net Core 3.0")]
        public void GetCustomerWithOrdersAsync_ShouldHaveHttpGetAttribute()
        {
            MyController<CustomerController>
               .Instance()
               .WithDependencies(mockMediator.Object)
               .Calling(c => c.GetCustomerWithOrdersAsync(fixture.Create<Guid>()))
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Get));
        }

        [TestMethod]
        [Ignore("Ignored untill MyTested NuGet Package starts to support .Net Core 3.0")]
        public void CreateCustomerAsync_ShouldHaveHttpPostAttribute()
        {
            MyController<CustomerController>
               .Instance()
               .WithDependencies(mockMediator.Object)
               .Calling(c => c.CreateCustomerAsync(
                   new CustomerRequest
                   {
                        Name = "Salih",
                        Email = "salihigde@gmail.com"
                   }
               ))
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post));
        }

        [TestMethod]
        [Ignore("Ignored untill MyTested NuGet Package starts to support .Net Core 3.0")]
        public void CreateCustomerAsync_ShouldReturnValidationError_WhenNameIsEmpty()
        {
            MyMvc
                .Controller<CustomerController>()
                .WithDependencies(mockMediator.Object)
                .Calling(c => c.CreateCustomerAsync(new CustomerRequest
                {
                    Name = string.Empty,
                    Email = "salihigde@gmail.com"
                }))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<CustomerRequest>()
                    .ContainingNoErrorFor(m => m.Email)
                    .AndAlso()
                    .ContainingErrorFor(m => m.Name)
                    .ThatEquals("'Name' must not be empty."));
        }

        [TestMethod]
        [Ignore("Ignored untill MyTested NuGet Package starts to support .Net Core 3.0")]
        public void CreateCustomerAsync_ShouldReturnValidationError_WhenNameIsNull()
        {
            MyMvc
                .Controller<CustomerController>()
                .WithDependencies(mockMediator.Object)
                .Calling(c => c.CreateCustomerAsync(new CustomerRequest
                {
                    Email = "salihigde@gmail.com"
                }))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<CustomerRequest>()
                    .ContainingNoErrorFor(m => m.Email)
                    .AndAlso()
                    .ContainingErrorFor(m => m.Name)
                    .ThatEquals("'Name' must not be empty."));
        }

        [TestMethod]
        [Ignore("Ignored untill MyTested NuGet Package starts to support .Net Core 3.0")]
        public void CreateCustomerAsync_ShouldReturnValidationError_WhenEmailIsNull()
        {
            MyMvc
                .Controller<CustomerController>()
                .WithDependencies(mockMediator.Object)
                .Calling(c => c.CreateCustomerAsync(new CustomerRequest
                {
                    Name = "Salih"
                }))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<CustomerRequest>()
                    .ContainingNoErrorFor(m => m.Name)
                    .AndAlso()
                    .ContainingErrorFor(m => m.Email)
                    .ThatEquals("'Email' must not be empty."));
        }

        [TestMethod]
        [Ignore("Ignored untill MyTested NuGet Package starts to support .Net Core 3.0")]
        public void CreateCustomerAsync_ShouldReturnValidationError_WhenEmailIsNotValid()
        {
            MyMvc
                .Controller<CustomerController>()
                .WithDependencies(mockMediator.Object)
                .Calling(c => c.CreateCustomerAsync(new CustomerRequest
                {
                    Name = "Salih",
                    Email = "wrongemail"
                }))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<CustomerRequest>()
                    .ContainingNoErrorFor(m => m.Name)
                    .AndAlso()
                    .ContainingErrorFor(m => m.Email)
                    .ThatEquals("Please enter valid customer email"));
        }
    }
}
