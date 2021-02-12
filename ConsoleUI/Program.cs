using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            OrderManager orderManager = new OrderManager(new EfOrderDal());

            Console.WriteLine("Brand Id 1 : ------------------------------------------------------------------------------------------------- " +
                "\nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tOrderDate\t\tDescription");
            var result1 = carManager.GetCarsByBrandId(1);

            if (result1.Success == true)
            {
                foreach (var car in result1.Data)
                {
                    Console.WriteLine($"{car.CarId}" +
                        $"\t{colorManager.GetById(car.ColorId).ColorName}" +
                        $"\t\t{brandManager.GetById(car.BrandId).BrandName}" +
                        $"\t\t{car.ModelYear}" +
                        $"\t\t{car.DailyPrice}" +
                        $"\t\t{orderManager.GetById(car.OrderId).OrderDate}" +
                        $"\t{car.Description}");
                }
            }
            else
            {
                Console.WriteLine(result1.Message);

            }
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");

            Console.WriteLine("\n\nColor Id 3 : ------------------------------------------------------------------------------------------------- " +
                "\nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tOrderDate\t\tDescription");
            var result2 = carManager.GetCarsByColorId(3);

            if (result2.Success == true)
            {
                foreach (var car in result2.Data)
                {
                    Console.WriteLine($"{car.CarId}" +
                        $"\t{colorManager.GetById(car.ColorId).ColorName}" +
                        $"\t\t{brandManager.GetById(car.BrandId).BrandName}" +
                        $"\t\t{car.ModelYear}" +
                        $"\t\t{car.DailyPrice}" +
                        $"\t\t{orderManager.GetById(car.OrderId).OrderDate}" +
                        $"\t{car.Description}");
                }
            }
            else
            {
                Console.WriteLine(result2.Message);

            }
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");

            Console.WriteLine("\n\nOrder Id 2 : ------------------------------------------------------------------------------------------------- " +
                "\nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tOrderDate\t\tDescription");
            var result3 = carManager.GetCarsByOrderId(2);

            if (result3.Success == true)
            {
                foreach (var car in result3.Data)
                {
                    Console.WriteLine($"{car.CarId}" +
                        $"\t{colorManager.GetById(car.ColorId).ColorName}" +
                        $"\t\t{brandManager.GetById(car.BrandId).BrandName}" +
                        $"\t\t{car.ModelYear}" +
                        $"\t\t{car.DailyPrice}" +
                        $"\t\t{orderManager.GetById(car.OrderId).OrderDate}" +
                        $"\t{car.Description}");
                }
            }
            else
            {
                Console.WriteLine(result3.Message);

            }
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");

            Console.WriteLine("\n\nDatabase All  : ---------------------------------------------------------------------------------------------- " +
                "\nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tOrderDate\t\tDescription");
            
            var result4 = carManager.GetCarsDetail();

            if (result4.Success == true)
            {
                foreach (var car in result4.Data)
                {
                    Console.WriteLine($"{car.CarId}" +
                        $"\t{colorManager.GetById(car.ColorId).ColorName}" +
                        $"\t\t{brandManager.GetById(car.BrandId).BrandName}" +
                        $"\t\t{car.ModelYear}" +
                        $"\t\t{car.DailyPrice}" +
                        $"\t\t{orderManager.GetById(car.OrderId).OrderDate}" +
                        $"\t{car.Description}");
                }
            }
            else
            {
                Console.WriteLine(result4.Message);

            }

                Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");

        }
    }
}
