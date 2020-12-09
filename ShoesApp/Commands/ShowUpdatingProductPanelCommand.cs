using ShoesApp.Models;
using ShoesApp.ViewModel;
using System;
using System.Windows.Input;

namespace ShoesApp.Commands
{
    public class ShowUpdatingProductPanelCommand : ICommand
    {
        private readonly ProductsViewModel _viewModel;

        public ShowUpdatingProductPanelCommand(ProductsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.ShowUpdatingProductPanel();
        }

        public event EventHandler CanExecuteChanged;
    }
}
