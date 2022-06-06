using Store.Models;
using System.Collections.Generic;

namespace Store.Metholds
{
    internal static class Database
    {
        static List<Device> _devices = new List<Device>
        {
            new LocalDevice
            {
                Brand = "A",
                Category = "C",
                Discout = 0.1,
                Id = "Id1",
                Manufacture = "M",
                Name = "N",
                Price = 100,
                Quantity = 50

            },
            new ExportedDevices
            {
                Brand = "A",
                Category = "C",
                Discout = 0.1,
                Id = "Id1",
                Country = "C",
                Provider = "P",
                Name = "N",
                Price = 100,
                Quantity = 50
            }
        };
        public static List<Device> Devices { get { return _devices; } }

        public static List<Device> locals()
        {
            List<Device> devices = new List<Device>();
            foreach (Device item in Devices)
                if (item is LocalDevice)
                    devices.Add((LocalDevice)item);
            return devices;
        }
        public static List<Device> exports()
        {
            List<Device> devices = new List<Device>();
            foreach (Device item in Devices)
                if (item is ExportedDevices)
                    devices.Add((ExportedDevices)item);
            return devices;
        }
    }
}
