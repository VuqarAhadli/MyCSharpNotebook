using System;

namespace HelloWorld
{
   
    enum CarMode { Eco, Normal, Sport }

    class Car
    {
         CarMode carmode;
         float fuelamount ;
         float fuelconsumpt;
         int modemultiple;

        public Car(CarMode c , float amount , float consumption)
        {
            carmode=c;
            fuelamount=amount;
            fuelconsumpt=consumption;
            
            switch (this.carmode)
            {
                case CarMode.Eco:
                    modemultiple = 1;
                    break;
                case CarMode.Normal:
                    modemultiple = 2;
                    break;
                default:
                    modemultiple = 3;
                    break;
            }
        }

        public void ChangeMode(CarMode mode)
        {
            this.carmode = mode;
             switch (this.carmode)
            {
                case CarMode.Eco:
                    modemultiple = 1;
                    break;
                case CarMode.Normal:
                    modemultiple = 2;
                    break;
                default:
                    modemultiple = 3;
                    break;
            }
        }

        public bool CanDrive(float distance)
        {
            float needed = (fuelconsumpt * modemultiple * distance) ;
            return needed <= fuelamount;
        }

        public void Refuel(float amount)
        {
            this.fuelamount += amount;
            Console.WriteLine($"fuel: {this.fuelamount}");
        }

        public void Drive(float distance)
        {
            if (CanDrive(distance))
            {
                this.fuelamount -= (distance * modemultiple * fuelconsumpt) ;
                Console.WriteLine($"driven: {distance}km remaining: {this.fuelamount}");
            }
            else
            {
                Console.WriteLine("not enough fuel");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Car carr = new Car(CarMode.Eco,49.7f,0.12f);
            Console.WriteLine($"500km: {carr.CanDrive(500f)}");
            carr.Refuel(10.5f);
            carr.Drive(22f);
        }
    }
}
