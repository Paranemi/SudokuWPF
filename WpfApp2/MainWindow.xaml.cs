using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace WpfApp2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Generate gs;
        private Solver sv;
        private int[,] field;
        private MediaPlayer m_mediaPlayer = new MediaPlayer();
        private Button current;
        private int k;
        public MainWindow(int difficulty)
        {
            InitializeComponent();
            create(difficulty);
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
            brush.ImageSource = new BitmapImage(new Uri("../../image/btnshadow.png", UriKind.Relative));
            gs = new Generate();
            field = gs.sudoku(difficulty);
            SoundPlayer pl = new SoundPlayer("../../sound/startgame.wav");
            pl.Play();
            SoundPlayer insert = new SoundPlayer("../../sound/btnstone.wav");
            Play("../../sound/gamesound.wav");
            SetVolume(5);


            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    Button bt = new Button();
                    bt.FontSize = 50;
                    bt.FontFamily = new FontFamily("Osake");
                    bt.Foreground = Brushes.White;
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
                        };
                        btwp.MouseRightButtonUp += (o, f) =>
                        {
                            current.Content = "";
                            field[Grid.GetRow(wrapPanel), Grid.GetColumn(wrapPanel)] = 0;
                        };


                        wrapPanel.Children.Add(btwp);
                    }

                    //Fields, the user has to enter, while solving the sudoku.
                    if (field[i, j] == 0)
                    {
                        bt.Foreground = Brushes.Beige;
                        bt.Background = Brushes.Transparent;
                        Grid.SetZIndex(bt, 2);
                        bt.Content = "";
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

        private void Media_Ended(object sender, EventArgs e)
        {
            m_mediaPlayer.Position = TimeSpan.Zero;
            m_mediaPlayer.Play();
        }

        //Button, when clicked using the solved method in the Solver class, to check if input war correct.
        private void finish(object sender, RoutedEventArgs e)
        {
            sv = new Solver();
            int b = sv.solved(field);


            if (b == 1)
            {
                k = 6;
                fin.IsEnabled = false;
                fin.Foreground = Brushes.DimGray;
                wrapPanel.IsEnabled = false;
            }
            else
            {
                k = 7;
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
