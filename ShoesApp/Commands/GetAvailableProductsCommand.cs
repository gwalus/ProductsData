using ShoesApp.ViewModel;
using System;
using System.Windows.Input;

namespace ShoesApp.Commands
{
    public class GetAvailableProductsCommand : ICommand
    {
        private readonly ProductsViewModel _viewModel;

        public GetAvailableProductsCommand(ProductsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.GetAvailableProducts();
        }

        public event EventHandler CanExecuteChanged;
    }
}
