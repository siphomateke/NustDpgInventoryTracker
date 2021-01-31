using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryTrackerFrontend.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public bool Show(string text, string caption, MessageType messageType)
        {
            if (messageType == MessageType.Information)
            {
                MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (messageType == MessageType.Error)
            {
                MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return true;
        }
    }
}
