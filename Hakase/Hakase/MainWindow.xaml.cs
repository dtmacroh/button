/*
 * DOCUMENTATION:
 * 
 * Author:              Debbie Macrohon
 * Create Date:         September 21, 2017
 * Last Modified Date:  September 23, 2017
 * Description: 
 *              This project is meant to convey my user's (Irene) personalities and likes, and will hopefully
 *              delight Irene.
 *               
 *              The main character/button, Hakase, is Irene's favorite character from the anime 
 *              Nichijou, which is also her favorite show from the Comedy Genre. Irene has described
 *              herself as oblivious, random, clumsy, funny. She has also mentioned that she likes reading,
 *              playing the alto-saxophone, watching anime and a lot more. For the purposes of this project 
 *              I chose hobbies and personalities that would be easier to convey.
 * 
 *              Personality 1: Oblivious
 *              One click on the character means you are calling their attention. Depending on the
 *              environment, there is either a 90% or 40% chance she will face you. If Hakase is in the jungle,
 *              it is 80%. If in a quiet library, 30%.
 * 
 *              Personality 2: Random
 *              Every few seconds, after you have called Hakase's attention, she will do her own thing. There is a 
 *              randomized sequence from 1-4, from this she will either turn her back on you, face left, right, but 
 *              will never turn to face you. This also plays to her obivious personality. Every so often, she will 
 *              jump. And every so often, she will trip and fall. :) 
 *              
 *              Personality 3: Funny
 *              Upon double clicking on Hakase, she will begin playing the sax. The music used is "Careless Whisper" 
 *              by George Michael. I think this contributes to her funny personality because it is unexpected. (after
 *              playing around with it for so long it gets stale though)
 * 
 *              Personality 4: Clumsy
 *              Hakase will randomly trip and fall. If Hakase is in the jungle environment, she will 
 *              trip a lot more than when she is in the library.
 *              
 *              Likes 1: Alto-Saxophone playing!
 *              Hakase will play the sax upon being double clicked. To stop this, single click on Hakase again and she
 *              will stop instantly. 
 *              
 *              Likes 2: Anime!
 *              Initially I wanted to rotate through a number of anime characters to convey, Irene's love for anime, and 
 *              to convey the idea of cosplaying. However due to time constraints I decided to focus on one character (Hakase)
 *              and project Irene's personality onto her.
 *               
 * Limitations:
 * 
 *             - When falling, Hakase has the same facial expression.
 *             - Hakase can only play "Careless Whisper". 
 * Resources:              
 *      Much of this code comes from Kevin Ta, the Teaching Assistant of this course (CPSC 581). 
 *      Kevin's website     = http://kevinta893.weebly.com/projects.html
 *      Base source code    = http://pages.cpsc.ucalgary.ca/~kta/581/#!index.md (A Useless Button link)
 *     
 *      Thanks Kevin!
 *      
 *      Images:
 *          Jungle image        = http://thehungergames.wikia.com/wiki/File:Jungle_1.jpg (/images/jungle.jpg)
 *          Hakase images       = http://nichijou.wikia.com/wiki/File:Hakase_01.jpg       
 *                              (/images/hakase_front.jpg, hakase_back.jpg, hakase_left.jpg, hakase_right.jpg)
 *          Musical notes       = http://clipart-library.com/free-music-note-clipart.html
 *                              (/images/G_clef.png, musi_2.png, musi_3.png)
 *          Library image       = http://www.credomag.com/2017/07/26/building-a-theological-library-part-3-tips-on-building-a-digital-library/
 *                              (/images/library.jpg)
 */



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Hakase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string backgroundSource = "library";    //keeps track of the background image
        private Random r = new Random();                
        private DispatcherTimer sleepTimer = new DispatcherTimer(); 
        private bool pressed = false;                   //determines if button is pressed or not
        private bool has_jumped = false;                //determines if button has jumped or not
        private string btnSource;                       //keeps track of button image
        Storyboard stationary;                          //stationary animation, bobs up and down
        private bool play_Sax = false;
        private SoundPlayer saxSound = 
            new SoundPlayer("sounds\\carelessWhisper.wav");
        private DateTime timeOfHoldStart { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            stationary = this.FindResource("stationary") as Storyboard;
            stationary.Begin();

            btnSource = "back";         //initial image is Hakase's back.

            sleepTimer.Tick += Timer_Tick;
            sleepTimer.Interval = System.TimeSpan.FromMilliseconds(3000);

            saxSound.Load();

        }

        public void btnHakase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Storyboard playSax = this.FindResource("music") as Storyboard; 
            sleepTimer.Stop();
            //if (hold == false)
            //{
            //    timeOfHoldStart = DateTime.Now;
            //}
            //if (DateTime.Now.Subtract(timeOfHoldStart)>= new TimeSpan(0,0,1))
            //{

            //    btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_left.png", UriKind.Relative));
            //    hold = true;
            //}
            if (e.ClickCount == 2)
            {
               

               btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_playing_sax.png", UriKind.Relative));
                btnSource = "sax";
                playSax.Begin();

                //playSax.Completed += playSax_Completed;
                saxSound.Play();
                playSax.RepeatBehavior = RepeatBehavior.Forever;

                
            }
            else if (e.ClickCount == 1 )
            {
                int rand = RandomInt(1, 10);
                if (backgroundSource.Equals("library"))
                {
                    if ((rand >= 1 && rand <= 3) || btnSource.Equals("sax"))
                    //there is a 30% chance that Hakase will turn around and face you! 
                    //also Hakase should not be playing the sax as of this time.
                    {
                        btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_front.png", UriKind.Relative));
                        btnSource = "front";
                    }
                   
                    else if (rand == 10 && !btnSource.Equals("sax"))
                    //Hakase is clumsy, chance of her falling is 10%.
                    {
                        Storyboard fall = this.FindResource("fall") as Storyboard;
                        fall.Begin();
                    }
                    else
                    {
                        btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_back.png", UriKind.Relative));
                        btnSource = "back";
                    }
                }
                else if (backgroundSource.Equals("jungle"))
                {
                    if ((rand >= 1 && rand <= 9) || btnSource.Equals("sax"))
                    //Given a new environment, Hakase will be more aware of surroundings. 
                    //90% chance she will turn to face you.
                    //also Hakase should not be playing the sax as of this time.
                    {
                        btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_front.png", UriKind.Relative));
                        btnSource = "front";
                    }

                    else if (rand >= 4 && rand >= 10 && !btnSource.Equals("sax"))
                    //Hakase is clumsy, chance of her falling is 60% in the jungle.
                    {
                        Storyboard fall = this.FindResource("fall") as Storyboard;
                        fall.Begin();
                    }

                }
                    //play_Sax = false;
                    saxSound.Stop();
                playSax.Stop();

            }

            //if (e.ClickCount == 3)
            //{
            //    btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_right.png", UriKind.Relative));
            //}
            pressed = true;
        }
        private void btnHakase_MouseRightDown(object sender, MouseButtonEventArgs e)
        {
            //toggle backgrounds
            if (backgroundSource.Equals("jungle"))
            {
                background.ImageSource = new BitmapImage(new Uri(@"images/library.jpeg", UriKind.Relative));
                backgroundSource = "library";
            }
            else
            {
                background.ImageSource = new BitmapImage(new Uri(@"images/jungle.jpg", UriKind.Relative));
                backgroundSource = "jungle";
            }
        }
        public void btnHakase_MouseUp(object sender, MouseButtonEventArgs e)
        {
           // btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_front.png", UriKind.Relative));
            pressed = false;
           
            
            sleepTimer.Start();
        }
        public void jumped_Completed(object sender, EventArgs e)
        {
            has_jumped = true;
        }
        public void playSax_Completed(object sender, EventArgs e)
        {
            play_Sax = true;
        }
        private void btnHakase_MouseMove(object sender, MouseEventArgs e)
        {
            if (pressed == false)
            {
                //mouse button not held down, we do nothing
                return;
            }
            //Point mousePos = e.GetPosition(this);



            //btnHakase.Margin = new Thickness(
            //    (mousePos.X - (btnHakase.ActualWidth / 2)),
            //    (mousePos.Y - (btnHakase.ActualHeight / 2)),
            //    0,0
               
            //);

        }

        //=====================
        //Utility functions

        private void Timer_Tick(object sender, EventArgs e)
        {
            //button is poked, Hakase will do her own thing after some time
            int rand = RandomInt(0, 4);
           
             btnSource = "";
            if (rand == 1)
            {
                btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_right.png", UriKind.Relative));
                btnSource = "right";
            }
            else if (rand == 2)
            {
                btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_back.png", UriKind.Relative));
                btnSource = "back";
            }
            else if (rand == 3)
            {
                btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_left.png", UriKind.Relative));
                btnSource = "left";

            }
            else if (rand == 4)
            {
                Storyboard jump = this.FindResource("jump") as Storyboard;
                jump.Begin();

                jump.Completed += jumped_Completed;

            }
            //else if (rand == 5)
            //{
            //    background.ImageSource = new BitmapImage(new Uri(@"images/jungle.jpg", UriKind.Relative));
            //}
            //else if (rand == 6)
            //{
            //    background.ImageSource = new BitmapImage(new Uri(@"images/library.jpeg", UriKind.Relative));
            //}
        }
        //returns a number between low and high inclusive
        private int RandomInt(int low, int high)
        {
            return (r.Next() % (high + 1) + low);
        }


    }

}

