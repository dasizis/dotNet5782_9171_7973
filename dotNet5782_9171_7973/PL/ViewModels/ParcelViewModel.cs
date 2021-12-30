using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL.ViewModels
{
    class ParcelViewModel: ContentControl
    {
        public ParcelViewModel(int id)
        {
            Content = new ParcelDetailsViewModel(id);
           
        }

        public ParcelViewModel()
        {
            Content = new AddParcelViewModel();
        }
    }
}
