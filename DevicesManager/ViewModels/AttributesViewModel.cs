using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using DevicesManager.Models;

namespace DevicesManager.ViewModels
{
    class AttributesViewModel : Screen
    {
        public AttributesViewModel(AttributesModel model)
        {
            DisplayName = "Параметри пристроїв";
            _model = model;
            _deviceId = 1;
            RefreshData();
        }

        private AttributesModel _model;

        private DataTable _attributesTable;
        public DataTable AttributesTable
        {
            get { return _attributesTable; }
            private set
            {
                _attributesTable = value;
                NotifyOfPropertyChange(() => AttributesTable);
            }
        }

        private int _deviceId;
        public int DeviceId
        {
            get { return _deviceId; }
            set
            {
                _deviceId = value; 
                NotifyOfPropertyChange(() => DeviceId);
            }
        }

        public bool IsNormal => _model.GetDeviceStatus(DeviceId).Equals(1);

        public bool IsBroken => _model.GetDeviceStatus(DeviceId).Equals(3);

        public void RefreshData()
        {
            var res = _model.GetAttributesTable(DeviceId);

            res.Columns[0].ColumnName = "Параметр";
            res.Columns[1].ColumnName = "Значення";
            res.Columns[2].ColumnName = "Одиниці вимірювання";

            _attributesTable = res;
            Refresh();
        }

        public void SetIsBroken()
        {
            if (MessageBox.Show("Встановити статус обраного пристрою як \"Потребує ремонту\"?", "Підтвердження.",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                _model.SetDeviceIsBroken(DeviceId);
                DataChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void SetCannotRestore()
        {
            if (MessageBox.Show("Встановити статус обраного пристрою як \"Не підлягає ремонту\"?", "Підтвердження.",
                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                _model.SetDeviceCannotRestore(DeviceId);
                DataChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler DataChanged;
    }
}
