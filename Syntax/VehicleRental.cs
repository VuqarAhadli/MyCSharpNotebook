using System;
class VehicleRentalApp
{

    public abstract class Vehicle
    {
        private string brand;
        private string model;
        private int year;
        private decimal pricePerDay;
        private bool isAvailable;
        private static int totalVehicles;
        private static decimal taxRate;

        public string Brand { get => brand; private set => brand = value; }
        public string Model { get => model; private set  => model = value; }
        public int Year { get => year; private set  => year = value; }
        public decimal PricePerDay { get => pricePerDay; private set  => pricePerDay = value; }
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
        public static int TotalVehicles => totalVehicles;
        public static decimal TaxRate { get => taxRate; set => taxRate = value; }

        public Vehicle( string brand, string model, int year, decimal pricePerDay, bool isAvailable)
        {
            Brand = brand;
            Model = model;
            Year = year;
            PricePerDay = pricePerDay;
            IsAvailable = isAvailable;
            Vehicle.totalVehicles++;
        }
        public abstract decimal CalculateRentalCost(int days);

        public abstract decimal CalculateRentalCost(int days, bool hasInsurance);

        public abstract string GetInfo();

        protected decimal ApplyTax(decimal rentalCost)
        {
            return rentalCost + (rentalCost * TaxRate);
        }

    }

    public class Car : Vehicle
    {
        private int numberOfSeats;
        private bool hasGPS;

        public bool HasGPS { get => hasGPS; private set  => hasGPS = value; }
        public int NumberOfSeats { get => numberOfSeats; private set  => numberOfSeats = value; }

        public Car (int numberOfSeats, bool hasGPS, string brand, string model, int year, decimal pricePerDay, bool isAvailable) : base(brand, model, year, pricePerDay, isAvailable)
        {
            NumberOfSeats = numberOfSeats;
            HasGPS = hasGPS;
        }
        public override decimal CalculateRentalCost(int days)
        {
            return ApplyTax(days * PricePerDay);
        }

        public override decimal CalculateRentalCost(int days, bool hasInsurance)
        {
            if (hasInsurance) return ApplyTax(days * PricePerDay + 15 * days);
            return CalculateRentalCost(days);
        }

        public override string GetInfo()
        {
            return $"INFO\nBrand: {this.Brand}\nModel: {this.Model}\nYear: {this.Year}\nPrice per day: {this.PricePerDay}\nNumber of seats: {this.NumberOfSeats}\nGps: {(this.HasGPS ? "Yes" : "No")}";
        }
    }

    public class Truck : Vehicle
    {
        private double loadCapacity;

        public double LoadCapacity { get => loadCapacity; private set  => loadCapacity = value; }

        public Truck (double loadCapacity, string brand, string model, int year, decimal pricePerDay, bool isAvailable) : base(brand, model, year, pricePerDay, isAvailable)
        {
            LoadCapacity = loadCapacity;
        }
        public override decimal CalculateRentalCost(int days)
        {
            return ApplyTax((PricePerDay + ((decimal)LoadCapacity * 5)) * days);
        }

        public override decimal CalculateRentalCost(int days, bool hasInsurance)
        {
            if (hasInsurance) return ApplyTax((PricePerDay + ((decimal)LoadCapacity * 5)) * days + 15 * days);
            return CalculateRentalCost(days);
        }

        public override string GetInfo()
        {
            return $"INFO\nBrand: {this.Brand}\nModel: {this.Model}\nYear: {this.Year}\nPrice per day: {this.PricePerDay}\nLoad capacity: {this.LoadCapacity}";
        }
    }

    public class Motorcycle : Vehicle
    {
        private int EngineCC;

        public int EngineCC1 { get => EngineCC; private set  => EngineCC = value; }


        public Motorcycle (int CC, string brand, string model, int year, decimal pricePerDay, bool isAvailable) : base(brand, model, year, pricePerDay, isAvailable)
        {
            EngineCC1 = CC;
        }
        public override decimal CalculateRentalCost(int days)
        {
            return ApplyTax((EngineCC1 > 600) ? (1.2m * PricePerDay * days) : PricePerDay * days);
        }

        public override decimal CalculateRentalCost(int days, bool hasInsurance)
        {
            if (hasInsurance) return ApplyTax((EngineCC1 > 600) ? (1.2m * PricePerDay * days) : PricePerDay * days + 15 * days);
            return CalculateRentalCost(days);
        }

        public override string GetInfo()
        {
            return $"INFO\nBrand: {Brand}\nModel: {Model}\nYear: {Year}\nPrice per day: {PricePerDay}\nEngine CC: {EngineCC1}";        }
        }

