using CatalogService.Domain.Common;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.UnitTests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace CatalogService.Domain.UnitTests.Entities
{
    public class ProductTests
    {
        [Test]
        public void CreateProduct_Success()
        {
            var product = new Product();

            product.Should().NotBeNull();
            product.DomainEvents.Should().NotBeNull();
            product.DomainEvents.Should().BeEmpty();
        }

        [Test]
        public void AddEvent_ShouldBeAdded()
        {
            var @event = new TestEvent();
            var product = new Product();
            
            product.AddDomainEvent(@event);
            
            product.DomainEvents.Should().Contain(@event);
        }
        
        [Test]
        public void RemoveEvent_ShouldBeRemoved()
        {
            var objs = PrepareProductWithEvents();

            objs.product.RemoveDomainEvent(objs.@event);
            
            objs.product.DomainEvents.Count.Should().Be(1);
            objs.product.DomainEvents.Should().Contain(objs.event2);
        }
        
        [Test]
        public void ClearEvents_ShouldBeEmpty()
        {
            var objs = PrepareProductWithEvents();

            objs.product.ClearDomainEvents();
            
            objs.product.DomainEvents.Should().BeEmpty();
        }

        [Test]
        public void AddAmount_Success()
        {
            var product = new Product();
            
            product.AddAmount(1);
            
            product.Amount.Should().Be(1);
        }
        
        [Test]
        public void RemoveAmount_Success()
        {
            var product = new Product(){Amount = 2};
            
            product.RemoveAmount(1);
            
            product.Amount.Should().Be(1);
        }
        
        [Test]
        public void RemoveAmount_ThrowExceptionZeroAmount()
        {
            var product = new Product();
            
            Action act = () => product.RemoveAmount(1);

            act.Should().Throw<CatalogException>();
        }
        
        [Test]
        public void RemoveAmount_ThrowExceptionAmountLessRemoving()
        {
            var product = new Product() {Amount = 2};
            
            Action act = () => product.RemoveAmount(3);

            act.Should().Throw<CatalogException>();
        }
        
        [Test]
        public void CheckAmount_ShouldBeEnough()
        {
            var product = new Product() {Amount = 2};
            
            var result = product.IsEnoughAmount(2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void CheckAmount_ShouldBeNotEnough()
        {
            var product = new Product() {Amount = 2};
            
            var result = product.IsEnoughAmount(3);

            result.Should().BeFalse();
        }
        
        private (TestEvent @event, TestEvent @event2, Product product) PrepareProductWithEvents()
        {
            var @event = new TestEvent();
            var @event2 = new TestEvent();
            var product = new Product();
            product.AddDomainEvent(@event);
            product.AddDomainEvent(@event2);
            return (@event, @event2, product);
        }
    }
}
