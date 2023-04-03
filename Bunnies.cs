using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2
{
    class Bunnies : Animal
    {

        #region fields

        protected static string currname = "Bunny";

        protected static int status = -1;

        Random rand = new Random();

        public Bunnies() : base(currname, status)
        {

        }

        #endregion


        #region methods

        public void luckyToMove(Animal[,] x, int anI, int anJ, Animal y)
        {
            if (rand.Next(0, 91) < 12)
                randomStep(x, anI, anJ, y);
        }

        public void luckyToMake(Animal[,] x, int anI, int anJ, Animal y)
        {
            if (rand.Next(0, 101) < 21)
                makeAChild(x, anI, anJ, y);
        }

        #endregion



    }
}
