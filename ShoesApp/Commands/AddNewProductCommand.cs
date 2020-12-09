using ShoesApp.Models;
using ShoesApp.ViewModel;
using System;
using System.Windows.Input;

namespace ShoesApp.Commands
{
    public class AddNewProductCommand : ICommand
    {
        private readonly AddProductViewModel _viewModel;

        public AddNewProductCommand(AddProductViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var product = parameter as Product;
            _viewModel.AddProduct(product);
        }

        public event EventHandler CanExecuteChanged;
    }
}
