using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Object_Classes;
using System.Windows;

namespace Assignment.ViewModelClasses
{
    class PersonPhoneAddress
    {
        public Person _Person {get; set;}

        public Address _Address { get; set; }

        public Phone _Phone { get; set; }

        public string FullName
        { get { return this._Person._FIRSTNAME + " " + this._Person._LASTNAME; } }

        public string FullAddress
        { get { return this._Address._STREET_NO + " " + this._Address._STREET_ADDRESS + "\n" + this._Address._CITY + ", " + this._Address._STATE + " " + this._Address._ZIP; } }

        public Point Location
        { get { return new Point(double.Parse(this._Address._X), double.Parse(this._Address._Y)); } }

        public Uri PicutreURI
        { get { return new Uri("pack://application:,,,/Assignment;component/Assets/Avatars/" + ((Int32)_Person._PERSONCODE % 42) + ".png"); } }

        public string FullPhone { get { return this._Phone._TYPE + ": " + this._Phone._PHONENUMBER; } }

        public PersonPhoneAddress()
        {
            this._Person = new Person();
            this._Address = new Address();
            this._Phone = new Phone();
        }
    }
}
