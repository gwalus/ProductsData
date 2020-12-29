using ShoesApp.Models;
using ShoesApp.ViewModel;
using System;
using System.Windows.Input;

namespace ShoesApp.Commands
{
    public class SearchProductInGoogleCommand : ICommand
    {
        private readonly ProductsViewModel _viewModel;

        public SearchProductInGoogleCommand(ProductsViewModel viewModel)
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
            if(parameter != null)
            {
                var product = parameter as Product;               
                string url = $"https://www.google.com/search?q={product.Brand}+{product.ProductCode}";

                _viewModel.SearchProductInGoogle(url);
            }
        }
    }
}
