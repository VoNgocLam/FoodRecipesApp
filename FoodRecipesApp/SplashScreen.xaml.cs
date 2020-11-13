using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Threading;


namespace FoodRecipesApp
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        private Random _rng = new Random();
        ObservableCollection<Recipes> _recipe;

        DispatcherTimer dTimer = new DispatcherTimer();
        string sFile = "";
        bool flag = true;
        public SplashScreen()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            sFile = $"{folder}Check.txt";
            var data = File.ReadAllText(sFile);
            InitializeComponent();
            if (data == "checked")
            {
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
            }
            else
            {
                dTimer.Tick += new EventHandler(dTimer_Tick);
                dTimer.Interval = new TimeSpan(0, 0, 60);
                dTimer.Start();
            }
        }
        private void dTimer_Tick(object sender, EventArgs e)
        {
            if (flag == true)
            {
                MainWindow m = new MainWindow();
                m.Show();
                dTimer.Stop();
                this.Close();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var direcFile = $"{folder}Recipes.txt";
            int lineCount = File.ReadLines(direcFile).Count();
            var database = File.ReadAllLines(direcFile);
            int count = lineCount / 5;
            _recipe = new ObservableCollection<Recipes>();
            for(int i=0; i<count; i++)
            {
                var line1 = database[i * 5];
                var line2 = database[i * 5 + 1];
                var line3 = database[i * 5 + 2];
                var recipes = new Recipes()
                {
                    Title = line1,
                    Picture = line2,
                    Description = line3,
                };

                _recipe.Add(recipes);
            }
            var k = _rng.Next(_recipe.Count);
            Title.Text = _recipe[k].Title;
            Description.Text = _recipe[k].Description;
            sFile = $"{folder}Images\\{_recipe[k].Picture}";
            BackgoundImg.ImageSource = new BitmapImage(new Uri(sFile));
        }

        private void Check_Checked(object sender, RoutedEventArgs e)
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            sFile = $"{folder}Check.txt";
            File.AppendAllText(sFile, "checked");
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            flag = false;
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void Check_Unchecked(object sender, RoutedEventArgs e)
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            sFile = $"{folder}Check.txt";
            File.Create(sFile).Close();
        }
    }
}
