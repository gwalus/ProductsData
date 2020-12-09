using MahApps.Metro.Controls.Dialogs;
using ShoesApp.Commands;
using ShoesApp.Data;
using ShoesApp.Helpers;
using ShoesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoesApp.ViewModel
{
    public class AddProductViewModel : BaseViewModel
    {
        #region Fields
        private readonly IDataRepository _dataRepository;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly ProductsViewModel _productsViewModel;
        #endregion

        #region Properties
        private Product product;
        public Product Product
        {
            get { return product; }
            set
            {
                product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        public List<string> ListOfBrands { get; set; } = StaticLists.listOfBrands;
        private object brandSelected = StaticLists.listOfBrands.ElementAt(0);
        public object BrandSelected
        {
            get { return brandSelected; }
            set
            {
                brandSelected = value;
                OnPropertyChanged(nameof(BrandSelected));
                SetProduct();
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
                SetProduct();
            }
        }

        private string productCode;
        public string ProductCode
        {
            get { return productCode; }
            set
            {
                productCode = value.ToUpper();
                OnPropertyChanged(nameof(ProductCode));
                SetProduct();
            }
        }

        private string color;
        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged(nameof(Color));
                SetProduct();
            }
        }

        private string size;
        public string Size
        {
            get { return size; }
            set
            {
                size = value;
                OnPropertyChanged(nameof(Size));
                SetProduct();
            }
        }

        private bool box;
        public bool Box
        {
            get { return box; }
            set
            {
                box = value;
                OnPropertyChanged(nameof(Box));
                SetProduct();
            }
        }

        public List<string> ListOfSources { get; set; } = StaticLists.listOfSources;
        private object sourceSelected = StaticLists.listOfSources.ElementAt(0);

        public object SourceSelected
        {
            get { return sourceSelected; }
            set
            {
                sourceSelected = value;
                OnPropertyChanged(nameof(SourceSelected));
                SetProduct();
            }
        }

        private DateTime dateOfPurchase = DateTime.Today;
        public DateTime DateOfPurchase
        {
            get { return dateOfPurchase; }
            set
            {
                dateOfPurchase = value;
                OnPropertyChanged(nameof(DateOfPurchase));
                SetProduct();
            }
        }

        private double purchasePrice = 100.00;
        public double PurchasePrice
        {
            get { return purchasePrice; }
            set
            {
                purchasePrice = value;
                OnPropertyChanged(nameof(PurchasePrice));
                SetProduct();
            }
        }
        #endregion

        #region Commands
        public AddNewProductCommand AddNewProductCommand { get; set; }
        #endregion

        #region Constructor
        //public AddProductViewModel(IDataRepository dataRepository, IDialogCoordinator dialogCoordinator)
        public AddProductViewModel(ProductsViewModel productsViewModel, IDataRepository dataRepository, IDialogCoordinator dialogCoordinator)
        {
            _dataRepository = dataRepository;
            _dialogCoordinator = dialogCoordinator;
            _productsViewModel = productsViewModel;
            AddNewProductCommand = new AddNewProductCommand(this);
        }
        #endregion

        #region Methods
        private void SetProduct()
        {
            Product = new Product()
            {
                Brand = BrandSelected.ToString(),
                Name = Name,
                ProductCode = ProductCode,
                Color = Color,
                Size = Size,
                Box = Box,
                Source = SourceSelected.ToString(),
                DateOfPurchase = DateOfPurchase.ToShortDateString(),
                PurchasePrice = PurchasePrice,
                IsSold = false
            };
        }

        private void SetDefaultProperties()
        {
            BrandSelected = ListOfBrands.ElementAt(0);
            Name = string.Empty;
            ProductCode = string.Empty;
            Color = string.Empty;
            Size = string.Empty;
            Box = false;
            SourceSelected = ListOfSources.ElementAt(0);
            DateOfPurchase = DateTime.Today;
            PurchasePrice = 100.00;
        }

        public async void AddProduct(Product product)
        {
            if (product is not null)
            {
                var dialogResult = await _dialogCoordinator.ShowMessageAsync(this, "Adding new product", "Are you sure you want to add this product?", MessageDialogStyle.AffirmativeAndNegative);

                if (dialogResult == MessageDialogResult.Affirmative)
                {
                    if (await _dataRepository.Add(product))
                    {
                        await _dialogCoordinator.ShowMessageAsync(this, "Informaction", "Product has been successfully added", MessageDialogStyle.Affirmative);
                        SetDefaultProperties();
                    }
                    else await _dialogCoordinator.ShowMessageAsync(this, "Informaction", "Cannot add this product", MessageDialogStyle.Affirmative);
                }

                _productsViewModel.GetProducts();
            }
            else await _dialogCoordinator.ShowMessageAsync(this, "Error", "Product cannot be empty!", MessageDialogStyle.Affirmative);
        }
        #endregion
    }
}
