using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
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


namespace FoodRecipesApp
{
    /// <summary>
    /// Interaction logic for RecipeDetailControl.xaml
    /// </summary>
    public partial class RecipeDetailControl : UserControl
    {
        private Recipes _data;
        private double _width;

        public RecipeDetailControl(Recipes r,double w)
        {
            InitializeComponent();
            _data = r;
            _width = w;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _data;
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            string html = "<html><head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            html += "<iframe id='video' src= 'https://www.youtube.com/embed/{0}' frameborder='0' height='265' width='460' allowfullscreen></iframe>";
            html += "</body></html>";
            this.BorderYoutube.Visibility = Visibility.Collapsed;
            this.Play.Visibility = Visibility.Collapsed;
            this.webBrowser.Visibility = Visibility.Visible;
            this.webBrowser.NavigateToString(string.Format(html, _data.Youtube.Split('=')[1]));
            // Do nothing;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.webBrowser.Close();
            RecipeDetail.Children.Clear();
            RecipeDetail.Children.Add(new HomeControl(_width));
        }
    }
}
