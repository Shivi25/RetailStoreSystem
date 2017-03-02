using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Object_Classes;

namespace Assignment.ViewModelClasses
{
    class ItemPhoto
    {
        public Item _Item { get; set; }
       
        public Uri PicutreURI
        { get { return new Uri("pack://application:,,,/Assignment;component/Assets/Items/" + ((Int32)this._Item._ITEMCODE % 48) + ".png"); } }

        public string FullPrice { get { return "$" + this._Item._PRICE + ".00"; } }

        public ItemPhoto()
        {
            this._Item = new Item();
        }
    }
}
