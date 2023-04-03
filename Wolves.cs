using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2
{
    class Wolves : Animal
    {

        #region fieldsAndConst

        protected static string currname = "Wolf";

        public Wolves(int status) : base(currname, status)
        {

        }

        #endregion

        #region methods

        public bool bunnyIsClose(Animal[,] x, int anI, int anJ)
        {
            setLimits(x, anI, anJ);
            for (int i = lowI; i <= highI; i++)
            {
                for (int j = lowJ; j <= highJ; j++)
                {
                    if (x[i, j].getName == "Bunny")
                        return true;
                }
            }
            return false;
        }


        public void bunnyEaten(Animal x)
        {
            x.AnHealth += 90;
        }

        public void someOneClose(Animal[,] x, int wfI, int wfJ, Animal y)
        {
            setLimits(x, wfI, wfJ);

            if(y.StepStatus == 1)
            {
                if(bunnyIsClose(x, wfI, wfJ))
                {
                    while(true)
                    {
                        int tempi = rnd.Next(lowI, highI + 1);
                        int tempj = rnd.Next(lowJ, highJ + 1);
                        if (x[tempi, tempj].getName == "Bunny")
                        {
                            bunnyEaten(y);
                            y.StepStatus = 0;
                            x[tempi, tempj] = y;
                            x[wfI, wfJ] = new emptySlot();
                            return;

                        }
                    }
                }
            }

            if(y.StepStatus == 1)
            {
                if (y.BornStatus == 0)
                {
                    for (int i = lowI; i <= highI; i++)
                    {
                        for (int j = lowJ; j <= highJ; j++)
                        {
                            if ((x[i, j].getName == "Wolf") && (x[i, j].BornStatus == 1))
                            {
                                makeAChild(x, i, j, x[i, j]);
                                takeDamage(y);
                                y.StepStatus = 0;
                                return;
                            }
                        }
                    }
                }
            }
        }

        #endregion

    }
}
