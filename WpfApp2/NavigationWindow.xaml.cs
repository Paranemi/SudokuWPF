using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaktionslogik für NavigationWindow.xaml
    /// </summary>
    public partial class NavigationWindow : Window
    {
        MediaPlayer m_mediaPlayer = new MediaPlayer();
        public NavigationWindow()
        {
            Play("../../sound/menusound.wav");
            SetVolume(18);
            InitializeComponent();          
        }

        // Play a sound.
        public void Play(string filename)
        {
            m_mediaPlayer = new MediaPlayer();
            m_mediaPlayer.Open(new Uri(filename, UriKind.Relative));
            m_mediaPlayer.Play();
            m_mediaPlayer.Position = TimeSpan.Zero;
        }

        // Set the volume of the MediaPlayer.
        public void SetVolume(int volume)
        {
            // MediaPlayer volume is a float value between 0 and 1.
            m_mediaPlayer.Volume = volume / 100.0f;
        }

        // Stops the MediaPlayer.
        public void Stop()
        {
            m_mediaPlayer.Stop();
            m_mediaPlayer.Close();
        }

        // Set the four buttons, that let you choose your difficulty, on visible.
        private void startgame(object sender, RoutedEventArgs e)
        {
            wp.Visibility = Visibility.Visible;
        }
       
        // Ends the application.
        private void systemexit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        // Starts a new game with seven missing numbers.
        private void startEazy(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(7);
            mw.Show();
            wp.Visibility = Visibility.Hidden;
            Stop();
            this.Hide();
        }

        // Starts a new game with 20 missing numbers.
        private void startNovice(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(20);
            mw.Show();
            wp.Visibility = Visibility.Hidden;
            Stop();
            this.Hide();
        }

        // Starts a new game with 37 missing numbers.
        private void startHard(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(37);
            mw.Show();
            wp.Visibility = Visibility.Hidden;
            Stop();
            this.Hide();
        }

        // Starts a new game with 53 missing numbers.
        private void startMaster(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(53);
            mw.Show();
            wp.Visibility = Visibility.Hidden;
            Stop();
            this.Hide();
        }

        // Starts the tutorial, with one missing number.
        private void starttutorial(object sender, RoutedEventArgs e)
        {
            Tutorial t = new Tutorial(1);
            t.Show();
            Stop();
            this.Hide();
        }
    }
}
