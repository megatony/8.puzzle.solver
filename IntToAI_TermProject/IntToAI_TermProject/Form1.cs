using IntToAI_TermProject.Classes;
using NGenerics.DataStructures.Queues;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntToAI_TermProject
{
    public partial class Form1 : Form
    {
        public static int[,] goal = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };
        public static int[,] given = new int[,] { {4, 1, 3}, {2, 8, 5}, {7, 0, 6} };

        public static string akin;
        public static int step = 0, sira1 = 0, sira2 = 0;
        public static bool flag = false;

        public Form1()
        {
            InitializeComponent();        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AdditionalFunctions.IsSolvable(given))
            {
                Solve();
            }
            else
            {
                MessageBox.Show("This puzzle cannot be solved.");
                ShowSelectionButtons();
            }
        }

        public async void Solve()
        {
            this.Hide();
            MessageBox.Show("Wait");
            State actualState = null;

            PriorityQueue<State, int> openList = 
                new PriorityQueue<State, int>(PriorityQueueType.Minimum);
            HashSet<State> closedList = new HashSet<State>();
            State goalState = new State();
            goalState.value = goal.Clone() as int[,];
            State initialState = new State();
            initialState.value = given.Clone() as int[,];
            initialState.movesCount = 0;
            initialState.Fvalue = 
                AdditionalFunctions.CalculateManhattan(initialState.value, goal) 
                + initialState.movesCount;         
            openList.Enqueue(initialState,initialState.Fvalue);

            while (openList.Count>0)
            {
                actualState = openList.Dequeue();
                closedList.Add(actualState);

                if (AdditionalFunctions.ContentEquals(actualState.value, goal))
                {
                    this.Show();
                    List<State> states = new List<State>();
                    State state = actualState;
                    while (state != null)
                    {
                        states.Add(state);
                        state = state.parent;
                    }

                    states = states.OrderBy(s => s.movesCount).ToList();

                    foreach (var k in states)
                    {
                        for (int a = 0; a <= k.value.Rank; a++)
                        {
                            for (int b = 0; b <= k.value.Rank; b++)
                            {
                                akin = akin + " " + k.value[a, b].ToString();
                            }
                            akin = akin + "\n";
                        }
                        akin = akin + "\n";
                        step = k.movesCount;
                    }

                    
                    
                    richTextBox1.Text = ("Finished in " + step.ToString() + " step\n" + akin);
                    ShowSelectionButtons();

                    foreach (var m in states)
                    {
                        label1.Text = m.value[0, 0].ToString();
                        label2.Text = m.value[0, 1].ToString();
                        label3.Text = m.value[0, 2].ToString();

                        label4.Text = m.value[1, 0].ToString();
                        label5.Text = m.value[1, 1].ToString();
                        label6.Text = m.value[1, 2].ToString();

                        label7.Text = m.value[2, 0].ToString();
                        label8.Text = m.value[2, 1].ToString();
                        label9.Text = m.value[2, 2].ToString();

                        await Task.Delay(1000);
                    }
                    states.Clear();
                    return;
                }
                else
                {
                    for (int saydir = 0; saydir <= given.Rank; saydir++)
                    {
                        for (int saydir2 = 0; saydir2 <= given.Rank; saydir2++)
                        {
                            if (actualState.value[saydir, saydir2] == 0)
                            {
                                State state1 = new State();
                                State state2 = new State();
                                State state3 = new State();
                                State state4 = new State();

                                state1.value = actualState.value.Clone() as int[,];
                                state2.value = actualState.value.Clone() as int[,];
                                state3.value = actualState.value.Clone() as int[,];
                                state4.value = actualState.value.Clone() as int[,];

                                try
                                {// Left
                                    state1.name = "Move Right!";
                                    state1.value[saydir, saydir2] = actualState.value[saydir, saydir2 - 1];
                                    state1.value[saydir, saydir2 - 1] = actualState.value[saydir, saydir2];
                                    state1.movesCount = actualState.movesCount + 1;
                                    state1.Fvalue =
                                    AdditionalFunctions.CalculateManhattan(state1.value, goal) + state1.movesCount;
                                    state1.parent = actualState;
                                    if (closedList.Count > 0)
                                    {
                                        foreach (var k in closedList)
                                        {
                                            if (AdditionalFunctions.ContentEquals(k.value, state1.value))
                                            {
                                                flag = true;
                                            }
                                        }
                                        if (flag == false)
                                        {
                                            openList.Enqueue(state1,state1.Fvalue);
                                        }
                                        else
                                        {
                                            flag = false;
                                        }
                                    }
                                    else
                                    {
                                        openList.Enqueue(state1,state1.Fvalue);
                                    }



                                }
                                catch
                                {

                                }

                                try
                                {// Up
                                    state2.name = "Move Down!";
                                    state2.value[saydir, saydir2] = actualState.value[saydir - 1, saydir2];
                                    state2.value[saydir - 1, saydir2] = actualState.value[saydir, saydir2];
                                    state2.movesCount = actualState.movesCount + 1;
                                    state2.Fvalue =
                                    AdditionalFunctions.CalculateManhattan(state2.value, goal) + state2.movesCount;
                                    state2.parent = actualState;
                                    if (closedList.Count > 0)
                                    {
                                        foreach (var k in closedList)
                                        {
                                            if (AdditionalFunctions.ContentEquals(k.value, state2.value))
                                            {
                                                flag = true;
                                            }
                                        }
                                        if (flag == false)
                                        {
                                            openList.Enqueue(state2,state2.Fvalue);
                                        }
                                        else
                                        {
                                            flag = false;
                                        }
                                    }
                                    else
                                    {
                                        openList.Enqueue(state2,state2.Fvalue);
                                    }
                                }
                                catch
                                {

                                }

                                try
                                { // Down
                                    state3.name = "Move Up!";
                                    state3.value[saydir, saydir2] = actualState.value[saydir + 1, saydir2];
                                    state3.value[saydir + 1, saydir2] = actualState.value[saydir, saydir2];
                                    state3.movesCount = actualState.movesCount + 1;
                                    state3.Fvalue =
                                    AdditionalFunctions.CalculateManhattan(state3.value, goal) + state3.movesCount;
                                    state3.parent = actualState;
                                    if (closedList.Count > 0)
                                    {
                                        foreach (var k in closedList)
                                        {
                                            if (AdditionalFunctions.ContentEquals(k.value, state3.value))
                                            {
                                                flag = true;
                                            }
                                        }
                                        if (flag == false)
                                        {
                                            openList.Enqueue(state3,state3.Fvalue);
                                        }
                                        else
                                        {
                                            flag = false;
                                        }
                                    }
                                    else
                                    {
                                        openList.Enqueue(state3,state3.Fvalue);
                                    }
                                }
                                catch
                                {

                                }
                                try
                                { // Right
                                    state4.name = "Move Left!";
                                    state4.value[saydir, saydir2] = actualState.value[saydir, saydir2 + 1];
                                    state4.value[saydir, saydir2 + 1] = actualState.value[saydir, saydir2];
                                    state4.movesCount = actualState.movesCount + 1;
                                    state4.Fvalue =
                                    AdditionalFunctions.CalculateManhattan(state4.value, goal) + state4.movesCount;
                                    state4.parent = actualState;
                                    if (closedList.Count > 0)
                                    {
                                        foreach (var k in closedList)
                                        {
                                            if (AdditionalFunctions.ContentEquals(k.value, state4.value))
                                            {
                                                flag = true;
                                            }
                                        }
                                        if (flag == false)
                                        {
                                            openList.Enqueue(state4,state4.Fvalue);
                                        }
                                        else
                                        {
                                            flag = false;
                                        }
                                    }
                                    else
                                    {
                                        openList.Enqueue(state4,state4.Fvalue);
                                    }

                                }
                                catch
                                {

                                }                               
                            }
                        }
                    }
                }
            }
            MessageBox.Show(":(");
        }

        public void GetNext()
        {
            if (sira2 < 3)
            {
                sira2++;

                if (sira2 == 3)
                {
                    sira2 = 0;
                    sira1++;
                    if (sira1 == 3)
                    {
                        Print();
                        sira1 = 0;
                        sira2 = 0;
                    }
                }
            }
        }
        public void ShowSelectionButtons()
        {
            button2.Show();
            button3.Show();
            button4.Show();
            button5.Show();
            button6.Show();
            button7.Show();
            button8.Show();
            button9.Show();
            button10.Show();
        }
        public void Print()
        {
            label1.Text = given[0, 0].ToString();
            label2.Text = given[0, 1].ToString();
            label3.Text = given[0, 2].ToString();

            label4.Text = given[1, 0].ToString();
            label5.Text = given[1, 1].ToString();
            label6.Text = given[1, 2].ToString();

            label7.Text = given[2, 0].ToString();
            label8.Text = given[2, 1].ToString();
            label9.Text = given[2, 2].ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // 1
            given[sira1, sira2] = 1;
            GetNext();
            Print();
            button2.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 2
            given[sira1, sira2] = 2;
            GetNext();
            Print();
            button3.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 3
            given[sira1, sira2] = 3;
            GetNext();
            Print();
            button4.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 4
            given[sira1, sira2] = 4;
            GetNext();
            Print();
            button5.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // 5
            given[sira1, sira2] = 5;
            GetNext();
            Print();
            button6.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // 6
            given[sira1, sira2] = 6;
            GetNext();
            Print();
            button7.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 7
            given[sira1, sira2] = 7;
            GetNext();
            Print();
            button8.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // 8
            given[sira1, sira2] = 8;
            GetNext();
            Print();
            button9.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // 0
            given[sira1, sira2] = 0;
            GetNext();
            Print();
            button10.Hide();
        }

    }
}
