using MahApps.Metro.Controls.Dialogs;
using ShoesApp.Commands;
using ShoesApp.Data;
using ShoesApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace ShoesApp.ViewModel
{
    public class ProductsViewModel : BaseViewModel
    {
        #region Fields
        private readonly IDataRepository _dataRepository;
        private readonly IDialogCoordinator _dialogCoordinator;
        #endregion

        private bool _addProductPanelVisible;

        public bool AddProductPanelVisible
        {
            get { return _addProductPanelVisible; }
            set
            {
                _addProductPanelVisible = value;
                OnPropertyChanged(nameof(AddProductPanelVisible));
            }
        }

        private bool _updatingProductPanelVisible;

        public bool UpdatingProductPanelVisible
        {
            get { return _updatingProductPanelVisible; }
            set
            {
                _updatingProductPanelVisible = value;
                OnPropertyChanged(nameof(UpdatingProductPanelVisible));
            }
        }


        private bool _updateProductPanelVisible;

        public bool UpdateProductPanelVisible
        {
            get { return _updateProductPanelVisible; }
            set { _updateProductPanelVisible = value; OnPropertyChanged(nameof(UpdateProductPanelVisible)); }
        }


        #region Properties
        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        #endregion

        #region Commands
        public GetProductsCommand GetProductsCommand { get; set; }
        public GetAvailableProductsCommand GetAvailableProductsCommand { get; set; }
        public GetSoldProductsCommand GetSoldProductsCommand { get; set; }
        public ShowUpdatingProductPanelCommand ShowUpdatingProductPanelCommand { get; set; }
        public ShowNewProductPanelCommand ShowNewProductPanelCommand { get; set; }
        public SelectedCellsChanged SelectedCellsChanged { get; set; }
        public SearchProductInGoogleCommand SearchProductInGoogleCommand { get; set; }
        #endregion

        #region ViewModels
        private TotalsViewModel totalsViewModel;

        public TotalsViewModel TotalsViewModel
        {
            get { return totalsViewModel; }
            set
            {
                totalsViewModel = value;
                OnPropertyChanged(nameof(TotalsViewModel));
            }
        }

        public AddProductViewModel AddProductViewModel { get; set; }
        private UpdateProductViewModel _updateProductViewModel;
        public UpdateProductViewModel UpdateProductViewModel
        {
            get { return _updateProductViewModel; }
            set
            {
                _updateProductViewModel = value;
                OnPropertyChanged(nameof(UpdateProductViewModel));
            }
        }
        #endregion

        #region Constructor
        public ProductsViewModel(IDataRepository dataRepository, IDialogCoordinator dialogCoordinator)
        {
            _dataRepository = dataRepository;
            _dialogCoordinator = dialogCoordinator;
            GetProductsCommand = new GetProductsCommand(this);
            GetAvailableProductsCommand = new GetAvailableProductsCommand(this);
            GetSoldProductsCommand = new GetSoldProductsCommand(this);
            ShowNewProductPanelCommand = new ShowNewProductPanelCommand(this);
            ShowUpdatingProductPanelCommand = new ShowUpdatingProductPanelCommand(this);
            SelectedCellsChanged = new SelectedCellsChanged(this);
            SearchProductInGoogleCommand = new SearchProductInGoogleCommand(this);
            AddProductViewModel = new AddProductViewModel(this, dataRepository, dialogCoordinator);
        }
        #endregion

        #region Methods
        public async void GetProducts()
        {
            var controller = await _dialogCoordinator.ShowProgressAsync(this, "Wait", "Loading products...");
            controller.SetIndeterminate();

            var products = await _dataRepository.GetProducts();
            SetTotalsViewModel(products);
            Products = new ObservableCollection<Product>(products);

            await controller.CloseAsync();
        }

        public async void GetAvailableProducts()
        {
            var controller = await _dialogCoordinator.ShowProgressAsync(this, "Wait", "Loading products...");
            controller.SetIndeterminate();

            var products = await _dataRepository.GetProducts();
            var availableProducts = products.Where(product => product.IsSold == false).ToList();

            SetTotalsViewModel(availableProducts);
            Products = new ObservableCollection<Product>(availableProducts);

            await controller.CloseAsync();
        }

        public async void GetSoldProducts()
        {
            var controller = await _dialogCoordinator.ShowProgressAsync(this, "Wait", "Loading products...");
            controller.SetIndeterminate();

            var products = await _dataRepository.GetProducts();
            var soldProducts = products.Where(product => product.IsSold).ToList();

            SetTotalsViewModel(soldProducts);
            Products = new ObservableCollection<Product>(soldProducts);

            await controller.CloseAsync();
        }

        private void SetTotalsViewModel(List<Product> products)
        {
            TotalsViewModel = new TotalsViewModel(products);
        }

        public void SelectedProductChanged()
        {
            UpdateProductViewModel = new UpdateProductViewModel(this, _dataRepository, _dialogCoordinator, _selectedProduct);
        }

        public void ShowAddingProductPanel()
        {
            if (AddProductPanelVisible)
                AddProductPanelVisible = false;
            else AddProductPanelVisible = true;
        }

        public void ShowUpdatingProductPanel()
        {
            if (UpdatingProductPanelVisible)
                UpdatingProductPanelVisible = false;
            else UpdatingProductPanelVisible = true;
        }

        public void SearchProductInGoogle(string url)
        {
            //Process.Start(wwwPath);

            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
        #endregion
    }
}
