using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Interaktionslogik für Tutorial.xaml
    /// </summary>
    public partial class Tutorial : Window
    {
        private Generate gs;
        private Solver sv;
        private int[,] field;
        private MediaPlayer m_mediaPlayer = new MediaPlayer();
        private Button current;
        private int k;
        public Tutorial(int difficulty)
        {
            InitializeComponent();
            create(difficulty);
            Notification n = new Notification(1);
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

        //Creating 81 buttons for the game board (9x9), filled with the numbers of the generated valid sudoku.
        public void create(int difficulty)
        {
            wrapPanel.Visibility = Visibility.Hidden;
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("../../image/btntutshadow.png", UriKind.Relative));
            gs = new Generate();
            field = gs.sudoku(difficulty);
            SoundPlayer insert = new SoundPlayer("../../sound/btnstone.wav");
            SoundPlayer pl = new SoundPlayer("../../sound/startgame.wav");
            pl.Play();
            Play("../../sound/tutorialsound.wav");
            SetVolume(10);
            int q = 3;
            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    
                    Button bt = new Button();
                    bt.FontSize = 50;
                    bt.FontFamily = new FontFamily("Osake");
                    bt.Foreground = Brushes.Black;
                    bt.Content = field[i, j];
                    Grid.SetRow(bt, i);
                    Grid.SetColumn(bt, j);
                    Grid.Children.Add(bt);
                    bt.Background = brush;

                    //Generates 9 buttons and add them to the WprapPanel. They serve as the input device for the user.
                    for (int k = 1; k < 10; k++)
                    {
                        Button btwp = new Button();
                        btwp.Content = k;
                        btwp.FontFamily = new FontFamily("Osake");
                        btwp.Foreground = Brushes.White;
                        btwp.Width = 30;
                        btwp.Height = 30;
                        btwp.Background = Brushes.Transparent;

                        btwp.Click += (o, f) =>
                        {
                            current.Content = btwp.Content;
                            field[Grid.GetRow(wrapPanel), Grid.GetColumn(wrapPanel)] = (int)current.Content;
                            insert.Play();
                            Notification n = new Notification(q);
                            q = 99;
                          
                        };
                        btwp.MouseRightButtonUp += (o, f) =>
                        {
                            current.Content = "";
                            field[Grid.GetRow(wrapPanel), Grid.GetColumn(wrapPanel)] = 0;
                        };
                        wrapPanel.Children.Add(btwp);
                    }

                    //Fields, the user has to enter, for solving the sudoku.
                    if (field[i, j] == 0)
                    {

                        bt.Background = Brushes.Transparent;
                        Grid.SetZIndex(bt, 2);
                        bt.Content = "";
                        bt.Foreground = Brushes.White;
                        Grid.SetRow(gif, Grid.GetRow(bt));
                        Grid.SetColumn(gif, Grid.GetColumn(bt));
                        bt.IsEnabled = true;
                        bt.MouseEnter += (o, f) =>
                        
                        {
                            current = bt;
                            Grid.SetRow(wrapPanel, Grid.GetRow(bt));
                            Grid.SetColumn(wrapPanel, Grid.GetColumn(bt));
                            
                            wrapPanel.Visibility = Visibility.Visible;
                        };

                    }
                    else
                    {
                        bt.MouseEnter += (o, f) =>
                        {
                            wrapPanel.Visibility = Visibility.Hidden;
                        };
                    }

                    img.MouseEnter += (o, f) =>
                    {
                        wrapPanel.Visibility = Visibility.Hidden;
                    };
                    img1.MouseEnter += (o, f) =>
                    {
                        wrapPanel.Visibility = Visibility.Hidden;
                    };
                    img2.MouseEnter += (o, f) =>
                    {
                        wrapPanel.Visibility = Visibility.Hidden;
                    };
                    img3.MouseEnter += (o, f) =>
                    {
                        wrapPanel.Visibility = Visibility.Hidden;
                    };
                   
                    
                }
            }
        }

        //Button, when clicked using the solved method in the Solver class, to check if input war correct.
        private void finish(object sender, RoutedEventArgs e)
        {
            sv = new Solver();
            int b = sv.solved(field);


            if (b == 1)
            {
                k = 4;
                fin.IsEnabled = false;
                fin.Foreground = Brushes.White;
                wrapPanel.IsEnabled = false;
            }
            else
            {
                k = 5;
            }


            Notification n = new Notification(k);
        }

        //Exit the game
        private void exitgame(object sender, RoutedEventArgs e)
        { 
            m_mediaPlayer.Stop();
            m_mediaPlayer.Close();
            this.Close();
            NavigationWindow nw = new NavigationWindow();
            nw.Show();

        }
        
    }
}
