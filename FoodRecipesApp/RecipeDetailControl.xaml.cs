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
    ///
    public partial class RecipeDetailControl : UserControl
    {
        private Recipes _data;
        private double _width;
        BindingList<Steps> _listdetails;
        int i = 0, j = 1;
        string nameStep = "Bước ";
        public RecipeDetailControl(Recipes r,double w)
        {
            InitializeComponent();
            _data = r;
            _width = w;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _data;
            _listdetails = new BindingList<Steps>();
            PreviewPhoto.ItemsSource=_listdetails;
            stepsListView.ItemsSource = _listdetails;
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            folder = folder + $"\\List\\{_data.Title}\\";
            string sFile = folder + "Detail.txt";
            var dataFile = File.ReadAllLines(sFile);
            while (i < dataFile.Length)
            {
                var step = new Steps()
                {
                    StepName = "",
                    StepDescription = "",
                    StepImages = new BindingList<string>(),
                    Materials = new BindingList<string>()
                };

                if(nameStep + j.ToString() == dataFile[i])
                {
                    step.StepName = dataFile[i];
                    step.StepDescription = dataFile[i + 1];
                    i += 2;
                    for(int k = i, temp = j + 1; ; k++)
                    {
                        if (k >= dataFile.Length)
                        {
                            i = k - 1;
                            j++;
                            break;
                        }

                        if (nameStep + temp.ToString() == dataFile[k] && k < dataFile.Length)
                        {
                            i = k - 1;
                            j++;
                            break;
                        }
                        step.StepImages.Add(folder + dataFile[k]);
                    }
                    _listdetails.Add(step);
                }
                i++;
            }
            stepContentListView.ItemsSource = _listdetails.Take(1);
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

        private void stepsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = stepsListView.SelectedIndex;
            stepContentListView.ItemsSource= _listdetails.Skip(index).Take(1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.webBrowser.Close();
            RecipeDetail.Children.Clear();
            RecipeDetail.Children.Add(new HomeControl(_width));
        }
    }
}
