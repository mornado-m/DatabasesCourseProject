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
    class AddNewDeviceViewModel : Screen
    {
        public AddNewDeviceViewModel(AddNewDeviceModel model)
        {
            DisplayName = "Зареєструвати новий пристрій";
            _model = model;
            
            _devicesTypesId = _model.GetDevicesTypes();
            _devicesTypesList = new List<string>();
            foreach (var types in _devicesTypesId)
                _devicesTypesList.Add(types.Key);
            _selectedTypeIdx = 0;

            _departmentsId = _model.GetDepartments();
            _departmentsList = new List<string>();
            foreach (var dep in _departmentsId)
                _departmentsList.Add(dep.Key);
            _selectedDepartmentIdx = 0;

            _attributesValues = new Dictionary<string, string>();
            var attrsInfo = _model.GetAttributesTypes(_devicesTypesId[_devicesTypesList[_selectedTypeIdx]]);
            _attributesTypesId = attrsInfo.Item1;
            _attributesMeasurements = attrsInfo.Item2;

            _attributesList = new List<string>();
            foreach (var type in _attributesTypesId)
                _attributesList.Add(type.Key);
            _selectedAttributeIdx = 0;

            _devCost = 0;
            _cost = 0;
            _prodDate = DateTime.Today;
            _tranDate = DateTime.Today;
        }

        private AddNewDeviceModel _model;
        private Dictionary<string, int> _devicesTypesId;
        private Dictionary<string, int> _departmentsId;
        private Dictionary<string, int> _attributesTypesId;
        private Dictionary<string, string> _attributesMeasurements;
        private Dictionary<string, string> _attributesValues;

        private List<string> _devicesTypesList;
        public List<string> DevicesTypesList
        {
            get { return _devicesTypesList; }
            set
            {
                _devicesTypesList = value;
                NotifyOfPropertyChange(() => DevicesTypesList);
            }
        }

        private int _selectedTypeIdx;
        public int SelectedTypeIdx
        {
            get { return _selectedTypeIdx; }
            set
            {
                if (value < 0 || value >= DevicesTypesList.Count)
                    return;
                _selectedTypeIdx = value;
                
                _attributesValues = new Dictionary<string, string>();
                var attrsInfo = _model.GetAttributesTypes(_devicesTypesId[_devicesTypesList[_selectedTypeIdx]]);
                _attributesTypesId = attrsInfo.Item1;
                _attributesMeasurements = attrsInfo.Item2;

                _attributesList = new List<string>();
                foreach (var type in _attributesTypesId)
                    _attributesList.Add(type.Key);
                _selectedAttributeIdx = 0;
                _attrVal = "";

                NotifyOfPropertyChange(() => SelectedTypeIdx);
                NotifyOfPropertyChange(() => AttributesList);
                NotifyOfPropertyChange(() => AttributesText);
                NotifyOfPropertyChange(() => AttrVal);
            }
        }

        private List<string> _attributesList;
        public List<string> AttributesList
        {
            get { return _attributesList; }
            set
            {
                _attributesList = value;
                NotifyOfPropertyChange(() => AttributesList);
            }
        }

        private int _selectedAttributeIdx;
        public int SelectedAttributeIdx
        {
            get { return _selectedAttributeIdx; }
            set
            {
                _selectedAttributeIdx = value;
                NotifyOfPropertyChange(() => SelectedAttributeIdx);
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

        private string _attrVal;
        public string AttrVal
        {
            get { return _attrVal; }
            set
            {
                _attrVal = value;
                NotifyOfPropertyChange(() => AttrVal);
            }
        }
        
        public string AttributesText
        {
            get
            {
                var res = "";
                foreach (var attribute in _attributesValues)
                    res += $"{attribute.Key}: {attribute.Value} {_attributesMeasurements[attribute.Key]}{Environment.NewLine}";
                return res;
            }
        }

        public bool IsSuperAdmin => _model.PermissionLevel > 2;

        private decimal _devCost;
        public decimal DevCost
        {
            get { return _devCost; }
            set
            {
                _devCost = value;
                NotifyOfPropertyChange(() => DevCost);
            }
        }

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

        private int _serialNum;
        public int SerialNum
        {
            get { return _serialNum; }
            set
            {
                _serialNum = value;
                NotifyOfPropertyChange(() => SerialNum);
            }
        }

        private DateTime _prodDate;
        public DateTime ProdDate
        {
            get { return _prodDate; }
            set
            {
                _prodDate = value;
                NotifyOfPropertyChange(() => ProdDate);
            }
        }

        private DateTime _tranDate;
        public DateTime TranDate
        {
            get { return _tranDate; }
            set
            {
                _tranDate = value;
                NotifyOfPropertyChange(() => TranDate);
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

        public void AddDevice()
        {
            var attrs = new Dictionary<int, string>();
            foreach (var attribute in _attributesValues)
                attrs.Add(_attributesTypesId[attribute.Key], attribute.Value);

            _model.AddDevice(
                _devicesTypesId[_devicesTypesList[_selectedTypeIdx]],
                _model.PermissionLevel < 3 ? _model.UserDepartmentId : _departmentsId[_departmentsList[_selectedDepartmentIdx]],
                DevCost,
                SerialNum,
                ProdDate,
                Description,
                Cost,
                TranDate,
                attrs);

            MessageBox.Show("Пристрій успішно додано.", "Успіх!");

            AttrVal = "";
            SerialNum = 0;
            DevCost = 0;
            Cost = 0;
            ProdDate = DateTime.Today;
            TranDate = DateTime.Today;
            Description = "";
            SelectedTypeIdx = 0;
            SelectedAttributeIdx = 0;
            SelectedDepartmentIdx = 0;
            
            DeviceAdded?.Invoke(this, EventArgs.Empty);
        }

        public void AddAttribute()
        {
            if (_attributesValues.ContainsKey(_attributesList[_selectedAttributeIdx]))
                _attributesValues[_attributesList[_selectedAttributeIdx]] = AttrVal;
            else
                _attributesValues.Add(_attributesList[_selectedAttributeIdx], AttrVal);
            AttrVal = "";
            SelectedAttributeIdx = 0;
            NotifyOfPropertyChange(() => AttributesText);
        }

        public void CleareAttributes()
        {
            _attributesValues.Clear();
            AttrVal = "";
            SelectedAttributeIdx = 0;
            NotifyOfPropertyChange(() => AttributesText);
        }

        public event EventHandler DeviceAdded;
    }
}
