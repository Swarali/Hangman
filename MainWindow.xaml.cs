using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;


namespace Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    

    public partial class MainWindow : Window
    {
        int wrongcount = 0;
        int score=0;
        String country = "India";
        int length;
        StringBuilder dispString = new StringBuilder("");
       

        public MainWindow()
        {
         
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChildControls ccChildren = new ChildControls();

            foreach (object o in ccChildren.GetChildren(ButtongroupBox, 1))
            {
                if (o.GetType() == typeof(Button))
                {
                    ((Button)o).IsEnabled = true;
                }
            }
       //     hangmanimage.Source = new BitmapImage(new Uri(@"C:\Users\Swarali\Pictures\Favorites\Hangman-0.png"));
            hangmanimage.Source = new BitmapImage(new Uri(@"Images\Hangman-0.png", UriKind.Relative));
            
            PlayButton.Visibility = System.Windows.Visibility.Collapsed;
            PlayButton.IsEnabled = false;
            
            Scorelabel.Content = "Your score : " +score + "\nChances remaining : " +(5-wrongcount);

            correctlabel.Visibility = System.Windows.Visibility.Collapsed;
           
            DataSet1 ds;
            DataSet1TableAdapters.CountryTableTableAdapter ta = new DataSet1TableAdapters.CountryTableTableAdapter();
            
            ds = new DataSet1();
                      
           var ran = new Random().Next(242);
          
            while(ran == 138 && ran != 43)
                  ran = new Random().Next(242);

            country = ta.GetData().FindByCountryID(ran).CountryName;
            //MessageBox.Show(country);
          //  DispLabel.Content = country;
           

            length = country.Length;
           
            int i;
            for (i = 0; i < length; i++)
            {


                if (country[i].Equals(' '))
                {
                    dispString = dispString.Append(" ");
                    dispString = dispString.Append(" ");
                }
                else
                {
                    dispString = dispString.Append("_");
                    dispString = dispString.Append(" ");
                }

            }

          // ResultLabel.Content = country;
            DispLabel.Content = dispString;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            String c = clickedButton.Content.ToString();
            //String temp;
            
            char[] tempcountry = dispString.ToString().ToCharArray();
            
            Boolean charfound = false, win= true ;
            for(int i=0; i<length*2;i=i+2)
            {
                 String temp =  country[i/2].ToString();
                // MessageBox.Show(temp + " and " + c + (temp.Equals(c) || temp.Equals(c.ToLower())));
                if (temp.Equals(c) || temp.Equals(c.ToLower()))
                {
                   // ResultLabel.Content = "Checking" + " " + c;
                    tempcountry[i] = c[0];
                    dispString = new StringBuilder(new string(tempcountry));
                 //   MessageBox.Show(dispString.ToString());
                    DispLabel.Content = dispString ;
                    charfound = true;
                }
            }

            clickedButton.IsEnabled = false;

            for (int i = 0; i < length*2; i=i+2)
            {
                if (dispString[i] == '_')
                {
                    win = false;
                    break;
                }
            }

           // ResultLabel.Content = "Checking" + " " + win +" " + wrongcount;

            if (win)
            {
                youWin();
            }

            else    if (charfound == true)
                {
                    DispLabel.Content = dispString;
                }
                else
                {///Hangman;component/Images/Chrysanthemum.jpg
                    wrongcount++;
                    
//                    if (wrongcount == 1) hangmanimage.Source = new BitmapImage(new Uri(@"C:\Users\Swarali\Pictures\Favorites\Hangman-1.png"));
                    if (wrongcount == 1) hangmanimage.Source = new BitmapImage(new Uri(@"Images\Hangman-1.png", UriKind.Relative));
                    if (wrongcount == 2) hangmanimage.Source = new BitmapImage(new Uri(@"Images\Hangman-2.png", UriKind.Relative));
                    if (wrongcount == 3) hangmanimage.Source = new BitmapImage(new Uri(@"Images\Hangman-3.png", UriKind.Relative));
                    if (wrongcount == 4) hangmanimage.Source = new BitmapImage(new Uri(@"Images\Hangman-4.png", UriKind.Relative)); 
                    if (wrongcount == 5)
                    {
                        hangmanimage.Source = new BitmapImage(new Uri(@"Images\Hangman-5.png", UriKind.Relative));
                     //   label1.Content = "x x";
                        youLose(); 
                    }
                    Scorelabel.Content = "Your score : " + score + "\nChances remaining : " + (5 - wrongcount);
                }


        }

       

        private void youWin()
        {

            PlayButton.Visibility = System.Windows.Visibility.Visible;
            PlayButton.IsEnabled = true;
          

            ChildControls ccChildren = new ChildControls();

            foreach (object o in ccChildren.GetChildren(ButtongroupBox, 1))
            {
                if (o.GetType() == typeof(Button))
                {
                    ((Button)o).IsEnabled = false;
                }
            }
            score++;
            ResultLabel.Content = "Great job! You Win!";
            Scorelabel.Content = "Your score : " + score + "\nChances remaining : " + (5 - wrongcount);
        }

        private void youLose()
        {
            score = 0;
            int i;
            PlayButton.Visibility = System.Windows.Visibility.Visible;
            PlayButton.IsEnabled = true;

            correctlabel.Content = "Correct Answer is : " + country;
            correctlabel.Visibility = System.Windows.Visibility.Visible;
            ChildControls ccChildren = new ChildControls();

            foreach (object o in ccChildren.GetChildren(ButtongroupBox, 1))
            {
                if (o.GetType() == typeof(Button))
                {
                    ((Button)o).IsEnabled = false;
                }
            }
            ResultLabel.Content = "You lose.5 chances are up.Try again!";
            Scorelabel.Content = "Your score : " + score + "\nChances remaining : " + (5 - wrongcount);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            wrongcount = 0;
            dispString = new StringBuilder("");
            country = "";
            ResultLabel.Content = "";
            Window_Loaded(sender, e);
        }

      
       
    }


    class ChildControls
    {
        private List<object> lstChildren;

        public List<object> GetChildren(Visual p_vParent, int p_nLevel)
        {
            if (p_vParent == null)
            {
                throw new ArgumentNullException("Element {0} is null!", p_vParent.ToString());
            }

            this.lstChildren = new List<object>();

            this.GetChildControls(p_vParent, p_nLevel);

            return this.lstChildren;

        }

        private void GetChildControls(Visual p_vParent, int p_nLevel)
        {
            int nChildCount = VisualTreeHelper.GetChildrenCount(p_vParent);

            for (int i = 0; i <= nChildCount - 1; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(p_vParent, i);

                lstChildren.Add((object)v);

                if (VisualTreeHelper.GetChildrenCount(v) > 0)
                {
                    GetChildControls(v, p_nLevel + 1);
                }
            }
        }
    }

}
