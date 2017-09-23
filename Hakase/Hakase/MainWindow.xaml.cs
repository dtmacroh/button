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
        private int poked = 0; //is one click
        private Random r = new Random();
        private DispatcherTimer sleepTimer = new DispatcherTimer();
        private bool pressed = false;
        private bool has_jumped = false;
        private string btnSource;
        Storyboard stationary;
        private bool play_Sax = false;
        private SoundPlayer saxSound = new SoundPlayer("sounds\\carelessWhisper.wav");
        private DateTime timeOfHoldStart { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //stationary = this.FindResource("stationary") as Storyboard;
            //stationary.Begin();

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
                //Storyboard jump = this.FindResource("jump") as Storyboard;
                //jump.Begin();

                //jump.Completed += jumped_Completed;

               btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_playing_sax.png", UriKind.Relative));

                playSax.Begin();
                playSax.Completed += playSax_Completed;
                saxSound.Play();
                playSax.RepeatBehavior = RepeatBehavior.Forever;
                //if (play_Sax==true)
                //{

                //    btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_front.png", UriKind.Relative));
                //}
            }
            else if (e.ClickCount == 1 )
            {
                btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_front.png", UriKind.Relative));
                play_Sax = false;
                saxSound.Stop();
                playSax.Stop();
            }

            //if (e.ClickCount == 3)
            //{
            //    btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_right.png", UriKind.Relative));
            //}
            pressed = true;
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
            Point mousePos = e.GetPosition(this);



            btnHakase.Margin = new Thickness(
                (mousePos.X - (btnHakase.ActualWidth / 2)),
                (mousePos.Y - (btnHakase.ActualHeight / 2)),
                0,0
               
            );

        }

        //=====================
        //Utility functions

        private void Timer_Tick(object sender, EventArgs e)
        {
            //button is poked, Hakase will turn back after some time
            int rand = RandomInt(0, 5);
             btnSource = ""; 
            if (rand==1)
            { 
            btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_right.png", UriKind.Relative));
                btnSource = "right";
            }
            else if (rand ==2)
            {
                btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_back.png", UriKind.Relative));
                btnSource = "back";
            }
            else if (rand == 3)
            {
                btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_left.png", UriKind.Relative));
                btnSource = "left";

            }
            //else if (rand==4 && btnSource.Equals("right"))
            //{
            //    Storyboard goRight = this.FindResource("stationary") as Storyboard;
            //    goRight.Begin();

            //}

        }
        //returns a number between low and high inclusive
        private int RandomInt(int low, int high)
        {
            return (r.Next() % (high + 1) + low);
        }


    }

}
/* DOCUMENTATION
 * 
 * 
 * Much of this code comes from Kevin Ta, the Teaching Assistant of this course (CPSC 581)
 * 
 * Hakase images come from http://nichijou.wikia.com/wiki/File:Hakase_01.jpg
 * Musical notes come from http://clipart-library.com/free-music-note-clipart.html
 * 
 * 
 * 
*/