    public class Customer
    {
        private string name;
        private string email;
        private Vehicle currentRental;

        public string Name { get => name; private set  => name = value; }
        public string Email { get => email; private set  => email = value; }
        internal Vehicle CurrentRental { get => currentRental; set => currentRental = value; }

        public Customer (string name, string email)
        {
            this.Name = name;
            this.Email = email;
            this.CurrentRental = null;
        }

        public bool Rent(Vehicle v)
        {
            if (!v.IsAvailable || CurrentRental != null)
            {
                return false;
            }
            v.IsAvailable = false;
            CurrentRental = v;
            Console.WriteLine($"Vehicle {v.Brand} {v.Model} {v.Year} is rented.");
            return true;
        }


        public bool Return()
        {
            if (CurrentRental != null)
            {
                Console.WriteLine($"Vehicle {CurrentRental.Brand} {CurrentRental.Model} {CurrentRental.Year} is returned.");
                currentRental.IsAvailable = true;
                CurrentRental = null;
                return true;
            }

            return false;
        }

    }

    public interface IPayment
    {
        bool ProcessPayment(decimal amount);
        string GetPaymentInfo();
    }

    public class CashPayment : IPayment
    {
        public bool ProcessPayment(decimal amount)
        {
            return true;
        }

        public string GetPaymentInfo()
        {
            return "Cash";
        }
    }

    public class CardPayment : IPayment
    {
        private string cardNumber;

        public CardPayment(string cardNumber)
        {
            this.cardNumber = cardNumber;
        }

        public bool ProcessPayment(decimal amount)
        {
            return true;
        }

        public string GetPaymentInfo()
        {
            return $"Card: {cardNumber}";
        }
    }

    static void Main()
    {
        Vehicle.TaxRate = 0.18m; 

        Car car1 = new Car(5, true, "Toyota", "RAV4", 2021, 50m,  true);
        Car car2 = new Car(2, false, "Honda", "Civic", 2024, 300m, true);
        Truck truck1 = new Truck(10.5, "Ford", "F-150", 2025, 120m, true);
        Truck truck2 = new Truck(5.0, "Nissan", "Frontier", 2025, 150m, true);
        Motorcycle moto1 = new Motorcycle(400, "Honda",  "CB400", 2022, 30m, true);
        Motorcycle moto2 = new Motorcycle(1000, "BMW", "M 1000 R",  2023, 80m, true);

        Console.WriteLine($"Total vehicles registered: {Vehicle.TotalVehicles}\n");

        Console.WriteLine("Vehicle Info");
        Console.WriteLine(car1.GetInfo());
        Console.Write("\n");
        Console.WriteLine(car2.GetInfo());
        Console.Write("\n");
        Console.WriteLine(truck1.GetInfo());
        Console.Write("\n");
        Console.WriteLine(truck2.GetInfo());
        Console.Write("\n");
        Console.WriteLine(moto1.GetInfo());
        Console.Write("\n");
        Console.WriteLine(moto2.GetInfo());
        Console.Write("\n");


        Console.WriteLine("Customers");
        Customer Ali = new Customer("Ali", "ali222@proton.com");
        Customer Lale = new Customer("Lale", "lale123@email.com");
        Console.WriteLine($"Customer: {Ali.Name} ({Ali.Email})");
        Console.WriteLine($"Customer: {Lale.Name} ({Lale.Email})\n");


        Ali.Rent(car1);   
        Lale.Rent(truck1);    
        Ali.Rent(car2);    
        Lale.Rent(truck1);    
        Console.WriteLine();

        IPayment AliPayment = new CardPayment("4169-7388-0001-1122");
        IPayment LalePayment = new CashPayment();

        decimal AliCost = car1.CalculateRentalCost(5, true);
        decimal LaleCost = truck1.CalculateRentalCost(3);

        Console.WriteLine($"{Ali.Name} pays {AliCost:C} via {AliPayment.GetPaymentInfo()} - Success: {AliPayment.ProcessPayment(AliCost)}");
        Console.WriteLine($"{Lale.Name} pays {LaleCost:C} via {LalePayment.GetPaymentInfo()} - Success: {LalePayment.ProcessPayment(LaleCost)}");
        Console.WriteLine();


        Ali.Return();
        Lale.Return();
        Ali.Return(); 
        Console.WriteLine();

        Console.WriteLine($"{car1.Brand} {car1.Model} available: {car1.IsAvailable}");
        Console.WriteLine($"{truck1.Brand} {truck1.Model} available: {truck1.IsAvailable}");
    }
}
