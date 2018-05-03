using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DevicesManager.Models;

namespace DevicesManager.ViewModels
{
    class TransfersViewModel : Screen
    {
        public TransfersViewModel(TransfersModel model)
        {
            DisplayName = "Переміщення";
            _model = model;
            _title = _model.PermissionLevel > 2 ? "Переміщення всіх пристроїв компанії" : "Переміщення пристроїв вашого відділу";
            RefreshData();
        }

        private DataTable _startTransfersTable;
        private TransfersModel _model;

        private DataTable _transfersTable;
        public DataTable TransfersTable
        {
            get { return _transfersTable; }
            private set
            {
                _transfersTable = value;
                NotifyOfPropertyChange(() => TransfersTable);
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

        public string Description => _selectedRowIdx != -1 ? (string) _startTransfersTable.Rows[_selectedRowIdx][10] : "";

        private int _selectedRowIdx;
        public int SelectedRowIdx
        {
            get { return _selectedRowIdx; }
            set
            {
                if (value < 0 || value >= _startTransfersTable.Rows.Count)
                    return;
                _selectedRowIdx = value;
                NotifyOfPropertyChange(() => SelectedRowIdx);
                NotifyOfPropertyChange(() => Description);
            }
        }

        public void RefreshData()
        {
            var res = _model.GetTransfersTable();
            _startTransfersTable = res.Clone();
            foreach (DataRow row in res.Rows)
            {
                var nrow = _startTransfersTable.NewRow();
                for (int i = 0; i < res.Columns.Count; i++)
                    nrow[i] = row[i];
                _startTransfersTable.Rows.Add(nrow);
            }

            res.Columns[1].ColumnName = "Тип пристрою";
            res.Columns[2].ColumnName = "Серійний номер";
            res.Columns[3].ColumnName = "Тип трансферу";
            res.Columns[5].ColumnName = "Відділ-джерело";
            res.Columns[7].ColumnName = "Відділ призначення";

            res.Columns.RemoveAt(10);
            res.Columns.RemoveAt(9);
            res.Columns.RemoveAt(8);
            res.Columns.Add("Дата проведення", typeof(string));
            res.Columns.Add("Вартість проведення", typeof(decimal));

            for (int i = 0; i < res.Rows.Count; i++)
            {
                res.Rows[i][8] = ((DateTime)_startTransfersTable.Rows[i][8]).ToShortDateString();
                res.Rows[i][9] = Math.Round((decimal)_startTransfersTable.Rows[i][9], 2);
            }

            res.Columns.RemoveAt(6);
            res.Columns.RemoveAt(4);
            res.Columns.RemoveAt(0);

            _transfersTable = res;
            Refresh();
        }
    }
}
