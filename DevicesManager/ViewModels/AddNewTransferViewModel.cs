using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace DevicesManager.ViewModels
{
    class AddNewTransferViewModel : Conductor<object>
    {
        public AddNewTransferViewModel()
        {
            DisplayName = "Оформити трансфер";
        }

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
                _selectedTypeIdx = value; 
                NotifyOfPropertyChange(() => SelectedTypeIdx);
            }
        }


        public BindableCollection<IScreen> Itm { get; set; } = new BindableCollection<IScreen>();

        public void Select(object dataContext)
        {
            if (dataContext is IScreen vm && Itm.Contains(vm))
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
