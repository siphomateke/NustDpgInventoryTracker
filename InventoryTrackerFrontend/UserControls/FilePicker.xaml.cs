using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryTrackerFrontend.UserControls
{
    /// <summary>
    /// Interaction logic for FilePicker.xaml
    /// </summary>
    public partial class FilePicker : UserControl
    {
        public string FilePath { get; set; }
        public string DefaultExt = ".png";
        public string Filter = "Images|*.jpeg;*.png;*.jpg";

        public FilePicker()
        {
            InitializeComponent();
        }

        private void browserButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = DefaultExt;
            dialog.Filter = Filter;
            if (dialog.ShowDialog() == true)
            {
                FilePath = dialog.FileName;
                filePathTextBox.Text = FilePath;
            }
        }
    }
}
