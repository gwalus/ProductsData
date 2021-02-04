using ShoesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoesApp.Commands
{
    public class TestCommand : ICommand
    {
        private readonly ProductsViewModel _vm;

        public TestCommand(ProductsViewModel vm)
        {
            _vm = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _vm.Testowy();
        }
    }
}
