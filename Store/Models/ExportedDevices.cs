using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    internal class ExportedDevices : Device
    {
        public String Provider { get; set; }
        public String Country { get; set; }

        public override string ToString()
        {
            return base.ToString() + $",{this.Provider},{this.Country}";
        }
    }
}
