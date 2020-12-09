using ShoesApp.ViewModel;
using System;
using System.Windows.Input;

namespace ShoesApp.Commands
{
    public class GetSoldProductsCommand : ICommand
    {
        private readonly ProductsViewModel _viewModel;

        public GetSoldProductsCommand(ProductsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.GetSoldProducts();
        }

        public event EventHandler CanExecuteChanged;
    }
}
