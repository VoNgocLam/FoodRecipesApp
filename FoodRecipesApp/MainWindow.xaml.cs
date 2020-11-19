using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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


namespace FoodRecipesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public HomeControl homeScreen;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

          
                homeScreen = new HomeControl(TagMenu.Width);
                Recipes.Children.Clear();
                Recipes.Children.Add(homeScreen);
         
        }

        private void ListViewItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (TagMenu.Width.ToString().Equals("60"))
            {
                TagMenu.Width = 240;
                Window_Loaded(sender, e);
            }
            else
            {
                TagMenu.Width = 60;
                Window_Loaded(sender, e);
            }
        }
  

        private void TagItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int index = TagItem.SelectedIndex;
           
            switch(index)
            {
                case 1:
                    Recipes.Children.Clear();
                    Recipes.Children.Add(new HomeControl(TagMenu.Width));
                    break;
                case 2:
                    Recipes.Children.Clear();
                    Recipes.Children.Add(new FavouriteRecipesControl());
                    break;
                case 3:
                    NewRecipe f = new NewRecipe();
                    f.Show();
                    this.Close();
                    break;
            }
        }
    }
}
