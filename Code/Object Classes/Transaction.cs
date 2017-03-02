using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Object_Classes
{
    class Transaction
    {
        public int _TRANSACTIONCODE { get; set; }

        public int _PERSONCODE { get; set; }

        public int _STORECODE { get; set; }

        public System.DateTime _TRANSACTION_DATE { get; set; }

        public string _PAYMENT_METHOD { get; set; }

    }
}
