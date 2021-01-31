using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTrackerFrontend.Services
{
    public enum MessageType
    {
        Information,
        Error
    }
    public interface IMessageBoxService
    {
        bool Show(string text, string caption, MessageType messageType);
    }
}
