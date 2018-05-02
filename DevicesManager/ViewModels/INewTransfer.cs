using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesManager.ViewModels
{
    class INewTransfer
    {
        public int DeviceId { get; set; }
        public int FirstDeptId { get; set; }
        public int SecondDeptId { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
