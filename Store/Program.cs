using Store.Metholds;
using Store.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace Store
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Device> list;
            List<Device> selected = new List<Device>();
            int type = UserType();
            if (type == 1)
                AdminView();
            else
            {
                list = UserView();
                int count = 0;
                foreach (Device item in list)
                {
                    WriteLine($"{++count} - {item.totalPrice:C} - {item.Name}");
                }
            ERROR1:
                WriteLine("Please Enter The Number Of Device");
                try
                {

                    int devNum = Convert.ToInt32(Console.ReadLine());
                    if (devNum <= 1 || devNum >= list.Count())
                    {
                        selected.Add(list[devNum - 1]);
                    ERROR2:
                        WriteLine("Do You Need Another Device y/n");
                        string another = ReadLine();
                        if (another.ToLower().Trim() == "y")
                            goto ERROR1;
                        else if (another.ToLower().Trim() == "n")
                            Checkout(selected);
                        else goto ERROR2;
                    }
                    else throw new Exception();
                }
                catch (Exception)
                {
                    WriteLine("Enter A Valid Value");
                    goto ERROR1;
                }
            }






        }
        static public int UserType()
        {
            WriteLine("Welcome To electric devices store");
        ERROR1:
            WriteLine(@"
Enter User Type:
1- Admin
2- User
");
            try
            {
                int userType = Convert.ToInt32(Console.ReadLine());
                if (userType != 1 || userType != 2)
                    return userType;
                else throw new Exception();
            }
            catch (Exception)
            {
                WriteLine("Enter A Valid Value");
                goto ERROR1;
            }
        }
        static public void AdminView()
        {
            Clear();
            WriteLine("Welcome Admin");
        ERROR1:
            WriteLine(@"
Enter Task Number:
1- Export Data
2- Add Device
3- Delete Device
4- Update Device
5- Exit
");
            try
            {
                int taskType = Convert.ToInt32(Console.ReadLine());
                if (taskType == 1)
                    ExportData();
                else if (taskType == 2)
                    AddDevice();
                else if (taskType == 3)
                    DeleteDevice();
                else if (taskType == 4)
                    UpdateDevice();
                else if (taskType == 5)
                    ReadKey();
                else throw new Exception();
            }
            catch (Exception)
            {
                WriteLine("Enter A Valid Value");
                goto ERROR1;
            }
        }
        static public void ExportData()
        {
        ERROR3:
            WriteLine("Enter A File Path To Print ");
            string path = ReadLine();
            if (!Directory.Exists(path))
            {
                WriteLine("Path Not Exists");
                goto ERROR3;
            }
            string filepath = path + "/file.csv";
            if (!File.Exists(filepath))
            {
                File.Create(filepath);
            }
            String text = "";
            foreach (Device device in Database.Devices)
                text += device.ToString() + Environment.NewLine;

            File.WriteAllText(filepath, text);
        }
        static public List<Device> UserView()
        {
            Clear();
            WriteLine("Welcome User");
        ERROR1:
            WriteLine(@"
Enter Device Type Number:
1- Local Device
2- Exported Device
");
            try
            {
                int deviceType = Convert.ToInt32(Console.ReadLine());
                if (deviceType == 1 || deviceType == 2)
                {
                    if (deviceType == 1) return Database.locals();
                    else if (deviceType == 2) return Database.exports();
                    else return null;
                }
                else throw new Exception();
            }
            catch (Exception)
            {
                WriteLine("Enter A Valid Value");
                goto ERROR1;
            }
        }
        static public void Checkout(List<Device> list)
        {
            Clear();
            WriteLine("Check Out");
            WriteLine($"Your Toal Devices Number Is {list.Count()}");
            WriteLine($"Your Toal Price Is ${list.Sum(x => x.totalPrice):C}");
        ERROR3:
            string pathf = "";
            WriteLine("Enter A File Path To Print Bill");
            try
            {
                string path = ReadLine();
                if (!Directory.Exists(path))
                {
                    WriteLine("Path Not Exists");
                    goto ERROR3;
                }
                string filepath = path + "/file.txt";
                if (!File.Exists(filepath))
                {
                    File.Create(filepath);
                }
                pathf = filepath;
            }
            catch (Exception)
            {
                WriteLine("Path Not Exists");
                goto ERROR3;
            }
            String text = "";
            foreach (Device device in list)
            {
                text += $@"
Device Name = {device.Name}
Device Brand Name = {device.Brand}
Device Category = {device.Category}
Device Total Price = {device.totalPrice}
---------------------------------------- {Environment.NewLine}
";
            }
            File.WriteAllText(pathf, text);
            Error9:
            WriteLine("Do You Want To Exit y/n");
            string another = ReadLine();
            if (another.ToLower().Trim() == "y")
                Console.ReadKey();
            else if (another.ToLower().Trim() == "n")
                UserView();
            else goto Error9;
        }
        static public void AddDevice()
        {
            Clear();
            WriteLine(@"Add New Device");
        ERROR1:
            WriteLine(@"Enter A Device Type
1- Local
2- Exported
");
            try
            {
                int deviceType = Convert.ToInt32(Console.ReadLine());
                if (deviceType != 1 || deviceType != 2)
                {
                    if (deviceType == 1)
                    {
                    ERROR4:
                        try
                        {
                            WriteLine("Enhter The Name");
                            string name = ReadLine();
                            WriteLine("Enhter The brand");
                            string brand = ReadLine();
                            WriteLine("Enhter The quantity");
                            int quantity = int.Parse(ReadLine());
                            WriteLine("Enhter The price");
                            double price = double.Parse(ReadLine());
                            WriteLine("Enhter The discout");
                            double discout = double.Parse(ReadLine());
                            WriteLine("Enhter The category");
                            string category = ReadLine();
                            WriteLine("Enhter The Manufacture");
                            string Manufacture = ReadLine();

                            if (String.IsNullOrEmpty(name) ||
                                String.IsNullOrEmpty(brand) ||
                                String.IsNullOrEmpty(Manufacture) ||
                                String.IsNullOrEmpty(category))
                            {
                                WriteLine("Enter All Fields");
                                goto ERROR4;
                            }
                            Device dev = new LocalDevice
                            {
                                Name = name,
                                Brand = brand,
                                Price = price,
                                Quantity = quantity,
                                Discout = discout,
                                Category = category,
                                Manufacture = Manufacture
                            };
                            Database.Devices.Add(dev);
                            WriteLine("Added , Pres Any Key To Continue");
                            ReadKey();
                            Clear();
                            AdminView();
                        }
                        catch (Exception)
                        {
                            WriteLine("Enter A Valid Value");
                            goto ERROR4;
                        }
                    }
                    else if (deviceType == 2)
                    {
                    ERROR5:
                        try
                        {
                            WriteLine("Enhter The Name");
                            string name = ReadLine();
                            WriteLine("Enhter The brand");
                            string brand = ReadLine();
                            WriteLine("Enhter The quantity");
                            int quantity = int.Parse(ReadLine());
                            WriteLine("Enhter The price");
                            double price = double.Parse(ReadLine());
                            WriteLine("Enhter The discout");
                            double discout = double.Parse(ReadLine());
                            WriteLine("Enhter The category");
                            string category = ReadLine();
                            WriteLine("Enhter The Provider");
                            string Provider = ReadLine();
                            WriteLine("Enhter The Country");
                            string Country = ReadLine();

                            if (String.IsNullOrEmpty(name) ||
                                String.IsNullOrEmpty(brand) ||
                                String.IsNullOrEmpty(Provider) ||
                                String.IsNullOrEmpty(Country) ||
                                String.IsNullOrEmpty(category))
                            {
                                WriteLine("Enter All Fields");
                                goto ERROR5;
                            }
                            Device dev = new ExportedDevices
                            {
                                Name = name,
                                Brand = brand,
                                Price = price,
                                Quantity = quantity,
                                Discout = discout,
                                Category = category,
                                Provider = Provider,
                                Country = Country
                            };
                            Database.Devices.Add(dev);
                            WriteLine("Added , Pres Any Key To Continue");
                            ReadKey();
                            Clear();
                            AdminView();
                        }
                        catch (Exception)
                        {
                            WriteLine("Enter A Valid Value");
                            goto ERROR5;
                        }
                    }
                }

                else throw new Exception();
            }
            catch (Exception)
            {
                WriteLine("Enter A Valid Value");
                goto ERROR1;
            }
        }
        static public void DeleteDevice()
        {
            Clear();
            WriteLine(@"Delete Device");
            int count = 0;
            foreach (Device item in Database.Devices)
            {
                WriteLine($"{++count} - {item.totalPrice:C} - {item.Name}");
            }
        ERROR6:
            WriteLine("Please Enter The Number Of Device");
            try
            {

                int devNum = Convert.ToInt32(Console.ReadLine());
                if (devNum <= 1 || devNum >= Database.Devices.Count())
                {
                    Database.Devices.RemoveAt(devNum - 1);
                }
                else throw new Exception();

                WriteLine("Removed Successfully, Press Any Key To Continue");
                ReadKey();
                Clear();
                AdminView();

            }
            catch { goto ERROR6; }
        }
        static public void UpdateDevice()
        {
            Clear();
            WriteLine(@"Update Device");
            int count = 0;
            foreach (Device item in Database.Devices)
            {
                WriteLine($"{++count} - {item.totalPrice:C} - {item.Name}");
            }
        ERROR7:
            WriteLine("Please Enter The Number Of Device");
            try
            {
                int devNum = Convert.ToInt32(Console.ReadLine());
                if (devNum <= 1 || devNum >= Database.Devices.Count())
                {
                    Device dev = Database.Devices[devNum-1];
                    if (dev is LocalDevice)
                    {
                        
                    ERROR4:
                        try
                        {
                            WriteLine("Enhter The Name");
                            string name = ReadLine();
                            WriteLine("Enhter The brand");
                            string brand = ReadLine();
                            WriteLine("Enhter The quantity");
                            int quantity = int.Parse(ReadLine());
                            WriteLine("Enhter The price");
                            double price = double.Parse(ReadLine());
                            WriteLine("Enhter The discout");
                            double discout = double.Parse(ReadLine());
                            WriteLine("Enhter The category");
                            string category = ReadLine();
                            WriteLine("Enhter The Manufacture");
                            string Manufacture = ReadLine();

                            if (String.IsNullOrEmpty(name) ||
                                String.IsNullOrEmpty(brand) ||
                                String.IsNullOrEmpty(Manufacture) ||
                                String.IsNullOrEmpty(category))
                            {
                                WriteLine("Enter All Fields");
                                goto ERROR4;
                            }
                            LocalDevice devc = (LocalDevice)dev;

                            devc.Name = name;
                            devc.Brand = brand;
                            devc.Price = price;
                            devc.Quantity = quantity;
                            devc.Discout = discout;
                            devc.Category = category;
                            devc.Manufacture = Manufacture;
                            Database.Devices.RemoveAt(devNum-1);
                            Database.Devices.Insert(devNum-1, devc);
                            
                            WriteLine("Updated , Pres Any Key To Continue");
                            ReadKey();
                            Clear();
                            AdminView();
                        }
                        catch (Exception)
                        {
                            WriteLine("Enter A Valid Value");
                            goto ERROR4;
                        }
                    }
                    else if (dev is ExportedDevices)
                    {
                    ERROR5:
                        try
                        {
                            WriteLine("Enhter The Name");
                            string name = ReadLine();
                            WriteLine("Enhter The brand");
                            string brand = ReadLine();
                            WriteLine("Enhter The quantity");
                            int quantity = int.Parse(ReadLine());
                            WriteLine("Enhter The price");
                            double price = double.Parse(ReadLine());
                            WriteLine("Enhter The discout");
                            double discout = double.Parse(ReadLine());
                            WriteLine("Enhter The category");
                            string category = ReadLine();
                            WriteLine("Enhter The Provider");
                            string Provider = ReadLine();
                            WriteLine("Enhter The Country");
                            string Country = ReadLine();

                            if (String.IsNullOrEmpty(name) ||
                                String.IsNullOrEmpty(brand) ||
                                String.IsNullOrEmpty(Provider) ||
                                String.IsNullOrEmpty(Country) ||
                                String.IsNullOrEmpty(category))
                            {
                                WriteLine("Enter All Fields");
                                goto ERROR5;
                            }
                            ExportedDevices devc = (ExportedDevices)dev;

                            devc.Name = name;
                            devc.Brand = brand;
                            devc.Price = price;
                            devc.Quantity = quantity;
                            devc.Discout = discout;
                            devc.Category = category;
                            devc.Provider = Provider;
                            devc.Country = Country;

                            Database.Devices.RemoveAt(devNum - 1);
                            Database.Devices.Insert(devNum - 1, devc);
                            WriteLine("Updated , Pres Any Key To Continue");
                            ReadKey();
                            Clear();
                            AdminView();
                        }
                        catch (Exception)
                        {
                            WriteLine("Enter A Valid Value");
                            goto ERROR5;
                        }
                    }
                }
                else throw new Exception();
            }
            catch { goto ERROR7; }
        }
    } 
}
