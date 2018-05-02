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
    class AttributesViewModel : Screen
    {
        public AttributesViewModel(AttributesModel model)
        {
            DisplayName = "Параметри пристроїв";
            _model = model;
            _title = _model.PermissionLevel > 2 ? "Характеристики пристроїв компанії" : "Параметри пристроїв вашого відділу";
            var res = _model.GetAttributesTable();
            
            res.Columns[0].ColumnName = "Тип пристрою";
            res.Columns[1].ColumnName = "Серійний номер";
            res.Columns[2].ColumnName = "Відділ";
            res.Columns[3].ColumnName = "Параметр";
            res.Columns[4].ColumnName = "Значення";
            res.Columns[5].ColumnName = "Одиниці вимірювання";

            if (model.PermissionLevel < 3)
                res.Columns.RemoveAt(2);

            _attributesTable = res;
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
    }
}
