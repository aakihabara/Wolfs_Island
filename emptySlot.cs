using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2
{
    class emptySlot : Animal
    {
        protected static int status = -2;

        protected static string currname = "None";

        public emptySlot() : base(currname, status)
        {

        }
    }
}
