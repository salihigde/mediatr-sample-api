using System;
using MediatrSampleApi.Controllers;
using MediatrSampleApi.Handlers.Command;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyTested.AspNetCore.Mvc;

namespace MediatrSampleApi.UnitTest.Controllers
{
    [TestClass]
    [Ignore("MyTested package currently doesn't support .net core 3.0")]
    public class OrderControllerTests
    {
        Mock<IMediator> mockMediator;

        [TestInitialize]
        public void Init()
        {
            mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        [Ignore("Ignored untill MyTested NuGet Package starts to support .Net Core 3.0")]
        public void CreateOrder_ShouldHaveHttPostAttribute()
        {
            MyController<OrderController>
               .Instance()
               .WithDependencies(mockMediator.Object)
               .Calling(c => c.CreateOrderAsync(new OrderRequest { CustomerId = Guid.NewGuid(), Price = 20 }))
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post));
        }

        [TestMethod]
        [Ignore("Ignored untill MyTested NuGet Package starts to support .Net Core 3.0")]
        public void CreateOrder_ShouldReturnValidationError_WhenPriceIsSmallerOrEqualToZero()
        {
            MyMvc
                .Controller<OrderController>()
                .WithDependencies(mockMediator.Object)
                .Calling(c => c.CreateOrderAsync(new OrderRequest
                {
                    CustomerId = Guid.NewGuid(),
                    Price = 0
                }))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<OrderRequest>()
                    .ContainingNoErrorFor(m => m.CustomerId)
                    .AndAlso()
                    .ContainingErrorFor(m => m.Price)
                    .ThatEquals("'Price' must be greater than '0'."));
        }

        [TestMethod]
        [Ignore("Ignored untill MyTested NuGet Package starts to support .Net Core 3.0")]
        public void CreateOrder_ShouldReturnValidationError_WhenCustomerIdIsEmpty()
        {
            MyMvc
                .Controller<OrderController>()
                .WithDependencies(mockMediator.Object)
                .Calling(c => c.CreateOrderAsync(new OrderRequest
                {
                    Price = 20
                }))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<OrderRequest>()
                    .ContainingNoErrorFor(m => m.Price)
                    .AndAlso()
                    .ContainingErrorFor(m => m.CustomerId)
                    .ThatEquals("'Customer Id' must not be empty."));
        }
    }
}
