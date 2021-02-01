using InventoryTrackerFrontend.Common;
using InventoryTrackerFrontend.Models;
using InventoryTrackerFrontend.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryTrackerFrontend.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public CommandHandler LoadedCommand { get; set; }
        public CommandHandler RefreshEquipmentCommand { get; set; }
        public CommandHandler<SelectionChangedEventArgs> EquipmentSelectionChangedCommand { get; set; }
        public CommandHandler<SelectionChangedEventArgs> EquipmentChangeSelectionChangedCommand { get; set; }
        public CommandHandler ApproveChangeCommand { get; set; }
        public CommandHandler DiscardChangeCommand { get; set; }
        public IMessageBoxService MessageBoxService { get; set; }
        public HomePageViewModel(IMessageBoxService messageBoxService)
        {
            LoadedCommand = new CommandHandler(() => OnLoaded());
            RefreshEquipmentCommand = new CommandHandler(() => RefreshEquipment());
            EquipmentSelectionChangedCommand = new CommandHandler<SelectionChangedEventArgs>((o) => OnEquipmentSelectionChanged(o));
            EquipmentChangeSelectionChangedCommand = new CommandHandler<SelectionChangedEventArgs>((o) => OnEquipmentChangeSelectionChanged(o));
            ApproveChangeCommand = new CommandHandler(() => ApproveEquipmentChange());
            DiscardChangeCommand = new CommandHandler(() => DiscardEquipmentChange());

            this.MessageBoxService = messageBoxService;
        }

        public bool IsLoading { get; set; }
        public int SelectedEquipmentId { get; set; }
        public bool AnyEquipmentSelected { get; set; }
        public List<Equipment> Equipment { get; set; }
        public List<EquipmentChange> EquipmentChanges { get; set; }
        public List<BasicEquipment> EquipmentToBuy { get; set; }
        public bool AnyEquipmentChangeSelected { get; set; }
        public int SelectedEquipmentChangeId { get; set; }
        public bool ShowOwnChangesOnly { get; set; }

        public void OnLoaded()
        {
            RefreshEquipment();
        }

        public async void RefreshEquipment()
        {
            try
            {
                IsLoading = true;
                DataAccess db = new DataAccess();
                Equipment = await db.GetEquipment();
                // FIXME: Refresh on ShowOwnChangesOnly value change
                EquipmentChanges = await db.GetEquipmentChanges(ShowOwnChangesOnly);
                EquipmentToBuy = await db.GetAllEquipmentToBuy();
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void OnEquipmentSelectionChanged(SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Equipment selectedEquipment = (Equipment)e.AddedItems[0];
                SelectedEquipmentId = selectedEquipment.EquipmentId;
                AnyEquipmentSelected = true;
            }
            else
            {
                AnyEquipmentSelected = false;
            }
        }

        public void OnEquipmentChangeSelectionChanged(SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                EquipmentChange selectedEquipmentChange = (EquipmentChange)e.AddedItems[0];
                SelectedEquipmentChangeId = selectedEquipmentChange.EquipmentChangeId;
                AnyEquipmentChangeSelected = true;
            }
            else
            {
                AnyEquipmentChangeSelected = false;
            }
        }

        public async void ApproveEquipmentChange()
        {
            try
            {
                IsLoading = true;
                DataAccess db = new DataAccess();
                await db.ApproveEquipmentChange(SelectedEquipmentChangeId);
                RefreshEquipment();
            }
            catch (Exception ex)
            {
                MessageBoxService.Show(ex.Message, "Error", MessageType.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async void DiscardEquipmentChange()
        {
            try
            {
                IsLoading = true;
                DataAccess db = new DataAccess();
                await db.UndoEquipmentChange(SelectedEquipmentChangeId);
                RefreshEquipment();
            }
            catch (Exception ex)
            {
                MessageBoxService.Show(ex.Message, "Error", MessageType.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

    }
}
