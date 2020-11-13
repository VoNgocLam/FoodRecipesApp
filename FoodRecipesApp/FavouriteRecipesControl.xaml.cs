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

namespace FoodRecipesApp
{
    /// <summary>
    /// Interaction logic for FavouriteRecipesControl.xaml
    /// </summary>
    public partial class FavouriteRecipesControl : UserControl
    {
        public FavouriteRecipesControl()
        {
            InitializeComponent();
            DataContext = this;
        }
        public Recipes FavoriteRecipes;
        ObservableCollection<Recipes> _data;
        PagingInfo pageno = new PagingInfo();
        List<int> temp = new List<int>();


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string iconHeart;
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var pathFile = $"{folder}Recipes.txt";
            int lineCount = File.ReadLines(pathFile).Count();
            var database = File.ReadAllLines(pathFile);
            int count = lineCount / 5;
            _data = new ObservableCollection<Recipes>();
            for (int i = 0; i < count; i++)
            {
                int line5 = int.Parse(database[i * 5 + 4]);
                if (line5 == 1)
                {
                    iconHeart = "Heart";
                    var line1 = database[i * 5];
                    var line2 = database[i * 5 + 1];
                    var line3 = database[i * 5 + 2];
                    var line4 = database[i * 5 + 3];
                    var recipe = new Recipes()
                    {
                        Title = line1,
                        Picture = $"{folder}Images\\{line2}",
                        Description = line3,
                        Youtube = line4,
                        IconHeart = iconHeart,
                        WidthItem = "222"
                    };
                    temp.Add(i);
                    _data.Add(recipe);
                }
            };

            if (_data.Count > 10)
            {
                this.NextButton.Visibility = Visibility.Visible;
                this.PrevButton.Visibility = Visibility.Visible;
            };

            if (_data.Count == 0)
            {
                this.MessageText.Visibility = Visibility.Visible;
                MessageText.Text = "Nothing Found";
                dataListview.ItemsSource = _data.Take(0);

            }
            else
            {
                pageno.CurrentPage = 1;
                pageno.RowsPerPage = 10;
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
            }
          

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
                var folder = AppDomain.CurrentDomain.BaseDirectory;
                var database = $"{folder}Recipes.txt";
                var lines = File.ReadAllLines(database);
                lines[temp[index] * 5 + 4] = "0";
                temp.RemoveAt(index);
                File.WriteAllLines(database, lines);
            }
            UserControl_Loaded(sender, e);
        }
    }
}
