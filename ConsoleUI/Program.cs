using Business.Concrete;
using Business.Constants;
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
            UserManager userManager = new UserManager(new EfUserDal());
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());

            bool cikis = true;

            while (cikis)
            {
                Console.WriteLine(
                    "Rent A Car \n--------------------------------------------------------------------------------------------------------------" +
                    "\n\n1.Add Car\n" +
                    "2.Delete Car\n" +
                    "3.Update Car\n" +
                    "4.List of Car\n" +
                    "5.Detailed List Of Cars\n" +
                    "6.Cars for Brands\n" +
                    "7.Cars for Colors\n" +
                    "8.Cars for Id\n" +
                    "9.Cars for Prices\n" +
                    "10.Cars for Model Years\n" +
                    "11.Add Customer\n" +
                    "12.List of Customers\n" +
                    "13.Add User\n" +
                    "14.List of Users\n" +
                    "15.RENT A CAR\n" +
                    "16.Return a Car\n" +
                    "17.List of Rental Cars\n" +
                    "18.Exit\n\n" +
                    "Choose A Menu...\n" +
                    "-------------------------------------------------------------------------------------------------------------- ");

                int number = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n--------------------------------------------------------------------------------------------------------------\n");
                switch (number)
                {
                    case 1:
                        CarAddition(carManager, brandManager, colorManager);
                        break;
                    case 2:
                        GetAllCarDetails(carManager);
                        CarDeletion(carManager);
                        break;
                    case 3:
                        GetAllCarDetails(carManager);
                        CarUpdate(carManager);
                        break;
                    case 4:
                        GetAllCar(carManager);
                        break;
                    case 5:
                        GetAllCarDetails(carManager);
                        break;
                    case 6:
                        GetAllBrand(brandManager);
                        CarListByBrand(carManager);
                        break;
                    case 7:
                        GetAllColor(colorManager);
                        CarListByColor(carManager);
                        break;
                    case 8:
                        GetAllCarDetails(carManager);
                        CarById(carManager, brandManager, colorManager);
                        break;
                    case 9:
                        CarByDailyPrice(carManager, brandManager, colorManager);
                        break;
                    case 10:
                        GetAllCarDetails(carManager);
                        CarByModelYear(carManager, brandManager, colorManager);
                        break;
                    case 11:
                        GetAllUserList(userManager);
                        CustomerAddition(customerManager);
                        break;
                    case 12:
                        GetAllCustomerList(customerManager);
                        break;
                    case 13:
                        UserAddition(userManager);
                        break;
                    case 14:
                        GetAllUserList(userManager);
                        break;
                    case 15:
                        GetAllCarDetails(carManager);
                        GetAllCustomerList(customerManager);
                        RentalAddition(rentalManager);
                        break;
                    case 16:
                        ReturnRental(rentalManager);
                        break;
                    case 17:
                        GetAllRentalDetailList(rentalManager);
                        break;
                    case 18:
                        cikis = false;
                        Console.WriteLine("Exit!");
                        break;
                }
            }
        }

        private static void GetAllRentalDetailList(RentalManager rentalManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("List of Rental Cars: \nId\tCar Name\tCustomer Name\tRent Date\tReturn Date");
            foreach (var rental in rentalManager.GetRentalDetails().Data)
            {
                Console.WriteLine($"{rental.Id}\t{rental.CarName}\t{rental.CustomerName}\t{rental.RentDate}\t{rental.ReturnDate}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void ReturnRental(RentalManager rentalManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("What's the Id number of the car you have rented?");
            int carId = Convert.ToInt32(Console.ReadLine());
            var returnedRental = rentalManager.GetRentalDetails(I => I.CarId == carId);
            foreach (var rental in returnedRental.Data)
            {
                rental.ReturnDate = DateTime.Now;
                Console.WriteLine(Messages.RentalReturned); 
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void RentalAddition(RentalManager rentalManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("Car Id: ");
            int addCarId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Customer Id: ");
            int addCustomerId = Convert.ToInt32(Console.ReadLine());

            Rental addRental = new Rental
            {
                CarId = addCarId,
                CustomerId = addCustomerId,
                RentDate = DateTime.Now,
                ReturnDate = null,
            };
            Console.WriteLine(rentalManager.Add(addRental).Message);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");

        }

        private static void UserAddition(UserManager userManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("First Name: ");
            string addUserName = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            string addUserSurname = Console.ReadLine();
            Console.WriteLine("Email Name: ");
            string addUserEmail = Console.ReadLine();
            Console.WriteLine("Password Name: ");
            string addUserPassword = Console.ReadLine();


            User addUserNames = new User
            {
                FirstName = addUserName,
                LastName = addUserSurname,
                Email = addUserEmail,
                Password = addUserPassword

            };
            userManager.Add(addUserNames);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void GetAllCustomerList(CustomerManager customerManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("List of Customers : \nId\tKullanıcı Id\tCustomer Name");
            foreach (var customer in customerManager.GetAll().Data)
            {
                Console.WriteLine($"{customer.CustomerId}\t{customer.UserId}\t{customer.CompanyName}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void CustomerAddition(CustomerManager customerManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("User Id: ");
            int addUserId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Customer Name: ");
            string addCustomerName = Console.ReadLine();

            Customer addCustomer = new Customer
            {
                UserId = addUserId,
                CompanyName = addCustomerName
            };
            customerManager.Add(addCustomer);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void GetAllUserList(UserManager userManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("List of Users : \nId\tFirst Name\tLast Name\tEmail\tPassword");
            foreach (var user in userManager.GetAll().Data)
            {
                Console.WriteLine($"{user.UserId}\t{user.FirstName}\t{user.LastName}\t{user.Password}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void CarByModelYear(CarManager carManager, BrandManager brandManager, ColorManager colorManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("What's the model year of the car?");
            string modelYearForCarList = Console.ReadLine();
            Console.WriteLine($"\n\nColor Id'si {modelYearForCarList} olan arabalar: \nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tDescriptions");
            foreach (var car in carManager.GetCarsDetail(I => I.ModelYear == modelYearForCarList).Data)
            {
                Console.WriteLine($"{car.CarId}\t{car.ColorName}\t\t{car.BrandName}\t\t{car.ModelYear}\t\t{car.DailyPrice}\t\t{car.Description}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void CarByDailyPrice(CarManager carManager, BrandManager brandManager, ColorManager colorManager)
        {
            decimal min = Convert.ToDecimal(Console.ReadLine());
            decimal max = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine($"\n\nDaily Price of Cars {min} with {max}: \nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tDescriptions");
            foreach (var car in carManager.GetCarsDetail(I => I.DailyPrice >= min & I.DailyPrice <= max).Data)
            {
                Console.WriteLine($"{car.CarId}\t{car.ColorName}\t\t{car.BrandName}\t\t{car.ModelYear}\t\t{car.DailyPrice}\t\t{car.Description}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void CarById(CarManager carManager, BrandManager brandManager, ColorManager colorManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("What's Car Id that you would like to see?");
            int carId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"\n\n Car {carId} : \nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tDescriptions");
            Car carById = carManager.GetById(carId).Data;
            Console.WriteLine($"{carById.CarId}\t{colorManager.GetById(carById.ColorId).Data.ColorName}\t\t{brandManager.GetById(carById.BrandId).Data.BrandName}\t\t{carById.ModelYear}\t\t{carById.DailyPrice}\t\t{carById.Description}");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void CarListByColor(CarManager carManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("What's the Color Id that you would like to see?");
            int colorIdForCarList = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"\n\n Color Id of Cars {colorIdForCarList}: \nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tDescriptions");
            foreach (var car in carManager.GetCarsDetail(I => I.ColorId == colorIdForCarList).Data)
            {
                Console.WriteLine($"{car.CarId}\t{car.ColorName}\t\t{car.BrandName}\t\t{car.ModelYear}\t\t{car.DailyPrice}\t\t{car.Description}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void CarListByBrand(CarManager carManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("What's the Brand Id that you would like to see?");
            int brandIdForCarList = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"\n\n Brand Id of Cars {brandIdForCarList} : \nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tDescriptions");
            foreach (var car in carManager.GetCarsDetail(I => I.BrandId == brandIdForCarList).Data)
            {
                Console.WriteLine($"{car.CarId}\t{car.ColorName}\t\t{car.BrandName}\t\t{car.ModelYear}\t\t{car.DailyPrice}\t\t{car.Description}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void CarUpdate(CarManager carManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("Car Id: ");
            int updateCarId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Brand Id: ");
            int updateBrandId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Color Id: ");
            int UpdateColorId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Daily Price: ");
            decimal uodateDailyPrice = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Description : ");
            string uodateDescription = Console.ReadLine();

            Console.WriteLine("Model Year: ");
            string updateModelYear = Console.ReadLine();

            Car carForUpdate = new Car { CarId = updateCarId, BrandId = updateBrandId, ColorId = UpdateColorId, DailyPrice = uodateDailyPrice, Description = uodateDescription, ModelYear = updateModelYear };
            carManager.Update(carForUpdate);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void CarDeletion(CarManager carManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("What's the Id number of the car you would like to delete? ");
            int carIdToDelete = Convert.ToInt32(Console.ReadLine());
            carManager.Delete(carManager.GetById(carIdToDelete).Data);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void CarAddition(CarManager carManager, BrandManager brandManager, ColorManager colorManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("Color List");
            GetAllColor(colorManager);

            Console.WriteLine("Brand List");
            GetAllBrand(brandManager);

            Console.WriteLine("\nBrand Id: ");
            int brandIdForAdd = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Color Id: ");
            int colorIdForAdd = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Daily Price: ");
            decimal dailyPriceForAdd = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Description : ");
            string descriptionForAdd = Console.ReadLine();

            Console.WriteLine("Model Year: ");
            string modelYearForAdd = Console.ReadLine();

            Car carForAdd = new Car { BrandId = brandIdForAdd, ColorId = colorIdForAdd, DailyPrice = dailyPriceForAdd, Description = descriptionForAdd, ModelYear = modelYearForAdd };
            carManager.Add(carForAdd);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void GetAllCarDetails(CarManager carManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("Detailed List Of Cars :  \nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tDescriptions");
            foreach (var car in carManager.GetCarsDetail().Data)
            {
                Console.WriteLine($"{car.CarId}\t{car.ColorName}\t\t{car.BrandName}\t\t{car.ModelYear}\t\t{car.DailyPrice}\t\t{car.Description}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void GetAllCar(CarManager carManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            Console.WriteLine("List of Cars :  \nId\tColor Name\tBrand Name\tModel Year\tDaily Price\tDescriptions");
            foreach (var car in carManager.GetAll().Data)
            {
                Console.WriteLine($"{car.CarId}\t{car.ColorId}\t\t{car.BrandId}\t\t{car.ModelYear}\t\t{car.DailyPrice}\t\t{car.Description}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void GetAllBrand(BrandManager brandManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            foreach (var brand in brandManager.GetAll().Data)
            {
                Console.WriteLine($"{brand.BrandId}\t{brand.BrandName}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }

        private static void GetAllColor(ColorManager colorManager)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
            foreach (var color in colorManager.GetAll().Data)
            {
                Console.WriteLine($"{color.ColorId}\t{color.ColorName}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------- ");
        }
    }
}
