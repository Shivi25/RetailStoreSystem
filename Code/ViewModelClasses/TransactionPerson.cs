using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Object_Classes;

namespace Assignment.ViewModelClasses
{
    class TransactionPerson
    {
        public Transaction _Transaction { get; set; }

        public string _FirstName { get; set; }

        public string _LastName { get; set; }

        public string FullName { get { return this._FirstName + " " + this._LastName; } }

        public string ShortDate { get { return this._Transaction._TRANSACTION_DATE.ToShortDateString(); } }

        public TransactionPerson()
        {
            this._Transaction = new Transaction();
        }
    }
}
