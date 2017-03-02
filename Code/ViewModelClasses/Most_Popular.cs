using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.ViewModelClasses
{
    class Most_Popular
    {
        public string _Brand { get; set; }
        public int _Quantity { get; set; }

        public string FullInfo
        {
            get
            {
                return string.Format("{0}, Sold: {1}", this._Brand, this._Quantity);
            }
        }
    }
}

