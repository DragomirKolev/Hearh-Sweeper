using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WindowsFormsApplication2

{
    public partial class Form1 : Form
    {
        //create dictionary so I can get the value of the buttons
        Dictionary<Button, bool> dictionary = new Dictionary<Button, bool>();
        //detremental step in 2 player games
        bool playersTurn = true;
        // two arrays, one for each player
        Button[,] array1 = new Button[6,6];
        Button[,] array2 = new Button[6,6];
        // those arrays are basically representing the hearths and X's in my program, array1 = player1, array2 = player2
        bool[,] player1 = new bool[6, 6];
        bool[,] player2 = new bool[6, 6];
        // random number generator needed for making the game always different
        Random r = new Random();
        string result;
        //lifebars
        int life1 = 5;
        int life2 = 5;

        
        
        public Form1()
        {
            
            
            InitializeComponent();
            // initializing life bars
            progressBar1.Value = life1 * 20;
            progressBar2.Value = life2 * 20;
            //Setting up the two 2D arrays of Buttons that the user are gonna use.
            for (int x = 0; x < array1.GetLength(0); x++)
            {
               
                for (int y = 0; y < array1.GetLength(1); y++)
                {
                    array1[x, y] = new Button();
                    array1[x, y].SetBounds(20+55 * x, 40+55 * y, 45, 45);
                    array1[x, y].BackColor = Color.DodgerBlue;
                    array1[x, y].Click += new EventHandler(this.array1Event_Click);
                    Controls.Add(array1[x,y]);
                    player1[x,y] = false;
                   
                }
            }
            for (int x = 0; x < array2.GetLength(0); x++)
            {
                for (int y = 0; y < array2.GetLength(1); y++)
                {
                    array2[x, y] = new Button();
                    array2[x, y].SetBounds(500 + 55 * x, 40 + 55 * y, 45, 45);
                    array2[x, y].BackColor = Color.Azure;
                    array2[x, y].Click += new EventHandler(this.array1Event2_Click);
                    Controls.Add(array2[x, y]);
                    player2[x,y] = false;
                
                    
                }

            }
            // making 5 true values in the arrays that go along with the button arrays ( those values will represent the Hearths later on)
            for (int i = 0; i < 5; i++)
            {
                player2[r.Next(6), r.Next(6)] = true;
                player1[r.Next(6), r.Next(6)] = true;
            }
          
            //seting up dictionarys for both arrays
            for(int i = 0;i < 6; i++)
                for (int j = 0; j < 6; j++)
                {
                    dictionary.Add(array1[i, j], player1[i, j]);

                    dictionary.Add(array2[i, j], player2[i, j]);
                }



            
    
            
        }
        //Loading the background image
        private void Form1_Load(object sender, EventArgs e)
        {
       
            BackgroundImage = Image.FromFile("ship.jpg");
        }
        //Player1 even handler 
        void array1Event_Click(object sender, EventArgs e)
        {
          // seting up the players name
            string player1Name = textBox1.Text;

                //checking what value the player hit and doing things accordingly
                if (playersTurn == true && dictionary[(Button)sender] == true)
                {


                    result = "hit";
                    textBox3.Text = result;
                    life1 = life1 + 2;
                    ((Button)sender).Image = Image.FromFile("hearth.jpg");
                    ((Button)sender).Enabled = false;

                }
                if (playersTurn == true && dictionary[(Button)sender] == false)
                {
                    result = "miss";
                    textBox3.Text = result;
                    playersTurn = false;
                    textBox4.Text = "Second player turn";
                    life1 = life1 - 1;
                    ((Button)sender).Image = Image.FromFile("X.jpg"); 
                    ((Button)sender).Enabled = false;


                }
                //only happens when a player dies( given one of two optons, leave the game or play again.
                if (life1 == 0)
                {
                    DialogResult result = MessageBox.Show(string.Format("The player {0} LOST! Press yes to restart, no to exit. ", player1Name), "Results", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Application.Restart();

                    }
                    else
                        Close();
                }
                progressBar1.Value = life1 * 20;
               
      
        }
    


        
        //the handler for the second player
        void array1Event2_Click(object sender, EventArgs e)
        {

            string player2Name = textBox2.Text;

                //Determends what sort of button the player clicked and does stuff accordingly
                if (playersTurn == false && dictionary[(Button)sender] == true)
                {

                    
                    result = "hit";
                    textBox3.Text = result;
                    life2 = life2 + 2;
                    ((Button)sender).Image = Image.FromFile("hearth.jpg");
                    ((Button)sender).Enabled = false;
                }
                if (playersTurn == false && dictionary[(Button)sender] == false)
                {


                    result = "miss";
                    textBox3.Text = result;
                    textBox4.Text = "First player turn";
                    playersTurn = true;
                    life2 = life2 - 1;
                    ((Button)sender).Image = Image.FromFile("X.jpg");
                    ((Button)sender).Enabled = false;


                }
                // only happens if the player dies ( given two options, leave the game or play again.
                if (life2 == 0)
                {

                    DialogResult result = MessageBox.Show(string.Format("The player {0} LOST! Press yes to restart, no to exit. ", player2Name), "Results", MessageBoxButtons.YesNo);
                   if (result == DialogResult.Yes)
                   {
                       Application.Restart();

                   }else
                    Close();
                }
                progressBar2.Value = life2 * 20;
               
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click_1(object sender, EventArgs e)
        {

        }
        //instructions
        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This game is made for 2 players, that will fight for their lives. The players can enter their names in the boxes on top. In order to win at this game you have to make the other person lose. Each player will be given 5 lives at the start of the game. If you hit a hearth(Placed at random on the board) you will gain 2 lifes. If you miss you lose 1 life. Each player will have 1 click per turn but if he hits a hearth he gains one more click. Have fun and may the odds be with you."); 
        }
        //restart the game
        private void label2_Click(object sender, EventArgs e)
        {

            Application.Restart();
        }

        

       
    
    }
}

