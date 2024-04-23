using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicle_price_backend_api.Data;
using vehicle_price_backend_api.Models;
//using static System.Net.Mime.MediaTypeNames;

namespace vehicle_price_backend_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly VehicleAPIDbCon dbContext;

        public VehicleController(VehicleAPIDbCon dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("AddNewVehicle")]
        public async Task<IActionResult> AddNewVehicle(VehicleDTO vehicleDTO)
        {
            try
            {

                var vehicle = new Vehicle()
                {
                    Brand = vehicleDTO.Brand,
                    Model = vehicleDTO.Model,
                    VehicleType = vehicleDTO.VehicleType,
                    Location = vehicleDTO.Location,
                    Mileage = vehicleDTO.Mileage,
                    PostedDate = vehicleDTO.PostedDate,
                    CurrencyRate = vehicleDTO.CurrencyRate,
                    ManufacturedYear = vehicleDTO.ManufacturedYear,
                    FuelType = vehicleDTO.FuelType,
                    Transmission = vehicleDTO.Transmission,
                    Price = vehicleDTO.Price
                };

                await dbContext.Vehicles.AddAsync(vehicle);
                await dbContext.SaveChangesAsync();

                return Ok("Vehicle added successfully.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet("GetVehicles")]
        public async Task<IActionResult> GetVehicles()
        {
            try
            {
                var vehicles = await dbContext.Vehicles.ToListAsync();

                var vehicleDTOs = vehicles.Select(vehicle => new VehicleDTO
                {
                    Id = vehicle.Id,
                    Brand = vehicle.Brand,
                    Model = vehicle.Model,
                    VehicleType = vehicle.VehicleType,
                    Location = vehicle.Location,
                    Mileage = vehicle.Mileage,
                    PostedDate = vehicle.PostedDate,
                    CurrencyRate = vehicle.CurrencyRate,
                    ManufacturedYear = vehicle.ManufacturedYear,
                    FuelType = vehicle.FuelType,
                    Transmission = vehicle.Transmission,
                    Price = vehicle.Price,
                }).ToList();

                return Ok(vehicleDTOs);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("UpdateVehicle/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, VehicleDTO updatedVehicleDTO)
        {
            try
            {
                var vehicle = await dbContext.Vehicles.FindAsync(id);

                if (vehicle == null)
                {
                    return NotFound();
                }

                vehicle.Brand = updatedVehicleDTO.Brand;
                vehicle.Model = updatedVehicleDTO.Model;
                vehicle.VehicleType = updatedVehicleDTO.VehicleType;
                vehicle.Location = updatedVehicleDTO.Location;
                vehicle.Mileage = updatedVehicleDTO.Mileage;
                vehicle.PostedDate = updatedVehicleDTO.PostedDate;
                vehicle.CurrencyRate = updatedVehicleDTO.CurrencyRate;
                vehicle.ManufacturedYear = updatedVehicleDTO.ManufacturedYear;
                vehicle.FuelType = updatedVehicleDTO.FuelType;
                vehicle.Transmission = updatedVehicleDTO.Transmission;
                vehicle.Price = updatedVehicleDTO.Price;

                dbContext.Vehicles.Update(vehicle);
                await dbContext.SaveChangesAsync();

                return Ok("Vehicle details updated successfully.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteVehicle/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            try
            {
                var vehicle = await dbContext.Vehicles.FindAsync(id);

                if (vehicle == null)
                {
                    return NotFound(); 
                }

                dbContext.Vehicles.Remove(vehicle);
                await dbContext.SaveChangesAsync(); 

                return Ok("Vehicle deleted successfully.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
