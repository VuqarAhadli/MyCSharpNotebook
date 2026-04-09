using System;
class VehicleRentalApp
{

    public abstract class Vehicle
    {
        private string brand;
        private string model;
        private int year;
        private int pricePerDay;
        private bool isAvailable;
        private static int totalVehicles;
        private static int taxRate;

        public string Brand { get => brand; set => brand = value; }
        public string Model { get => model; set => model = value; }
        public int Year { get => year; set => year = value; }
        public int PricePerDay { get => pricePerDay; set => pricePerDay = value; }
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
        public static int TotalVehicles { get => totalVehicles; set => totalVehicles = value; }
        public static int TaxRate { get => taxRate; set => taxRate = value; }

        public Vehicle()
        {
            Vehicle.TotalVehicles++;
        }
        public abstract int CalculateRentalCost(int days);

        public abstract int CalculateRentalCost(int days, bool hasInsurance);

        public abstract string GetInfo();

    }

    public class Car : Vehicle
    {
        private int numberOfSeats;
        private bool hasGPS;

        public bool HasGPS { get => hasGPS; set => hasGPS = value; }
        public int NumberOfSeats { get => numberOfSeats; set => numberOfSeats = value; }
    }


    static void Main()
    {
        Console.WriteLine("Hello World");
    }
}
