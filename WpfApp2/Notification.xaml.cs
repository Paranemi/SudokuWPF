using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaktionslogik für Notification.xaml
    /// </summary>
    public partial class Notification : Window
    {
        private Tutorial t;
        public Notification(int theme)
        {
            InitializeComponent();
            setMode(theme);
        }

        // Set the bachground picture, and buttons in the matching style. Depends on the handed over int.
        private void setMode(int theme)
        {
            var tutorialBrush = new ImageBrush();
            tutorialBrush.ImageSource = new BitmapImage(new Uri("../../image/notificationtut.png", UriKind.Relative));
            var gameBrush = new ImageBrush();
            gameBrush.ImageSource = new BitmapImage(new Uri("../../image/notificationgame.png", UriKind.Relative));
            var tutbtn = new ImageBrush();
            tutbtn.ImageSource = new BitmapImage(new Uri("../../image/btntutshadow.png", UriKind.Relative));
            var gamebtn = new ImageBrush();
            gamebtn.ImageSource = new BitmapImage(new Uri("../../image/btnshadow.png", UriKind.Relative));
            SoundPlayer notification = new SoundPlayer("../../sound/btn.wav");
            SoundPlayer congrats = new SoundPlayer("C:../../sound/congrats.wav");

            if (theme == 1)
            {
                notificwnd.Background = tutorialBrush;
                notbtn.Background = tutbtn;
                nottextblock.Text = "the game sudoku is played on a 9x9 board. it is a game of pure logic and is great for keeping your brain in top shape.";
                notbtn.Content = "got it!";
                this.Show();
                this.Focus();
                this.Topmost = true;
                notbtn.Click += (o, f) =>
                {
                    Notification n = new Notification(2);
                };               
            }
            if (theme == 2)
            {
                notificwnd.Background = tutorialBrush;
                notbtn.Background = tutbtn;
                nottextblock.Text = "sudoku has a single rule: fill every row, column and 3x3 box with numbers 1 to 9, so each number appears exactly once. use the mouse to enter the field with the glowing effect and try to find the missing number.";
                notbtn.Content = "got it!";          
                this.ShowDialog();
                this.Focus();
                this.Topmost = true;
            }
            if (theme == 3)
            {
                notificwnd.Background = tutorialBrush;
                notbtn.Background = tutbtn;
                nottextblock.Text = "when you have completed the sudoku, click the finish-button. you will get a notification, if your input was correct. right click to undo, or just override your number with a new one.";
                notbtn.Content = "got it!";
                notification.Play();
                this.ShowDialog();
                this.Focus();
                this.Topmost = true;               
            }

            if (theme == 4)
            {
                notificwnd.Background = tutorialBrush;
                notbtn.Background = tutbtn;
                nottextblock.Text = "well done! you have solved the puzzle.";
                notbtn.Content = "thanks!";
                nottextblock.FontSize = 35;
                congrats.Play();
                this.ShowDialog();
                this.Focus();
                this.Topmost = true;             
            }
            if (theme == 5)
            {
                notificwnd.Background = tutorialBrush;
                notbtn.Background = tutbtn;
                nottextblock.Text = "error! there is something wrong.";
                nottextblock.Foreground = Brushes.Crimson;
                notbtn.Content = "ok!";
                nottextblock.FontSize = 35;
                notification.Play();
                this.ShowDialog();
                this.Focus();
                this.Topmost = true;
                
            }
            if (theme == 6)
            {
                notificwnd.Background = gameBrush;
                notbtn.Background = gamebtn;
                nottextblock.Foreground = Brushes.Beige;
                nottextblock.Text = "well done! you have solved the puzzle.";
                notbtn.Content = "thanks!";
                nottextblock.FontSize = 35;
                congrats.Play();
                this.ShowDialog();
                this.Focus();
                this.Topmost = true;
                
            }
            if (theme == 7)
            {
                notificwnd.Background = gameBrush;
                notbtn.Background = gamebtn;
                nottextblock.Text = "error! there is something wrong.";
                nottextblock.Foreground = Brushes.IndianRed;
                notbtn.Content = "ok!";
                nottextblock.FontSize = 35;
                notification.Play();
                this.ShowDialog();
                this.Focus();
                this.Topmost = true;
           
            }
          
        }

        // Closes the notification window.
        private void gotit(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
