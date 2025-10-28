using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services;
using CustomerCRM.Domain.Services.Warehouse;
using CustomerCRM.Domain.Services.Warehouse.Abstract;
using CustomerCRM.Domain.Services.Warehouse.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;


namespace CustomerCRM.Tests
{
    public class WarehouseServiceTests
    {
        [Fact]
        public void AddProduct_AddsProductToList()
        {
            // Arrange
            var service = new WarehouseService();
            var product = new Mock<ProductBase>();

            // Act
            service.AddProduct(product.Object);

            // Assert
            Assert.Contains(product.Object, service.Products);
        }

        [Fact]
        public void RemoveProduct_RemovesProductFromList()
        {
            // Arrange
            var service = new WarehouseService();
            var product = new Mock<ProductBase>();
            service.AddProduct(product.Object);

            // Act
            service.RemoveProduct(product.Object.Id);

            // Assert
            Assert.DoesNotContain(product.Object, service.Products);
        }

        [Fact]
        public void ModifyProductQuantity_UpdatesProductQuantity()
        {
            // Arrange
            var service = new WarehouseService();
            var product = new Mock<ProductBase>();
            service.AddProduct(product.Object);
            var newQuantity = 10;

            // Act
            service.ModifyProductQuantity(product.Object.Id, newQuantity);

            // Assert
            Assert.Equal(newQuantity, product.Object.Quantity);
        }

        [Fact]
        public void ModifyProductPrice_UpdatesProductPrice()
        {
            // Arrange
            var service = new WarehouseService();
            var product = new Mock<ProductBase>();
            service.AddProduct(product.Object);
            var newPrice = 19.99m;

            // Act
            service.ModifyProductPrice(product.Object.Id, newPrice);

            // Assert
            Assert.Equal(newPrice, product.Object.Price);
        }

        [Fact]
        public void GetProductsByName_ReturnsMatchingProducts()
        {
            // Arrange
            var service = new WarehouseService();
            var product1 = new FruitProduct { Name = "Product1" };
            var product2 = new FruitProduct { Name = "Product2" };
            service.AddProduct(product1);
            service.AddProduct(product2);

            // Act
            var result = service.GetProductsByName("Product1");

            // Assert
            Assert.Single(result);
            Assert.Contains(product1, result);
        }


        [Fact]
        public void GetProductsByCategory_ReturnsMatchingProducts()
        {
            // Arrange
            var service = new WarehouseService();
            var product1 = new Mock<IProduct>();
            var product2 = new Mock<IProduct>();
            product1.Setup(p => p.Category).Returns("Category1");
            product2.Setup(p => p.Category).Returns("Category2");
            service.AddProduct(product1.Object);
            service.AddProduct(product2.Object);

            // Act
            var result = service.GetProductsByCategory("Category1");

            // Assert
            Assert.Single(result);
            Assert.Contains(product1.Object, result);
        }


        [Fact]
        public void GetProductById_ReturnsProductById()
        {
            // Arrange
            var service = new WarehouseService();
            var product = new Mock<ProductBase>();
            service.AddProduct(product.Object);

            // Act
            var result = service.GetProductById(product.Object.Id);

            // Assert
            Assert.Equal(product.Object, result);
        }
    }
}