using ShoesApp.ViewModel;
using System;
using System.Windows.Input;

namespace ShoesApp.Commands
{
    public class GetProductsCommand : ICommand
    {
        private readonly ProductsViewModel _viewModel;

        public GetProductsCommand(ProductsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.GetProducts();
        }

        public event EventHandler CanExecuteChanged;
    }
}
