using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2
{
    class Animal
    {

        #region fieldsAndConstuctors

        private int health;
        private int bornStatus;
        private string name;
        protected int lowJ, lowI, highI, highJ, bornIndI, bornIndJ;
        private int stepStatus = 1;

        public Random rnd = new Random();

        public Animal(string name, int status)
        {
            this.name = name;
            this.bornStatus = status;
            health = 100;
        }

        #endregion

        #region prop

        public string getName
        {
            get
            {
                return name;
            }
        }

        public int AnHealth
        {
            get
            { 
                return health; 
            }

            set
            {
                health = value;
            }
        }

        public int BornStatus
        {
            get
            {
                return bornStatus;
            }
        }

        public int StepStatus
        {
            get
            {
                return stepStatus;
            }

            set
            {
                stepStatus = value;
            }
        }

        public bool isAlive
        {
            get
            {
                if (health > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region methods

        public bool canIGoSomewhere(Animal[,] x, int anI, int anJ)
        {
            setLimits(x, anI, anJ);
            for (int i = lowI; i <= highI; i++)
            {
                for (int j = lowJ; j <= highJ; j++)
                {
                    if (x[i, j].getName == "None")
                        return true;
                }
            }
            return false;
        }

        public void takeDamage(Animal x)
        {
            x.AnHealth -= 10;
        }

        public void setLimits(Animal[,] x, int anI, int anJ)
        {
            if (anI > 0)
                lowI = anI - 1;
            else
                lowI = anI;

            if (anJ > 0)
                lowJ = anJ - 1;
            else
                lowJ = anJ;

            if (anI < Math.Sqrt(x.Length) - 1)
                highI = anI + 1;
            else
                highI = anI;

            if (anJ < Math.Sqrt(x.Length) - 1)
                highJ = anJ + 1;
            else
                highJ = anJ;
        }

        public void findPlace(Animal[,] x, int inix, int injx)
        {
            setLimits(x, inix, injx);

            if (canIGoSomewhere(x, inix, injx))
            {
                while (true)
                {
                    int tempi = rnd.Next(lowI, highI + 1);
                    int tempj = rnd.Next(lowJ, highJ + 1);
                    if (x[tempi, tempj].getName == "None")
                    {
                        bornIndI = tempi;
                        bornIndJ = tempj;
                        return;
                    }
                }
            }
            //Поиск идёт только по ближайшим клеткам
        }

        public void randomStep(Animal[,] x, int anI, int anJ, Animal y)
        {
            if (y.StepStatus == 1)
            {
                int tempi = anI;
                int tempj = anJ;
                if (canIGoSomewhere(x, anI, anJ))
                {
                    while (true)
                    {
                        setLimits(x, anI, anJ);
                        tempi = rnd.Next(lowI, highI + 1);
                        tempj = rnd.Next(lowJ, highJ + 1);
                        if (x[tempi, tempj].getName == "None")
                        {
                            if(y.bornStatus >= 0)
                            {
                                takeDamage(y);
                            }
                            y.stepStatus = 0;
                            x[tempi, tempj] = y;
                            x[anI, anJ] = new emptySlot();
                            break;
                        }
                    }
                    return;
                }
                else
                {
                    if (y.bornStatus >= 0)
                        takeDamage(y);
                    y.stepStatus = 0;
                    return;
                }
            }
            else
                return;
        }

        public void makeAChild(Animal[,] x, int indi, int indj, Animal y)
        {
                findPlace(x, indi, indj);
                if (y.bornStatus == 1)
                {
                    int choice = rnd.Next(0, 2);
                    switch (choice)
                    {
                        case 0:
                            x[bornIndI, bornIndJ] = new Wolves(0);
                            y.StepStatus = 0;
                            takeDamage(y);
                            break;
                        case 1:
                            x[bornIndI, bornIndJ] = new Wolves(1);
                            y.StepStatus = 0;
                            takeDamage(y);
                        break;
                        default:
                            break;
                    }
                }
                else if (y.bornStatus == -1)
                {
                    x[bornIndI, bornIndJ] = new Bunnies();
                    //Получает ли он дамаг?
                    y.stepStatus = 0;
                }
        }

        #endregion



    }
}