using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Object_Classes;

namespace Assignment.ViewModelClasses
{
    class Item_Include
    {
        public Item _Item { get; set; }

        public int _Quantity { get; set; }

        public int _Total { get; set; }

        public Item_Include()
        {
            _Item = new Item();
        }
    }
}
