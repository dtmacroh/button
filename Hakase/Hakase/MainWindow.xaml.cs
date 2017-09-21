using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
        bool is_poked = false; //is one click
        private Random r = new Random();
        private DispatcherTimer sleepTimer = new DispatcherTimer();
        private bool pressed = false;
        public MainWindow()
        {
            InitializeComponent();
            


        }

        public void btnHakase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_back.png", UriKind.Relative));
            }
            if (e.ClickCount == 2)
            {
                btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_left.png", UriKind.Relative));
            }
            if (e.ClickCount == 3)
            {
                btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_right.png", UriKind.Relative));
            }
        }
        public void btnHakase_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_front.png", UriKind.Relative));
         
        }

        private void btnHakase_MouseMove(object sender, MouseEventArgs e)
        {
            

        }

        //=====================
        //Utility functions

        private void Stopwatch_Tick(object sender, EventArgs e)
        {
            //button is poked, Hakase will turn back
            btnHakase.Source = new BitmapImage(new Uri(@"/images/hakase_right.png", UriKind.Relative));
            
        }



    }

}
/* DOCUMENTATION
 * 
 * 
 * Much of this code comes from Kevin Ta, the Teaching Assistant of this course (CPSC 581)
 * 
 * Hakase images come from http://nichijou.wikia.com/wiki/File:Hakase_01.jpg
 * 
 * 
 * 
 * 
*/
