using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FoodRecipesApp
{
    public class PagingInfo: INotifyPropertyChanged
    {
        private int _currentPage;
        public int TotalPage { get; set; }
        public int Count { get; set; }
        public int RowsPerPage { get; set; }
        public int CurrentPage { get => _currentPage; set { _currentPage = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));} }
       
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
