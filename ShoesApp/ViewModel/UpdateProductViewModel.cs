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
    public class UpdateProductViewModel : BaseViewModel
    {
        #region Fields
        private readonly IDataRepository _repo;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly Product _selectedProduct;
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

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
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
                if (value == null)
                    productCode = value;
                else productCode = value.ToUpper();
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
                if(value != null) sourceSelected = value.ToString();
                OnPropertyChanged(nameof(SourceSelected));
                SetProduct();
            }
        }

        private DateTime dateOfPurchase;
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

        private double purchasePrice;
        public double PurchasePrice
        {
            get { return purchasePrice; }
            set
            {
                purchasePrice = value;
                OnPropertyChanged(nameof(PurchasePrice));
                Profit = PriceWithoutShipping - PurchasePrice;
                SetProduct();
            }
        }

        private DateTime? saleDate;
        public DateTime? SaleDate
        {
            get { return saleDate; }
            set
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    saleDate = null;
                }
                else
                {
                    saleDate = value;   
                }
                OnPropertyChanged(nameof(SaleDate));
                SetProduct();
            }
        }

        private double sellingPrice;

        public double SellingPrice
        {
            get { return sellingPrice; }
            set
            {
                sellingPrice = value;
                OnPropertyChanged(nameof(SellingPrice));
                PriceWithoutShipping = SellingPrice - ShippingPrice;
                SetProduct();
            }
        }


        private double shippingPrice;

        public double ShippingPrice
        {
            get { return shippingPrice; }
            set
            {
                shippingPrice = value;
                OnPropertyChanged(nameof(ShippingPrice));
                PriceWithoutShipping = SellingPrice - ShippingPrice;
                SetProduct();
            }
        }

        private double priceWithoutShipping;

        public double PriceWithoutShipping
        {
            get { return priceWithoutShipping; }
            set
            {
                priceWithoutShipping = value;
                Profit = PriceWithoutShipping - PurchasePrice;
            }
        }

        private double profit;

        public double Profit
        {
            get { return profit; }
            set
            {
                profit = Math.Round(value, 2);
            }
        }

        private bool isSold;

        public bool IsSold
        {
            get { return isSold; }
            set
            {
                isSold = value;
                OnPropertyChanged(nameof(IsSold));
                SetProduct();
            }
        }
        #endregion

        #region Commands
        public UpdateProductCommand UpdateProductCommand { get; set; }
        #endregion

        #region Constructor
        public UpdateProductViewModel(ProductsViewModel productsViewModel, IDataRepository repo, IDialogCoordinator dialogCoordinator, Product selectedProduct)
        {
            _repo = repo;
            _dialogCoordinator = dialogCoordinator;
            _selectedProduct = selectedProduct;
            _productsViewModel = productsViewModel;
            UpdateProductCommand = new UpdateProductCommand(this);
            SetProperties();
        }
        #endregion

        private void SetProperties()
        {
            if (_selectedProduct != null)
            {
                Id = _selectedProduct.Id;
                BrandSelected = _selectedProduct.Brand;
                Name = _selectedProduct.Name;
                ProductCode = _selectedProduct.ProductCode;
                Color = _selectedProduct.Color;
                Size = _selectedProduct.Size;
                Box = _selectedProduct.Box.GetValueOrDefault();
                SourceSelected = _selectedProduct.Source;
                DateOfPurchase = DateTime.Parse(_selectedProduct.DateOfPurchase);
                PurchasePrice = _selectedProduct.PurchasePrice;               
                if (string.IsNullOrWhiteSpace(_selectedProduct.SaleDate))
                    SaleDate = null;
                else
                    SaleDate = DateTime.Parse(_selectedProduct.SaleDate);
                SellingPrice = _selectedProduct.SellingPrice.GetValueOrDefault();
                ShippingPrice = _selectedProduct.ShippingPrice.GetValueOrDefault();
                PriceWithoutShipping = _selectedProduct.PriceWithoutShipping.GetValueOrDefault();
                Profit = _selectedProduct.Profit.GetValueOrDefault();
                IsSold = _selectedProduct.IsSold;
            }
        }

        private void SetProduct()
        {
            Product = new Product()
            {
                Id = Id,
                Brand = BrandSelected.ToString(),
                Name = Name,
                ProductCode = ProductCode,
                Color = Color,
                Size = Size,
                Box = Box,
                Source = SourceSelected.ToString(),
                DateOfPurchase = DateOfPurchase.ToShortDateString(),
                PurchasePrice = PurchasePrice,
                SaleDate = SaleDate.ToString(),
                SellingPrice = SellingPrice,
                ShippingPrice = ShippingPrice,
                PriceWithoutShipping = PriceWithoutShipping,
                Profit = Profit,
                IsSold = IsSold
            };
        }

        #region Methods
        public async void UpdateProduct(Product product)
        {
            if (await _repo.Update(product))
            {
                await _dialogCoordinator.ShowMessageAsync(this, "Informaction", "Product has been updated", MessageDialogStyle.Affirmative);
                _productsViewModel.GetProducts();
            }
            else await _dialogCoordinator.ShowMessageAsync(this, "Informaction", "Cannot update this product", MessageDialogStyle.Affirmative);
        }
        #endregion
    }
}
