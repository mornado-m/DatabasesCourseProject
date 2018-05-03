using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using DevicesManager.Models;

namespace DevicesManager.ViewModels
{
    class DeviceTransferViewModel : Screen
    {
        public DeviceTransferViewModel(AddNewTransferModel model)
        {
            DisplayName = "Оформити перенесення пристрою";
            _model = model;
            RefreshData();
        }

        private AddNewTransferModel _model;
        private Dictionary<string, int> _devicesId;
        private Dictionary<string, int> _departmentsId;

        private List<string> _devicesList;
        public List<string> DevicesList
        {
            get { return _devicesList; }
            set
            {
                _devicesList = value;
                NotifyOfPropertyChange(() => DevicesList);
            }
        }

        private int _selectedDeviceIdx;
        public int SelectedDeviceIdx
        {
            get { return _selectedDeviceIdx; }
            set
            {
                _selectedDeviceIdx = value;
                NotifyOfPropertyChange(() => SelectedDeviceIdx);
            }
        }

        private List<string> _departmentsList;
        public List<string> DepartmentsList
        {
            get { return _departmentsList; }
            set
            {
                _departmentsList = value;
                NotifyOfPropertyChange(() => DepartmentsList);
            }
        }

        private int _selectedDepartmentIdx;
        public int SelectedDepartmentIdx
        {
            get { return _selectedDepartmentIdx; }
            set
            {
                _selectedDepartmentIdx = value;
                NotifyOfPropertyChange(() => SelectedDepartmentIdx);
            }
        }

        public int DeviceId => _devicesId[_devicesList[_selectedDeviceIdx]];

        public int DeptId => _departmentsId[_departmentsList[_selectedDepartmentIdx]];

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                NotifyOfPropertyChange(() => Date);
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        public void RefreshData()
        {
            _devicesId = _model.GetDevices();
            _departmentsId = _model.GetDepartments();

            _devicesList = new List<string>();
            foreach (var device in _devicesId)
                _devicesList.Add(device.Key);
            _selectedDeviceIdx = 0;

            _departmentsList = new List<string>();
            foreach (var department in _departmentsId)
                _departmentsList.Add(department.Key);
            _selectedDepartmentIdx = 0;
            
            _date = DateTime.Today;
            _description = "";

            Refresh();
        }

        public void AddTransfer()
        {
            _model.AddTransfer(
                AddNewTransferModel.TransfersTypes.Move,
                DeviceId,
                DeptId,
                Date,
                0,
                Description);
            MessageBox.Show("Трансфер успішно додано.", "Успіх!");
            Date = DateTime.Today;
            Description = "";
            SelectedDeviceIdx = 0;
            SelectedDepartmentIdx = 0;

            TransferAdded?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler TransferAdded;
    }
}
