using System.Windows.Forms;
namespace MemoryGame
{
    public partial class frmmemorygame : Form
    {
        List<Button> lstall;
        string currentturn = "";
        string playerone = "One";
        string playertwo = "Two";
        int scoreplayer1 = 0;
        int scoreplayer2 = 0;
       
        List<string> icons = new List<string>();
        bool firstclicked, secondclicked = false;
        string firsticon = "";
        string secondicon = "";
        Button btna;
        Button btnb;
        bool matchfound = false;

        System.Windows.Forms.Timer timerobj = new System.Windows.Forms.Timer();


        public frmmemorygame()
        {
            InitializeComponent();
            lstall = new List<Button>() { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10, btn11, btn12, btn13, btn14, btn15, btn16 };
            lstall.ForEach(b => b.Click += BtnPlayer_Click);
            btnStartGame.Click += BtnStartGame_Click;
            timerobj.Interval = 1000;
            timerobj.Tick += timerobj_Tick;


            StartGame();

        }

        private void Timer()
        {
            timerobj.Enabled = false;
            lstall.ForEach(b => b.Enabled = true);

        }


        //When the its the users turn he choses a box the pic comes up he then gets to chose a second box.If the 2 pis are the same the pics should dissapear and that player gets a point.
        // If he does not get a match he does not get a point but does not lose one either.

        private void SelectButton(Button btn)
        {

            if (timerobj.Enabled == false)
            {
                if (firstclicked == false)
                {

                    btn.ForeColor = Color.Black;
                    firstclicked = true;
                    firsticon = btn.Text;
                    btna = btn;
                }
                else 
                {
                   
                    btn.ForeColor = Color.Black;
                    secondclicked = true;
                    secondicon = btn.Text;
                    btnb = btn;

                    timerobj.Enabled = true;

                }

                //In reference to the comment made... I did this instead of what is commented out in the next procedure. I cant use the enabled = false it doesnt work for my game...
                //thats why I like the other way better
                //This is good, however it does work to use enabled, if you sent btna.enabled = false right below you set btna = btn above.
                if (firstclicked == true && btna == btnb)
                {
                    btna = btn;
                    secondclicked = false;
                    timerobj.Enabled = false;
                }




            }


        }
        private void CheckMatch()
        {
            if (secondclicked == true)
            {


                if (firsticon == secondicon
                   // && btna != btnb
                    )
                {
                    lblMessageBox.Text = "You got a match!!!";
                    btna.Visible = false;
                    btnb.Visible = false;
                    matchfound = true;
                    Score();
                    DisplayScore();
                    CheckWinner();
                }

                else
                {
                    string nextcurrentturn = playertwo;
                    if (currentturn == playerone)
                    {
                        nextcurrentturn = playertwo;
                    }
                    else
                    {
                        nextcurrentturn = playerone;
                    }
                    lblMessageBox.Text = "Player " + currentturn.ToString() + " Try again next turn!!" + '\n' + " Player " + nextcurrentturn.ToString() + " now you chose your first box.";
                    btna.ForeColor = btna.BackColor;
                    btnb.ForeColor = btnb.BackColor;

                }



            }



            if (secondclicked == true && currentturn == playerone)
            {
                currentturn = playertwo;
                firstclicked = false;
                secondclicked = false;
                lstall.ForEach(b => b.ForeColor = b.BackColor);

            }
            else if (secondclicked == true && currentturn == playertwo)
            {
                currentturn = playerone;
                firstclicked = false;
                secondclicked = false;
                lstall.ForEach(b => b.ForeColor = b.BackColor);

            }


            DisplayCurrentTurn();
        }




        private void AssignIcons()
        {
            lstall.ForEach(b => b.ResetText());
            Random rnd = new Random();
            Button btn;
            int randomnumber;
            for (int i = 0; i < lstall.Count; i++)
            {
                if (tblMain.Controls[i] is Button)
                    btn = (Button)tblMain.Controls[i];
                else
                    continue;
                randomnumber = rnd.Next(0, icons.Count);
                btn.Text = icons[randomnumber];
                icons.RemoveAt(randomnumber);
                lstall.ForEach(b => b.ForeColor = b.BackColor);

            }
        }

        private void StartGame()
        {
            currentturn = playerone;
            DisplayMessage("Click a Box!");
            lblPlayer1Score.Text = scoreplayer1.ToString();
            lblPlayer2Score.Text = scoreplayer2.ToString();
            icons = new List<string>()
        {
            "!","!","N","N",",",",","K","K","B","B","V","V","W","W","Z","Z"
        };
            lstall.ForEach(b => b.Visible = true);
            scoreplayer1 = 0;
            scoreplayer2 = 0;

            DisplayCurrentTurn();
            AssignIcons();
            DisplayScore();

        }
        private void Score()
        {
            if (currentturn == playerone && matchfound == true)
            {
                scoreplayer1 += 1;
            }
            else if (currentturn == playertwo && matchfound == true)
            {
                scoreplayer2 += 1;
            }
        }
        private void DisplayScore()
        {

            lblPlayer1Score.Text = scoreplayer1.ToString();
            lblPlayer2Score.Text = scoreplayer2.ToString();
        }



        private void DisplayMessage(string message)
        {
            lblMessageBox.Text = message;
        }



        private void DisplayCurrentTurn()
        {
            lblCurrentTurn.Text = "Current Turn = " + currentturn;
        }



        private void BtnPlayer_Click(object? sender, EventArgs e)
        {

            Button btn = (Button)sender;

            SelectButton(btn);


        }

        //when game is over check who has more points
        private void CheckWinner()
        {
            List<Button> lstcheckwinner = lstall.Where(b => b.Visible == true).ToList();
            if (lstcheckwinner.Count == 0)
            {
                if (scoreplayer1 > scoreplayer2)
                {
                    lblMessageBox.Text = "Player One You are the winner!!!";
                }
                else if (scoreplayer2 > scoreplayer1)
                {
                    lblMessageBox.Text = "Player Two You are the winner!!!";
                }
                else
                {
                    lblMessageBox.Text = "This game is a tie!!";
                }
            }
        }

        private void BtnStartGame_Click(object? sender, EventArgs e)
        {

            StartGame();
        }

        private void lblCurrentTurn_Click(object sender, EventArgs e)
        {

        }

        private void timerobj_Tick(object? sender, EventArgs e)
        {
            CheckMatch();
            Timer();
        }



    }
}