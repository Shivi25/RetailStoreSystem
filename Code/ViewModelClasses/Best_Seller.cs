using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.ViewModelClasses
{
    class Best_Seller
    {
        public string _ItemName { get; set; }
        public int _Quantity { get; set; }

        public string FullInfo
        {
            get
            {
                return string.Format("{0}, Sold: {1}", this._ItemName, this._Quantity);
            }
        }
    }
}
