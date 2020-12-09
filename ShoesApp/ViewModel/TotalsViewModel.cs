using ShoesApp.Data;
using ShoesApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoesApp.ViewModel
{
    public class TotalsViewModel : BaseViewModel
    {
        private readonly IDataRepository _dataRepository;

        #region Properties
        private int quantityTotal;

        public int QuantityTotal
        {
            get { return quantityTotal; }
            set { quantityTotal = value; OnPropertyChanged(nameof(QuantityTotal)); }
        }

        private double purchaseTotal;

        public double PurchaseTotal
        {
            get { return purchaseTotal; }
            set { purchaseTotal = value; OnPropertyChanged(nameof(PurchaseTotal)); }
        }

        private double sellTotal;

        public double SellTotal
        {
            get { return sellTotal; }
            set { sellTotal = value; OnPropertyChanged(nameof(SellTotal)); }
        }

        private double shipTotal;

        public double ShipTotal
        {
            get { return shipTotal; }
            set { shipTotal = value; OnPropertyChanged(nameof(ShipTotal)); }
        }

        private double withoutShipTotal;

        public double WithoutShipTotal
        {
            get { return withoutShipTotal; }
            set { withoutShipTotal = value; OnPropertyChanged(nameof(WithoutShipTotal)); }
        }

        private double profitTotal;

        public double ProfitTotal
        {
            get { return profitTotal; }
            set { profitTotal = value; OnPropertyChanged(nameof(ProfitTotal)); }
        }
        #endregion

        #region Constructor
        public TotalsViewModel(List<Product> products)
        {
            SetTotals(products);
        }
        #endregion

        #region Methods
        private void SetTotals(List<Product> products)
        {
            QuantityTotal = products.Count;
            PurchaseTotal = products.Sum(p => p.PurchasePrice);
            SellTotal = products.Sum(p => p.SellingPrice).GetValueOrDefault();
            ShipTotal = products.Sum(p => p.ShippingPrice).GetValueOrDefault();
            WithoutShipTotal = products.Sum(p => p.PriceWithoutShipping).GetValueOrDefault();
            ProfitTotal = products.Sum(p => p.Profit).GetValueOrDefault();
        }
        #endregion
    }
}
