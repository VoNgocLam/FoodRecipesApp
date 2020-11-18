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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Security.Policy;

namespace FoodRecipesApp
{
    /// <summary>
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
        private double _width;
        public HomeControl(double Width)
        {
            InitializeComponent();
            DataContext = this;
            _width = Width;
        }
        
        public Recipes FavoriteRecipes { get; set; } = null;
        ObservableCollection<Recipes> _data;
        PagingInfo pageno = new PagingInfo();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string iconHeart;
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var pathFile = $"{folder}Recipes.txt";
            int lineCount = File.ReadLines(pathFile).Count();
            var database = File.ReadAllLines(pathFile);
            int count = lineCount / 5;
            _data = new ObservableCollection<Recipes>();
            for(int i=0; i<count; i++)
            {
                var line1 = database[i * 5];
                var line2 = database[i * 5 + 1];
                var line3 = database[i * 5 + 2];
                var line4 = database[i * 5 + 3];
                int line5 = int.Parse(database[i * 5 + 4]);
                if (line5==0)
                {
                    iconHeart = "HeartOutline";

                }
                else
                {
                    iconHeart = "Heart";
                }             
                var recipe = new Recipes()
                {
                    Title = line1,
                    Picture = $"{folder}Images\\{line2}",
                    Description = line3,
                    Youtube = line4,
                    IconHeart = iconHeart,
                    WidthItem="222"
                };
                _data.Add(recipe);
            };
            
            if (_data.Count > 10)
            {
                this.NextButton.Visibility = Visibility.Visible;
                this.PrevButton.Visibility = Visibility.Visible;
            };

                 
            pageno.CurrentPage = 1;
            if(_width==60)
            {
                pageno.RowsPerPage = 10;
            }
            else
            {
                pageno.RowsPerPage = 8;
            }
            pageno.Count = _data.Count;
            pageno.TotalPage = (pageno.Count / pageno.RowsPerPage) +
               (pageno.Count % pageno.RowsPerPage == 0 ? 0 : 1);

            Thread thread = new Thread(delegate ()
             {
                 Dispatcher.Invoke(() =>
                 {
                     dataListview.ItemsSource = _data.Take(pageno.RowsPerPage);

                 });
             });

            thread.Start();
            var temp = dataListview.ActualHeight;

        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageno.CurrentPage <= pageno.TotalPage)
            {
                pageno.CurrentPage--;
                dataListview.ItemsSource =
                _data
                    .Skip((pageno.CurrentPage - 1) * pageno.RowsPerPage)
                    .Take(pageno.RowsPerPage);
                if (pageno.CurrentPage <= 1)
                {
                    pageno.CurrentPage = 1;
                }
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageno.CurrentPage < pageno.TotalPage)
            {
                pageno.CurrentPage++;
                dataListview.ItemsSource =
                _data
                    .Skip((pageno.CurrentPage - 1) * pageno.RowsPerPage)
                    .Take(pageno.RowsPerPage);
            }
        }

        private void ButtonFavorite_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = dataListview.Items.IndexOf(item) + ((pageno.CurrentPage - 1) * pageno.RowsPerPage);
            if (_data[index].IconHeart == "Heart")
            {
                _data[index].IconHeart = "HeartOutline";
                var folder = AppDomain.CurrentDomain.BaseDirectory;
                var database = $"{folder}Recipes.txt";
                var lines = File.ReadAllLines(database);
                lines[index * 5 + 4] = "0";
                File.WriteAllLines(database, lines);

                
            }
            else
            {
                _data[index].IconHeart = "Heart";
                FavoriteRecipes = _data[index];
                var folder = AppDomain.CurrentDomain.BaseDirectory;
                var database = $"{folder}Recipes.txt";
                var line = File.ReadAllLines(database);
                line[index * 5 + 4] = "1";
                File.WriteAllLines(database, line);
            }
            UserControl_Loaded(sender, e);
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var searchkeyword = SearchTextBox.Text;
            ObservableCollection<Recipes> results = new ObservableCollection<Recipes>();
            for(int i = 0; i < _data.Count; i++)
            {
                if ((_data[i].Title).ToLower().Contains((SearchTextBox.Text).ToLower()) || (_data[i].Title).ToUpper().Contains((SearchTextBox.Text).ToUpper()))
                {
                    results.Add(_data[i]);
                }
            }
            if (results.Count > 0)
            {
                dataListview.ItemsSource = results.Take(10);
                this.MessageText.Visibility = Visibility.Hidden;

            }
            if (results.Count==0)
            {
                dataListview.ItemsSource = results.Take(0);
                this.MessageText.Visibility = Visibility.Visible;
                MessageText.Text = "Nothing Found";
            }    
        }
             
        private void dataListview_MouseUp(object sender, MouseEventArgs e)
        {
            
            var recipe = (sender as ListView).SelectedItem as Recipes;
            if (recipe != null)
            {
                Home.Children.Clear();
                Home.Children.Add(new RecipeDetailControl(recipe, _width));
            }           
        }
    }
}
