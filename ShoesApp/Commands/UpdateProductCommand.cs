using ShoesApp.Models;
using ShoesApp.ViewModel;
using System;
using System.Windows.Input;

namespace ShoesApp.Commands
{
    public class UpdateProductCommand : ICommand
    {
        private readonly UpdateProductViewModel _viewModel;

        public UpdateProductCommand(UpdateProductViewModel viewModel)
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
            if (parameter != null)
            {
                var product = parameter as Product;
                if(product.Id > 0)
                    _viewModel.UpdateProduct(product);
            }
        }
    }
}
