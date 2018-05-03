using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DevicesManager.Models;

namespace DevicesManager.ViewModels
{
    class DevicesViewModel : Conductor<object>
    {
        public DevicesViewModel(DevicesModel model)
        {
            DisplayName = "Пристрої";
            _model = model;
            _title = _model.PermissionLevel > 2 ? "Пристрої компанії" : "Пристрої вашого відділу";
            _deviceAttributes = new AttributesViewModel(new AttributesModel(_model.UserId, _model.PermissionLevel));
            _deviceAttributes.DataChanged += (sender, args) => RefreshData();
            ActivateItem(_deviceAttributes);
            RefreshData();
        }

        private DataTable _startDevicesTable;
        private DevicesModel _model;
        private AttributesViewModel _deviceAttributes;

        private DataTable _devicesTable;
        public DataTable DevicesTable
        {
            get { return _devicesTable; }
            private set
            {
                _devicesTable = value; 
                NotifyOfPropertyChange(() => DevicesTable);
            }
        }

        private int _selectedRowIdx;
        public int SelectedRowIdx
        {
            get { return _selectedRowIdx; }
            set
            {
                if (value < 0 || value >= _startDevicesTable.Rows.Count)
                    return;
                _selectedRowIdx = value;
                _deviceAttributes.DeviceId = (int) _startDevicesTable.Rows[_selectedRowIdx][0];
                _deviceAttributes.RefreshData();

                NotifyOfPropertyChange(() => SelectedRowIdx);
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value; 
                NotifyOfPropertyChange(() => Title);
            }
        }

        public void RefreshData()
        {
            var res = _model.GetDevicesTable();
            _startDevicesTable = res.Clone();
            foreach (DataRow row in res.Rows)
            {
                var nrow = _startDevicesTable.NewRow();
                for (int i = 0; i < res.Columns.Count; i++)
                    nrow[i] = row[i];
                _startDevicesTable.Rows.Add(nrow);
            }

            res.Columns[1].ColumnName = "Тип пристрою";
            res.Columns[2].ColumnName = "Статус пристрою";
            res.Columns[3].ColumnName = "Відділ";
            res.Columns[4].ColumnName = "Серійний номер";

            res.Columns.RemoveAt(6);
            res.Columns.RemoveAt(5);
            res.Columns.Add("Дата виробництва", typeof(string));
            res.Columns.Add("Вартість", typeof(decimal));

            for (int i = 0; i < res.Rows.Count; i++)
            {
                res.Rows[i][5] = ((DateTime)_startDevicesTable.Rows[i][5]).ToShortDateString();
                res.Rows[i][6] = Math.Round((decimal)_startDevicesTable.Rows[i][6], 2);
            }

            if (_model.PermissionLevel < 3)
                res.Columns.RemoveAt(3);
            res.Columns.RemoveAt(0);

            _devicesTable = res;
            Refresh();
            _deviceAttributes.RefreshData();
        }
    }
}
