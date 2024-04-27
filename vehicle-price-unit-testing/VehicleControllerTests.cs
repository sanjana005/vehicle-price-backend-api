using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using vehicle_price_backend_api.Controllers;
using vehicle_price_backend_api.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using vehicle_price_backend_api.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace vehicle_price_unit_testing
{
    [TestFixture]
    public class VehicleControllerTests
    {
        private VehicleController _controller;
        private VehicleAPIDbCon _dbContext;

        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<VehicleAPIDbCon>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new VehicleAPIDbCon(dbContextOptions);
            AddTestVehiclesToDatabase();
            _controller = new VehicleController(_dbContext);

        }


        [Test]
        public async Task AddNewVehicle_ValidVehicle_ReturnsOkResult()
        {
            var vehicleDTO = new VehicleDTO
            {
                Brand = "Toyota",
                Model = "Corolla",
                VehicleType = "Car",
                Location = "Colombo",
                Mileage = 100000,
                PostedDate = DateTime.Parse("2023-06-28T05:50:59.509Z".ToString()),
                CurrencyRate = 365,
                ManufacturedYear = 2022,
                FuelType = "Petrol",
                Transmission = "Automatic",
                Price = 100000000
                
            };

            var result = await _controller.AddNewVehicle(vehicleDTO);


            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task UpdateVehicle_ValidIdAndDTO_ReturnsOkResult()
        {
            var newVehicleId = 1; 
            var updatedVehicleDTO = new VehicleDTO
            {
                Brand = "Nissan",
                Model = "Blueird",
                VehicleType = "Car",
                Location = "Colombo",
                Mileage = 100000,
                PostedDate = DateTime.Parse("2023-06-28T05:50:59.509Z".ToString()),
                CurrencyRate = 365,
                ManufacturedYear = 2022,
                FuelType = "Petrol",
                Transmission = "Automatic",
                Price = 100000000
            };

            var result = await _controller.UpdateVehicle(newVehicleId, updatedVehicleDTO);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task DeleteVehicle_ValidId_ReturnsOkResult()
        {
            var newVehicleId = 1;

            var result = await _controller.DeleteVehicle(newVehicleId);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetVehicles_ReturnsOkResultWithVehicleDTOs()
        {
            await AddTestVehiclesToDatabase();

            var result = await _controller.GetVehicles();


            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf<List<VehicleDTO>>(okResult.Value);
            var vehicles = okResult.Value as List<VehicleDTO>;
            Assert.IsNotNull(vehicles);
            Assert.Greater(vehicles.Count, 0);
        }

        private async Task AddTestVehiclesToDatabase()
        {
            var testVehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    Brand = "Honda",
                    Model = "Civic",
                    VehicleType = "Car",
                    Location = "Colombo",
                    Mileage = 100000,
                    PostedDate = DateTime.Parse("2023-06-28T05:50:59.509Z".ToString()),
                    CurrencyRate = 365,
                    ManufacturedYear = 2022,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Price = 100000000
                    
                },
                new Vehicle
                {
                    Brand = "Toyota",
                    Model = "Corolla",
                    VehicleType = "Car",
                    Location = "Colombo",
                    Mileage = 100000,
                    PostedDate = DateTime.Parse("2023-06-28T05:50:59.509Z".ToString()),
                    CurrencyRate = 365,
                    ManufacturedYear = 2022,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Price = 100000000
                    
                },

            };

            foreach (var vehicle in testVehicles)
            {
                await _dbContext.Vehicles.AddAsync(vehicle);
            }

            await _dbContext.SaveChangesAsync();
        }

    }

}
