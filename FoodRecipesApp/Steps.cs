using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FoodRecipesApp
{
    class Steps
    {

        public string StepName { get; set; }
        public string StepDescription { get; set; }
        public BindingList<string> StepImages { get; set; }
        public BindingList<string> Materials { get; set; }

    }
}
