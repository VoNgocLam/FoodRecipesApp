using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Microsoft.Win32;

namespace FoodRecipesApp
{
    /// <summary>
    /// Interaction logic for NewRecipe.xaml
    /// </summary>
    public partial class NewRecipe : Window
    {
        ObservableCollection<string> listImages = new ObservableCollection<string>();
        BindingList<Steps> _listStep;
        BindingList<Recipes> _newRecipe;
        int i=1;

        public NewRecipe()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            _listStep = new BindingList<Steps>();
            _newRecipe = new BindingList<Recipes>();
            dataListview.ItemsSource = _listStep;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void RecipeImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image Files (*png; *.jpg; *.jpeg; *.gif; *.bmp; )|*.png; *.jpg; *.jpeg; *.gif; *.bmp;";
            if (openFileDialog.ShowDialog() == true)
            {
                var imagePath = openFileDialog.FileNames;
                ImageSource imagesource = new BitmapImage(new Uri(imagePath[0].ToString()));
                ImageDescriptionOfRecipe.ImageSource = imagesource;
                RecipeImageButton.Visibility = Visibility.Collapsed;
            }
        }

        private void imgStep_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files (*png; *.jpg; *.jpeg; *.gif; *.bmp; )|*.png; *.jpg; *.jpeg; *.gif; *.bmp;";
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string item in openFileDialog.FileNames)
                {
                    listImages.Add(item);
                }
            }
        }

        private void addStep_Click(object sender, RoutedEventArgs e)
        {
            var item = new Steps()
            {
                StepName = "Bước " + i.ToString(),
                StepDescription = StepName.Text,
                StepImages = new BindingList<string>()
            };
            foreach(string pathImage in listImages)
            {
                item.StepImages.Add(pathImage);
            }
            _listStep.Add(item);
            StepName.Text = "";
            listImages.Clear();
            i++;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(RecipeName.Text.Trim() != "" && RecipeDescription.Text.Trim()!="" && ImageDescriptionOfRecipe.ImageSource!=null && dataListview.ItemsSource!=null)
            {
                var folder = AppDomain.CurrentDomain.BaseDirectory;
                var pahtRecipeImage = $"{folder}Images";
                var pathRecipe = $"{folder}Recipes.txt";
                string imgRecipe = System.IO.Path.GetFileName(ImageDescriptionOfRecipe.ImageSource.ToString());

                using (StreamWriter sw = File.AppendText(pathRecipe))
                {
                    sw.WriteLine(RecipeName.Text);
                    sw.WriteLine(imgRecipe);
                    sw.WriteLine(RecipeDescription.Text);
                    sw.WriteLine(VideoLink.Text);
                    sw.WriteLine("0");
                };

                var imgPath = ((BitmapImage)ImageDescriptionOfRecipe.ImageSource).UriSource.ToString().Remove(0,8);
                var folderImageRecipe = pahtRecipeImage + "\\" + imgRecipe;
                File.Copy(imgPath, folderImageRecipe, true);

                
                string folderStep = folder + $"\\List\\{RecipeName.Text}";
                if(!Directory.Exists(folderStep))
                {
                    Directory.CreateDirectory(folderStep);
                    string sFile = folderStep + "\\Detail.txt";
                    using (StreamWriter sw = File.CreateText(sFile))
                    {
                        for(int j=0;j<_listStep.Count;j++)
                        {
                            sw.WriteLine(_listStep[j].StepName);
                            sw.WriteLine(_listStep[j].StepDescription);
                            foreach (string imgStepPath in _listStep[j].StepImages)
                            {
                                string nameImg = System.IO.Path.GetFileName(imgStepPath);
                                sw.WriteLine(nameImg);
                                string imgDirectory = folderStep + "\\" + nameImg;
                                File.Copy(imgStepPath, imgDirectory, true);
                            }
                        }
                    };
                }
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
            }

        }
    }
}
