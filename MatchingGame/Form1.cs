using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {

        //assigns values
        Random random = new Random();
        List<string> icons = new List<string>()
        {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
         };
        //gets the the clicked on squares
        Label firstClicked = null;
        Label seccondClicked = null;

        static int startTime = 60;
        int timeLeft = startTime;

        int points = 0;
        
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }
        //this will repopulate the list after a game is finsished 
        public void reset_icons()
        {
            icons.Add("!");
            icons.Add("!");
            icons.Add("N");
            icons.Add("N");
            icons.Add(",");
            icons.Add(",");
            icons.Add("k");
            icons.Add("k");
            icons.Add("b");
            icons.Add("b");
            icons.Add("v");
            icons.Add("v");
            icons.Add("w");
            icons.Add("w");
            icons.Add("z");
            icons.Add("z");
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //this assigns values to each lable in the app that is a part of the table layout
        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLable = control as Label;
                if (iconLable != null )
                {
                    
                    int randomNumber = random.Next(icons.Count);
                    iconLable.Text = icons[randomNumber];
                    //remove each value after it is selected. this is why the list needs to be repopulated
                    icons.RemoveAt(randomNumber);
                    iconLable.ForeColor = iconLable.BackColor;
                }
            }
        }

        // this handles whenver a user clicks on one of the lables in the table layout
        private void label_Click(object sender, EventArgs e)
        {
            //if time left = 0, and the user clicks on a label, the game starts over
            if (timeLeft == 0)
            {

                AssignIconsToSquares();
                timeLeft = startTime;
                timeLable.Text = timeLeft.ToString() + ": seconds left";
                return;
            }

            Label clickedLable = sender as Label;

            if (firstClicked != null && seccondClicked != null)
            {
                return;
            }

            if (clickedLable != null)
            {
                if (clickedLable.ForeColor == Color.Gray)
                {
                    return;
                }


                if (firstClicked == null)
                {
                    firstClicked = clickedLable;
                    clickedLable.ForeColor = Color.Gray;
                    return;
                }
                seccondClicked = clickedLable;
                seccondClicked.ForeColor = Color.Gray;

                if (firstClicked.Text == seccondClicked.Text)
                {
                    firstClicked.ForeColor = Color.Black;
                    seccondClicked.ForeColor = Color.Black;

                    firstClicked = null;
                    seccondClicked = null;
                    checkForWinner();
                    return;
                }
                //Starts the game time if the even that it is not already started
                timer1.Start();
                if (timer2.Enabled != true)
                {
                    timer2.Start();
                }
                
               
            }
        }
        //this timer is to reset the squares if they do not match
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            seccondClicked.ForeColor = seccondClicked.BackColor;

            firstClicked = null;
            seccondClicked = null;

        }
        // this checks to see if the user won the game
        private void checkForWinner()
        {
            //doing a for each loop for all the lables in the tableLayout 
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLable = control as Label;

                if (iconLable != null)
                {
                    //once a match is made, the lable turns black. this is checking to see if the lables for color is still the same as the background. 
                    //if all lables are black the user won and the game is over. if this section is ever true, then the user has not won yet
                    if (iconLable.ForeColor == iconLable.BackColor)
                    {
                        return;
                    }
                }

            }
            //if the code excutes to this point then the user has won
            timer2.Stop();
            timeLeft = 0;
            reset_icons();
            points = points + 1;
            pointsLable.Text = "Points: " + points.ToString();
            MessageBox.Show("You matched all the squares! Congrats or whatever.");
            
        }
        //this is for the game timer. the user will have 60 seconds to match all the squares.
        private void timer2_Tick(object sender, EventArgs e)
        {
            //the timer ticks once every second. every tick, the time left var is reduced by 1
            timeLeft = timeLeft - 1;
            timeLable.Text = timeLeft.ToString() + ": seconds left";
            //when time left = 0, the game is over.
            if (timeLeft == 0)
            {
                timer2.Stop();
                timeLable.Text = "Times up!";
                timeLeft = 0;
                reset_icons();
                points = 0;
                pointsLable.Text = "Points: " + points.ToString();
                MessageBox.Show("you lost, bitch.");
                foreach (Control control in tableLayoutPanel1.Controls)
                {

                    Label iconLable = control as Label;
                    
                    if (iconLable.ForeColor != Color.Black)
                    {
                        iconLable.ForeColor = Color.Red;
                    }
                }
                     
                

            }
        }
    }
}
