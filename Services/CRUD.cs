using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Week10Assignment10._3.Data;
using Week10Assignment10._3.Models;

namespace Week10Assignment10._3.Services
{
    public class CRUD
    {
        // READ: Get all cars
        public List<Car> GetAllCars()
        {
            return Records.context.Cars.ToList();
        }

        // CREATE: Add a new car
        public void AddCar(Car car)
        {
            Records.context.Cars.Add(car);
            Records.context.SaveChanges();
        }

        // UPDATE: Update an existing car
        public void UpdateCar(Car car)
        {
            var existingCar = Records.context.Cars.Find(car.Id);

            if (existingCar == null)
                throw new KeyNotFoundException("Car not found.");

            existingCar.VIN = car.VIN;
            existingCar.Make = car.Make;
            existingCar.Model = car.Model;
            existingCar.Year = car.Year;
            existingCar.Price = car.Price;
            existingCar.Color = car.Color;

            Records.context.SaveChanges();
        }

        // DELETE: Delete a car
        public void DeleteCar(int id)
        {
            var car = Records.context.Cars.Find(id);

            if (car != null)
            {
                Records.context.Cars.Remove(car);
                Records.context.SaveChanges();
            }
        }
    }
}
