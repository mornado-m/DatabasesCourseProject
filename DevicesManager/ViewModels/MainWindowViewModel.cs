using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DevicesManager.Models;

namespace DevicesManager.ViewModels
{
    class MainWindowViewModel : Conductor<object>
    {
        public MainWindowViewModel()
        {
            DisplayName = "Devices Manager";

            var login = new LoginViewModel();
            login.UserLogedIn += (sender, args) =>
            {
                _session = new SessionModel(args.UserId, args.UserLogin);
                var devices = new DevicesViewModel(new DevicesModel(_session.UserId, _session.PermissionLevel));
                Items.Add(devices);

                if (_session.PermissionLevel > 1)
                {
                    var transfers =
                        new TransfersViewModel(new TransfersModel(_session.UserId, _session.PermissionLevel));
                    var addDevice =
                        new AddNewDeviceViewModel(new AddNewDeviceModel(_session.UserId, _session.PermissionLevel));
                    var addTransfer =
                        new AddNewTransferViewModel(new AddNewTransferModel(_session.UserId, _session.PermissionLevel));

                    addTransfer.TransferAdded += (o, eventArgs) =>
                    {
                        devices.RefreshData();
                        transfers.RefreshData();
                    };

                    addDevice.DeviceAdded += (o, eventArgs) =>
                    {
                        devices.RefreshData();
                        transfers.RefreshData();
                        addTransfer.RefreshData();
                    };

                    Items.Add(transfers);
                    Items.Add(addDevice);
                    Items.Add(addTransfer);
                }
                else
                {
                    var addtransfer = new DeviceTransferViewModel(new AddNewTransferModel(_session.UserId, _session.PermissionLevel));
                    Items.Add(addtransfer);
                }

                Select(devices);
            };

            login.UserLogedOut += (sender, args) =>
            {
                Items.Clear();
                Items.Add(login);
                Select(login);
            };

            Items.Add(login);

            Select(login);
        }

        private SessionModel _session;

        public BindableCollection<IScreen> Items { get; set; } = new BindableCollection<IScreen>();

        public void Select(object dataContext)
        {
            if (dataContext is IScreen vm && Items.Contains(vm))
            {
                if (vm.IsActive)
                    return;
                ActivateItem(vm);
                vm.Refresh();
            }
            else
            {
                if (dataContext is Screen vm2)
                {
                    (vm2.Parent as IConductActiveItem)?.ActivateItem(vm2);
                    vm2.Refresh();
                }
            }
        }
    }
}
