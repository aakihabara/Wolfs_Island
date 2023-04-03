namespace L2
{
    public partial class Form1 : Form
    {

        #region fields

        string path = Application.StartupPath;

        DataGridViewImageColumn Bunny = new DataGridViewImageColumn();
        DataGridViewImageColumn Wolf = new DataGridViewImageColumn();
        DataGridViewImageColumn WolfFem = new DataGridViewImageColumn();
        DataGridViewImageColumn Empty = new DataGridViewImageColumn();


        int fieldSize, fieldPars, baseBunnies, baseWolvesF, baseWolves;

        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        Animal[,] grassAn;

        Random rnd = new Random();

        #endregion

        public Form1()  //ПРОВЕРИТЬ НА ДРУГОМ ДАТАГРИДЕ ВЫВОД(Я НЕ ПОНИМАЮ, КАКАЯ ИНДЕКСАЦИЯ)!!!
        {
            InitializeComponent();
            myTimer.Interval = 250;
            myTimer.Tick += timer1_Tick;
            button1.Hide();
            button2.Hide();
            button3.Hide();
        }

        #region buttons

        private void button1_Click(object sender, EventArgs e) // Начало
        {
            baseBunnies = int.Parse(textBox2.Text);
            baseWolves = int.Parse(textBox3.Text);
            baseWolvesF = int.Parse(textBox4.Text);
            baseFill();
            myTimer.Start();
            button1.Hide();
            button3.Hide();
            button2.Show();
        }

        private void button2_Click(object sender, EventArgs e) // Остановка
        {
            myTimer.Stop();
            zeroFields();
            button3.Show();
            button2.Hide();
        }

        private void button3_Click(object sender, EventArgs e) // Сброс поля
        {
            zeroFields();
            rebuildField();
            button4.Show();
            button1.Hide();
            button2.Hide();
            textBox1.Show();
            grassField.ClearSelection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fieldSize = int.Parse(textBox1.Text);
            rebuildField();
            button4.Hide();
            textBox1.Hide();
            grassField.ClearSelection();
            button1.Show();
            button3.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grassField.ClearSelection();

            Image img = new Bitmap(path + "\\Bunny.jpg");
            Bunny.Image = img;
            Bunny.HeaderText = "Bunny";
            Bunny.Name = "Bunny";

            img = new Bitmap(path + "\\Wolf.jpg");
            Wolf.Image = img;
            Wolf.HeaderText = "Wolf";
            Wolf.Name = "Wolf";

            img = new Bitmap(path + "\\WolfF.jpg");
            WolfFem.Image = img;
            WolfFem.HeaderText = "WolfFem";
            WolfFem.Name = "WolfFem";

            img = new Bitmap(path + "\\Empty.jpg");
            Empty.Image = img;
            Empty.HeaderText = "Empty";
            Empty.Name = "Empty";


        }

        #endregion


        #region methods

        public void refreshAnimals()
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    grassAn[i, j].StepStatus = 1;
                }
            }
        }

        public void rebuildField()
        {
            fieldPars = 600 / fieldSize;

            for (int i = 0; i < fieldSize; i++)
            {
                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imageCol.Image = Empty.Image;
                grassField.Columns.Add(imageCol);
                grassField.Rows.Add();
                grassField.Rows[i].Height = fieldPars;
                grassField.Columns[i].Width = fieldPars;
            }

            grassField.Rows.RemoveAt(0);
            grassField.Rows[0].Height = fieldPars;
            grassField.Rows[fieldSize - 1].Height = fieldPars;

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    grassField[i, j].Value = Empty.Image;
                }
            }
        }

        public void moveFieldOnGrass()
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    switch (grassAn[i, j].getName)
                    {
                        case "None":
                            {
                                grassField[j, i].Value = Empty.Image;
                            }
                            break;
                        case "Bunny":
                            {
                                grassField[j, i].Value = Bunny.Image;
                            }
                            break;
                        case "Wolf":
                            if (grassAn[i, j].BornStatus == 1)
                            {
                                grassField[j, i].Value = WolfFem.Image;
                            }
                            else
                            {
                                grassField[j, i].Value = Wolf.Image;
                            }
                            break;
                    }
                }
            }
        }

        public void baseFill()
        {

            grassAn = new Animal[fieldSize, fieldSize];

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    grassAn[i, j] = new emptySlot();
                }
            }

            while (baseBunnies > 0)
            {
                while (true)
                {
                    int tempi = rnd.Next(0, fieldSize);
                    int tempj = rnd.Next(0, fieldSize);
                    if (grassAn[tempi, tempj].getName == "None")
                    {
                        grassAn[tempi, tempj] = new Bunnies();
                        baseBunnies--;
                        break;
                    }
                }
            }

            while (baseWolves > 0)
            {
                while (true)
                {
                    int tempi = rnd.Next(0, fieldSize);
                    int tempj = rnd.Next(0, fieldSize);
                    if (grassAn[tempi, tempj].getName == "None")
                    {
                        grassAn[tempi, tempj] = new Wolves(0);
                        baseWolves--;
                        break;
                    }
                }
            }

            while(baseWolvesF > 0)
            {
                while (true)
                {
                    int tempi = rnd.Next(0, fieldSize);
                    int tempj = rnd.Next(0, fieldSize);
                    if (grassAn[tempi, tempj].getName == "None")
                    {
                        grassAn[tempi, tempj] = new Wolves(1);
                        baseWolvesF--;
                        break;
                    }
                }
            }

            moveFieldOnGrass();
        }

        public void zeroFields()
        {
            grassField.RowCount = 1;
            grassField.ColumnCount = 1;
            fieldSize = 1;
            rebuildField();
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
        }

        public void gameFinished()
        {
            textBox1.Show();
            button1.Show();
            button2.Show();
            button4.Show();
            zeroFields();
        }

        public void checkIfAlive()
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    if (!grassAn[i, j].isAlive)
                        grassAn[i, j] = new emptySlot();
                }
            }
        }

        #endregion



        private void timer1_Tick(object sender, EventArgs e)
        {
            refreshAnimals();

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    if(grassAn[i, j].StepStatus == 1)
                    {
                        switch(grassAn[i, j].getName)
                        {
                            case "None":
                                // Если мы находим пустой элемент, то с ним ничего не делаем
                                break;
                            case "Bunny":
                                {
                                    Bunnies temp = new Bunnies();
                                    temp.luckyToMake(grassAn, i, j, grassAn[i, j]);
                                    temp.luckyToMove(grassAn, i, j, grassAn[i, j]);
                                    //Код проверки на удачу :)
                                }
                                break;
                            case "Wolf":
                                if (grassAn[i, j].BornStatus == 1)
                                {
                                    Wolves temp = new Wolves(1);
                                    temp.someOneClose(grassAn, i, j, grassAn[i, j]);
                                    temp.randomStep(grassAn, i, j, grassAn[i, j]);
                                    //Код проверки, есть ли заяц поблизости, иначе - рандомный шаг
                                }
                                else
                                {
                                    Wolves temp = new Wolves(0);
                                    temp.someOneClose(grassAn, i, j, grassAn[i, j]);
                                    temp.randomStep(grassAn, i, j, grassAn[i, j]);
                                    //Код проверки, есть ли волчица поблизости. Если нет - поиск кролика, иначе - рандомный шаг
                                }
                                break;
                        }
                    }
                    checkIfAlive();
                }
            }

            int countBunn = 0, countWfs = 0, countWfsM = 0;

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    switch(grassAn[i, j].BornStatus)
                    {
                        case -1:
                            countBunn++;
                            break;
                        case 0:
                            countWfs++;
                            break;
                        case 1:
                            countWfsM++;
                            break;
                    }
                }
            }

            if(countWfs + countBunn + countWfsM == 0)
            {
                myTimer.Stop();
                MessageBox.Show("Вот и всё, на острове никого не осталось :(");
                gameFinished();
            }

            textBox2.Text = countBunn.ToString();
            textBox3.Text = countWfs.ToString();
            textBox4.Text = countWfsM.ToString();

            moveFieldOnGrass();
        }

        

    }
}