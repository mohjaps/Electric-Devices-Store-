using System;

namespace Store.Models
{
    internal class LocalDevice : Device
    {
        public String Manufacture { get; set; }
        public override string ToString()
        {
            return base.ToString() + $",{this.Manufacture}";
        }
    }
}
