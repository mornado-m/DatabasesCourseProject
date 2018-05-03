﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using DevicesManager.Models;

namespace DevicesManager.ViewModels
{
    class AddNewTransferViewModel : Screen
    {
        public AddNewTransferViewModel(AddNewTransferModel model)
        {
            DisplayName = "Оформити трансфер";
            _model = model;
            RefreshData();
        }

        private AddNewTransferModel _model;
        private Dictionary<string, int> _devicesId;
        private Dictionary<string, int> _departmentsId;
        private Dictionary<string, AddNewTransferModel.TransfersTypes> _transfersTypes;

        private List<string> _transferTypesList;
        public List<string> TransferTypesList
        {
            get { return _transferTypesList; }
            set
            {
                _transferTypesList = value;
                NotifyOfPropertyChange(() => TransferTypesList);
            }
        }

        private int _selectedTypeIdx;
        public int SelectedTypeIdx
        {
            get { return _selectedTypeIdx; }
            set
            {
                if (value < 0 || value >= TransferTypesList.Count)
                    return;
                _selectedTypeIdx = value; 
                NotifyOfPropertyChange(() => SelectedTypeIdx);
                NotifyOfPropertyChange(() => IsMoveTransfer);
            }
        }

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

                _transfersTypes = _model.GetTransfersTypes(_devicesId[_devicesList[_selectedDeviceIdx]]);
                _transferTypesList = new List<string>();
                foreach (var transfer in _transfersTypes)
                    _transferTypesList.Add(transfer.Key);
                _selectedTypeIdx = 0;

                NotifyOfPropertyChange(() => TransferTypesList);
                NotifyOfPropertyChange(() => SelectedTypeIdx);
                NotifyOfPropertyChange(() => IsMoveTransfer);
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

        public bool IsMoveTransfer => _transfersTypes[_transferTypesList[_selectedTypeIdx]]
            .Equals(AddNewTransferModel.TransfersTypes.Move);
        
        public int DeviceId => _devicesId[_devicesList[_selectedDeviceIdx]];
        
        public int DeptId => _departmentsId[_departmentsList[_selectedDepartmentIdx]];

        private decimal _cost;
        public decimal Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                NotifyOfPropertyChange(() => Cost);
            }
        }

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

            _transfersTypes = _model.GetTransfersTypes(_devicesId[_devicesList[_selectedDeviceIdx]]);
            _transferTypesList = new List<string>();
            foreach (var transfer in _transfersTypes)
                _transferTypesList.Add(transfer.Key);
            _selectedTypeIdx = 0;

            _cost = 0;
            _date = DateTime.Today;
            _description = "";

            Refresh();
        }

        public void AddTransfer()
        {
            _model.AddTransfer(
                _transfersTypes[_transferTypesList[_selectedTypeIdx]],
                DeviceId,
                DeptId,
                Date,
                Cost,
                Description);
            MessageBox.Show("Трансфер успішно додано.", "Успіх!");
            Cost = 0;
            Date = DateTime.Today;
            Description = "";
            SelectedDeviceIdx = 0;
            SelectedDepartmentIdx = 0;
            SelectedTypeIdx = 0;

            TransferAdded?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler TransferAdded;
    }
}
