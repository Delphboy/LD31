using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LD31
{

    public struct Clue {
        public int x;
        public int y;
        public string name;
        public string details;
        public bool found;
    }

    public enum direction { 
        up = 1,
        down = 2,
        left = 3,
        right = 4,
    }

    public partial class Form1 : Form
    {
        //GLOBAL VARIABLES
        //Graphics variables
        Graphics visual;
        Font screenFont = new Font(new FontFamily("Tahoma"), 32);
        Font inventoryFont = new Font(new FontFamily("Tahoma"), 12);
        Image black = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Black.png");
        Image success = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\success.png");
        Image failure = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\failure.png");
        Image melted = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Melted.png");
        
        //count down
        int secLeft = 120;

        //Player
        int x = 380;
        int y = 525;
        direction dir = direction.up;
        int animationCycle = 1;
        bool isPaused = false;
        Image playerImg = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\error.png");
        Image bg = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\World\\World.png");
        int inventroyCounter = 0;
        Clue[] Inventory = new Clue[5];

        //Clues
        public Clue[] clueArray = new Clue[5];

        //suspects
        public string[] suspectArray = new string[5];
        public string trueSuspect = "";

        public Form1()
        {
            InitializeComponent();
            visual = CreateGraphics();
            loadGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                        
        }

        public void loadGame() {
            x = 380;
            y = 525;

            //Load textures
            visual.DrawImage(bg, new Rectangle(0, 0, 784, 562)); y += 25;           //WORLD
            visual.DrawImage(playerImg, new Rectangle(x - 6, y - 6, 25, 25));       //PLAYER

            //Load suspects
            suspectArray[0] = "Edward";
            suspectArray[1] = "James";
            suspectArray[2] = "Margret";
            suspectArray[3] = "Elizabeth";
            suspectArray[4] = "George";

            trueSuspect = suspectArray[rndNum(0, 5)];

            Console.WriteLine(trueSuspect);

            //Load clues
            loadClues(trueSuspect);

            //Start game timer
            gameTimer.Interval = 1;
            gameTimer.Start();
            gameTimer.Enabled = true;

            //Start melting Timer
            meltTimer.Interval = 1000;
            meltTimer.Start();
            meltTimer.Enabled = true;
        }

        //LOAD BMP images
        public Bitmap loadImage(string fileloc)
        {
            try
            {
                Bitmap bmp = new Bitmap(fileloc);
                return bmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR LOADING IMAGE: " + ex.Message);
                return null;
            }
        }

        //RND NUM GENERATOR
        //create random numbers for the x and y pos of the clues
        private int rndNum(int min, int max)
        {
            Random rnd = new Random();
            int num = rnd.Next(min, max);
            return num;
        }

        //LOAD CLUES
        void loadClues(string suspect) {
            //set first clue in array to a family pic so player knows all the suspects
            clueArray[0].x = 400;
            clueArray[0].y = 540;
            clueArray[0].found = false;
            clueArray[0].name = "Family Picture";
            clueArray[0].details = "You come accross what looks like a family picture. You turn the picture over and see five names: " + Environment.NewLine + "Edward, James, Margret, Elizabeth, George";

            //load custom parts
            switch (suspect) { 
                //sciency person
                case "Edward":
                    clueArray[1].name = "Tools";
                    clueArray[1].details = "You find a blow torch on the ground, on the bottom the name EDWARD is engraved. Near it you see a welding mask," + Environment.NewLine + "they haven't been put away properly. They have been used recently.";
                    clueArray[2].x = 450;
                    clueArray[2].y = 450;
                    clueArray[2].found = false;

                    clueArray[2].name = "Plans";
                    clueArray[2].details = "You find some plans hidden under a desk entitled 'THE MASTER PLAN'. It is covered in lots of carrots!";
                    clueArray[2].x = 720;
                    clueArray[2].y = 380;
                    clueArray[2].found = false;

                    clueArray[3].name = "Reindeer";
                    clueArray[3].details = "You come accross some marks on the floor that look like they may have come from Reindeer!";
                    clueArray[2].x = 420;
                    clueArray[2].y = 330;
                    clueArray[2].found = false;

                    clueArray[4].name = "Rabbit";
                    clueArray[4].details = "You come accross a rabbit eating a familiar looking carrot! Well, then again, most carrots look the same." + Environment.NewLine + "The rabbit has a collar that reads 'Margret's Rabbit'";
                    clueArray[2].x = 320;
                    clueArray[2].y = 460;
                    clueArray[2].found = false;
                    break;
                //Sporty person
                case "James":
                    clueArray[1].name = "Lunch Box";
                    clueArray[1].details = "You stumble accross a lunch box and some football boots. Upon further inspection you see that the lunch box" + Environment.NewLine + "contains carroty remains!";
                    clueArray[1].x = 190;
                    clueArray[1].y = 170;
                    clueArray[1].found = false;

                    clueArray[2].name = "Picture";
                    clueArray[2].details = "There is a picture of someone after a rugby match. In their hand there is a carrot! On the back of the picture is" + Environment.NewLine + "the date 24th Dec. That's the day your nose went missing!!";
                    clueArray[2].x = 370;
                    clueArray[2].y = 440;
                    clueArray[2].found = false;

                    clueArray[3].name = "Reindeer";
                    clueArray[3].details = "You come accross some marks on the floor that look like they may have come from Reindeer!";
                    clueArray[3].x = 340;
                    clueArray[3].y = 280;
                    clueArray[3].found = false;

                    clueArray[4].name = "Soldering Iron";
                    clueArray[4].details = "You find a soldering iron and plans for 'The Carrot Magnet 3000' on a table, both have the name EDWARD on them.";
                    clueArray[4].x = 470;
                    clueArray[4].y = 520;
                    clueArray[4].found = false;
                    break;
                //animal lover
                case "Margret":
                    clueArray[1].name = "Rabbit";
                    clueArray[1].details = "You come accross a rabbit eating a familiar looking carrot! Well, then again, most carrots look the same. " + Environment.NewLine + "The rabbit has a collar that reads 'Margret's Rabbit'";
                    clueArray[1].x = 330;
                    clueArray[1].y = 520;
                    clueArray[1].found = false;

                    clueArray[2].name = "Shopping List";
                    clueArray[2].details = "A shopping list that reads: 'More carrots for Elizabeth, she has drank too much carrot juice...again!'";
                    clueArray[2].x = 250;
                    clueArray[2].y = 250;
                    clueArray[2].found = false;

                    clueArray[3].name = "Reindeer";
                    clueArray[3].details = "You come accross some marks on the floor that look like they may have come from Reindeer!";
                    clueArray[3].x = 300;
                    clueArray[3].y = 350;
                    clueArray[3].found = false;

                    clueArray[4].name = "Reindeer Food";
                    clueArray[4].details = "You find an empty bag of reindeer food. It must have been for Santa's reindeer last night";
                    clueArray[4].x = 30;
                    clueArray[4].y = 35;
                    clueArray[4].found = false;
                    break;
                //Chef
                case "Elizabeth":
                    clueArray[1].name = "Carrot Juice";
                    clueArray[1].details = "You find a glass of orange liquid, near it is a recipe for 'Carrot Juice'";
                    clueArray[1].x = 510;
                    clueArray[1].y = 30;
                    clueArray[1].found = false;

                    clueArray[2].name = "Cook Book";
                    clueArray[2].details = "You discover a cook book entitled '101 ways to cook carrots' on the inside you see the name 'Elizabeth'";
                    clueArray[2].x = 130;
                    clueArray[2].y = 170;
                    clueArray[2].found = false;

                    clueArray[3].name = "Reindeer";
                    clueArray[3].details = "You come accross some marks on the floor that look like they may have come from Reindeer!";
                    clueArray[3].x = 450;
                    clueArray[3].y = 380;
                    clueArray[3].found = false;

                    clueArray[4].name = "To Do List";
                    clueArray[4].details = "You find a To Do List. It reads: Put carrots in James's lunch and Feed Margret's Carrot";
                    clueArray[4].x = 240;
                    clueArray[4].y = 30;
                    clueArray[4].found = false;
                    break;
                //Young child
                case "George":
                    clueArray[1].name = "Baby Picture";
                    clueArray[1].details = "You find a picture of a young child on the counter, the child is eating a carrot!";
                    clueArray[1].x = 740;
                    clueArray[1].y = 150;
                    clueArray[1].found = false;

                    clueArray[2].name = "Carrot Soup";
                    clueArray[2].details = "You come accross a flask with a note by it. It reads: 'George's carrot soup. Warm through. Elizabeth xx'";
                    clueArray[2].x = 50;
                    clueArray[2].y = 260;
                    clueArray[2].found = false;

                    clueArray[3].name = "Reindeer";
                    clueArray[3].details = "You come accross some marks on the floor that look like Reindeer footprints!";
                    clueArray[3].x = 430;
                    clueArray[3].y = 400;
                    clueArray[3].found = false;

                    clueArray[4].name = "Snowman";
                    clueArray[4].details = "Out of the window you see some snowmen being built by the family. You see the young child eating the carrots!";
                    clueArray[4].x = 80;
                    clueArray[4].y = 30;
                    clueArray[4].found = false;
                    break;
            }
        }

        //Movement
        //Take key press and pass it to the move player function then call check clue void
        
        public bool isWall(string dir) {
            switch (dir) { 
                case "up":
                    if (y < 40)
                    {
                        return true;
                    }
                    break;
                case "down":
                    if (y > 560)
                    {
                        return true;
                    }
                    break;
                case "left":
                    if (x < 40)
                    {
                        return true;
                    }
                    break;
                case "right":
                    if (x > 730)
                    {
                        return true;
                    }
                    break;
            }           
            
            //default response
            return false;
        }

        public Image setPlayerImg(direction dir) {
            //Value to return
            Image playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\error.png");

            switch (dir) { 
                case direction.up:
                    switch (animationCycle)
                    {
                        case 1:
                            playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\up1.png");
                            animationCycle += 1;
                            break;
                        case 2:
                            playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\up2.png");
                            animationCycle += 1;
                            break;
                        case 3:
                            playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\up3.png");
                            animationCycle = 1;
                            break;
                        default:
                            animationCycle = 1;
                            break;
                    }
                    break;
                case direction.down:
                    switch (animationCycle)
                    {
                        case 1:
                            playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\down1.png");
                            animationCycle += 1;
                            break;
                        case 2:
                            playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\down2.png");
                            animationCycle += 1;
                            break;
                        case 3:
                            playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\down3.png");
                            animationCycle = 1;
                            break;
                        default:
                            animationCycle = 1;
                            break;
                    }
                    break;
                case direction.left:
                    switch (animationCycle)
                    {
                        case 1:
                            playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\left1.png");
                            animationCycle += 1;
                            break;
                        case 2:
                            playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\left2.png");
                            animationCycle += 1;
                            break;
                        case 3:
                            playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\left3.png");
                            animationCycle = 1;
                            break;
                        default:
                            animationCycle = 1;
                            break;
                    }
                    break;
                case direction.right:
                    switch (animationCycle)
                    {
                        case 1:
                            playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\right1.png");
                            animationCycle += 1;
                            break;
                        case 2:
                            playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\right2.png");
                            animationCycle += 1;
                            break;
                        case 3:
                            playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\right3.png");
                            animationCycle = 1;
                            break;
                        default:
                            animationCycle = 1;
                            break;
                    }
                    break;
                default:
                    playerImgRtn = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\LD31\\Graphics\\Character\\final\\error.png");
                    break;
            }
            //return the image
            //visual.DrawImage(playerImg, new Rectangle(0, 0, 25, 25));             //use to check the the image function is returning the right picture

            return playerImgRtn;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.W)&&(isPaused == false)&&(isWall("up") == false))
            {
                visual.DrawImage(bg, new Rectangle(0, 0, 784, 562));
                y -= 10;
                dir = direction.up;
                playerImg = setPlayerImg(dir);

                visual.DrawImage(playerImg, new Rectangle(x - 6, y - 6, 25, 25));
            }

            if ((e.KeyCode == Keys.S) && (isPaused == false) && (isWall("down") == false))
            {
                visual.DrawImage(bg, new Rectangle(0, 0, 784, 562));
                y += 10;
                dir = direction.down;
                playerImg = setPlayerImg(dir);

                visual.DrawImage(playerImg, new Rectangle(x - 6, y - 6, 25, 25));
            }

            if ((e.KeyCode == Keys.A) && (isPaused == false) && (isWall("left") == false))
            {
                visual.DrawImage(bg, new Rectangle(0, 0, 784, 562));
                x-=10;
                dir = direction.left;
                playerImg = setPlayerImg(dir);

                visual.DrawImage(playerImg, new Rectangle(x - 6, y - 6, 25, 25));
            }

            if ((e.KeyCode == Keys.D) && (isPaused == false) && (isWall("right") == false))
            {
                visual.DrawImage(bg, new Rectangle(0, 0, 784, 562));
                x += 10;
                dir = direction.right;
                playerImg = setPlayerImg(dir);

                visual.DrawImage(playerImg, new Rectangle(x - 6, y - 6, 25, 25));
            }

            Console.WriteLine("x: " + x.ToString() + "      y: " + y.ToString());

            //ESCAPE[pause game]
            if (e.KeyCode == Keys.Escape) {
                if (isPaused == true)
                {
                    isPaused = false;
                    gameTimer.Enabled = true;
                    visual.DrawImage(bg, new Rectangle(0, 0, 784, 562));
                    visual.DrawImage(playerImg, new Rectangle(x - 6, y - 6, 25, 25));
                }
                else {
                    isPaused = true;
                    gameTimer.Enabled = false;
                    visual.DrawImage(black, new Rectangle(0, 0, 1000, 1000));
                    visual.DrawString("Game Paused", screenFont, Brushes.White, 260, 200);
                }
            }

            //Restart game
            if (e.KeyCode == Keys.Enter) {
                loadGame();
            }

            //Guess suspect
            if (e.KeyCode == Keys.G) { 
                MessageBox.Show("If you are sure you want to accuss who stole your name go ahead!" + Environment.NewLine + "Press 1 for Edward" + Environment.NewLine + "Press 2 for James" + Environment.NewLine + "Press 3 for Margret" + Environment.NewLine + "Press 4 for Elizabeth" + Environment.NewLine + "Press 5 for George");
            }

            if (e.KeyCode == Keys.D1) {
                if (trueSuspect == "Edward")
                {
                    isPaused = true;
                    visual.DrawImage(success, new Rectangle(0, 0, 784, 562));
                }
                else {
                    isPaused = true;
                    visual.DrawImage(failure, new Rectangle(0, 0, 784, 562));
                }
            }

            if (e.KeyCode == Keys.D2)
            {
                if (trueSuspect == "James")
                {
                    isPaused = true;
                    visual.DrawImage(success, new Rectangle(0, 0, 784, 562));
                }
                else
                {
                    isPaused = true;
                    visual.DrawImage(failure, new Rectangle(0, 0, 784, 562));
                }
            }

            if (e.KeyCode == Keys.D3)
            {
                if (trueSuspect == "Margret")
                {
                    isPaused = true;
                    visual.DrawImage(success, new Rectangle(0, 0, 784, 562));
                }
                else
                {
                    isPaused = true;
                    visual.DrawImage(failure, new Rectangle(0, 0, 784, 562));
                }
            }

            if (e.KeyCode == Keys.D4)
            {
                if (trueSuspect == "Elizabeth")
                {
                    isPaused = true;
                    visual.DrawImage(success, new Rectangle(0, 0, 784, 562));
                }
                else
                {
                    isPaused = true;
                    visual.DrawImage(failure, new Rectangle(0, 0, 784, 562));
                }
            }

            if (e.KeyCode == Keys.D5)
            {
                if (trueSuspect == "George")
                {
                    isPaused = true;
                    visual.DrawImage(success, new Rectangle(0, 0, 784, 562));
                }
                else
                {
                    isPaused = true;
                    visual.DrawImage(failure, new Rectangle(0, 0, 784, 562));
                }
            }

            //Inventroy
            if (e.KeyCode == Keys.I) {
                if (isPaused == true)
                {
                    isPaused = false;
                    gameTimer.Enabled = true;
                    visual.DrawImage(bg, new Rectangle(0, 0, 784, 562));
                    visual.DrawImage(playerImg, new Rectangle(x - 6, y - 6, 25, 25));
                }
                else
                {
                    isPaused = true;
                    gameTimer.Enabled = false;
                    visual.DrawImage(black, new Rectangle(0,0, 1000, 1000));
                    visual.DrawString("Inventory", screenFont, Brushes.White, 10, 10);
                    visual.DrawString(Inventory[0].details, inventoryFont, Brushes.White, 10, 60);
                    visual.DrawString(Inventory[1].details, inventoryFont, Brushes.White, 10, 100);
                    visual.DrawString(Inventory[2].details, inventoryFont, Brushes.White, 10, 140);
                    visual.DrawString(Inventory[3].details, inventoryFont, Brushes.White, 10, 180);
                    visual.DrawString(Inventory[4].details, inventoryFont, Brushes.White, 10, 220);

                }
            }

            if (e.KeyCode == Keys.T) { MessageBox.Show(gameTimer.Enabled.ToString()); }

        }

        public int checkIfOnClue() {
            for (int i = 0; i <= 4; i++)
            {
                //Console.WriteLine(i.ToString());
                if ((x == clueArray[i].x) && (y == clueArray[i].y))
                {
                    clueArray[i].found = true;
                    addInventory(clueArray[i]);
                    return i;
                }
            }
            return -1;
        }

        //add a clue to the inventory array
        public void addInventory(Clue clueToAdd) {
            bool loadToInventory = true;

                for (int i = 0; i < 5; i++)
                {
                    if (Inventory[i].name == clueToAdd.name)
                    {
                        loadToInventory = false;
                    }
                }

            if (loadToInventory == true)
            {
                Inventory[inventroyCounter] = clueToAdd;
                inventroyCounter += 1;
                gameTimer.Enabled = true;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //check if found clue
            if (checkIfOnClue() > -1) {
                gameTimer.Enabled = false;
            }
        }

        //updates the time until melt
        private void meltEvent(object sender, EventArgs e)
        {
            if (secLeft > 0)
            {
                secLeft -= 1;
                this.Text = "Time left until melted: " + secLeft;
            }
            else {
                isPaused = true;
                visual.DrawImage(melted, new Rectangle(0, 0, 784, 562));
            }
        }
    }
}
