using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace FoodRecipesApp
{
    public class Recipes 
    {
        public string Title { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string Youtube { get; set; }
        public BindingList<string> Imagesss { get; set; }
        public string IconHeart { get; set; }
        public string WidthItem { get; set; }
    }
}
