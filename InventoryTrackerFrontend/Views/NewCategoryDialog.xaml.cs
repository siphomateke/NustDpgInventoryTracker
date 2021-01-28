using InventoryTrackerFrontend.ViewModels;
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

namespace InventoryTrackerFrontend.Views
{
    /// <summary>
    /// Interaction logic for NewCategoryDialog.xaml
    /// </summary>
    public partial class NewCategoryDialog : Window
    {
        public NewCategoryDialogViewModel ViewModel { get; set; }
        public NewCategoryDialog()
        {
            InitializeComponent();
            ViewModel = new NewCategoryDialogViewModel();
            this.DataContext = ViewModel;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
