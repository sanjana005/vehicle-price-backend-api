using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicle_price_backend_api.Data;
using vehicle_price_backend_api.Models;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<IActionResult> AddNewVehicle(List<IFormFile> images, VehicleDTO vehicleDTO)
        {
            try
            {
                if (images.Count > 4)
                {
                    return BadRequest("You can upload a maximum of 4 images.");
                }

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

                foreach (var image in images)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        vehicle.ImageData = memoryStream.ToArray();
                    }

                    await dbContext.SaveChangesAsync();
                }

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
                    ImageData = vehicle.ImageData
                }).ToList();

                return Ok(vehicleDTOs);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("UpdateVehicle/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, VehicleDTO updatedVehicleDTO, List<IFormFile> images)
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

                if (images != null && images.Any())
                {
                    foreach (var image in images)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await image.CopyToAsync(memoryStream);
                            vehicle.ImageData = memoryStream.ToArray();
                        }
                    }
                }

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
