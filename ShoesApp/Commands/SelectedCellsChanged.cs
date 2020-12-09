using ShoesApp.ViewModel;
using System;
using System.Windows.Input;

namespace ShoesApp.Commands
{
    public class SelectedCellsChanged : ICommand
    {
        ProductsViewModel _viewModel;

        public SelectedCellsChanged(ProductsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.SelectedProductChanged();
        }
    }
}
