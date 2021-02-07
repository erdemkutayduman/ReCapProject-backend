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
            foreach (var car in carManager.GetCarsByBrandId(1))
            {
                Console.WriteLine($"{car.CarId}" +
                    $"\t{colorManager.GetById(car.ColorId).ColorName}" +
                    $"\t\t{brandManager.GetById(car.BrandId).BrandName}" +
                    $"\t\t{car.ModelYear}" +
                    $"\t\t{car.DailyPrice}" +
                    $"\t\t{orderManager.GetById(car.OrderId).OrderDate}" +
                    $"\t{car.Description}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");

            Console.WriteLine("\n\nColor Id 3 : ------------------------------------------------------------------------------------------------- " +
                "\nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tOrderDate\t\tDescription");
            foreach (var car in carManager.GetCarsByColorId(3))
            {
                Console.WriteLine($"{car.CarId}" +
                $"\t{colorManager.GetById(car.ColorId).ColorName}" +
                $"\t\t{brandManager.GetById(car.BrandId).BrandName}" +
                $"\t\t{car.ModelYear}" +
                $"\t\t{car.DailyPrice}" +
                $"\t\t{orderManager.GetById(car.OrderId).OrderDate}" +
                $"\t{car.Description}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");

            Console.WriteLine("\n\nOrder Id 2 : ------------------------------------------------------------------------------------------------- " +
                "\nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tOrderDate\t\tDescription");
            foreach (var car in carManager.GetCarsByOrderId(2))
            {
                Console.WriteLine($"{car.CarId}" +
                    $"\t{colorManager.GetById(car.ColorId).ColorName}" +
                    $"\t\t{brandManager.GetById(car.BrandId).BrandName}" +
                    $"\t\t{car.ModelYear}" +
                    $"\t\t{car.DailyPrice}" +
                    $"\t\t{orderManager.GetById(car.OrderId).OrderDate}" +
                    $"\t{car.Description}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");

            Console.WriteLine("\n\nDatabase All  : ---------------------------------------------------------------------------------------------- " +
                "\nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tOrderDate\t\tDescription");
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine($"{car.CarId}" +
                    $"\t{colorManager.GetById(car.ColorId).ColorName}" +
                    $"\t\t{brandManager.GetById(car.BrandId).BrandName}" +
                    $"\t\t{car.ModelYear}" +
                    $"\t\t{car.DailyPrice}" +
                    $"\t\t{orderManager.GetById(car.OrderId).OrderDate}" +
                    $"\t{car.Description}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");

        }
    }
}
