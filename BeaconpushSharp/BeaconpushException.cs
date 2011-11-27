using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeaconpushSharp
{
    public class BeaconpushException : Exception
    {
        private readonly int _status;

        public int Status
        {
            get { return _status; }
        }

        public BeaconpushException(int status) 
        {
            _status = status;
        }

        public BeaconpushException(int status, string message) 
            : base(message)
        {
            _status = status;
        }
    }
}
