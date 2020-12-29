using ShoesApp.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ShoesApp.ViewModel
{
    public class StatisticsViewModel : BaseViewModel
    {
        #region Fields
        private readonly IDataRepository _dataRepository;
        #endregion

        private IList<GroupingData> groupedSoldProducts;

        public IList<GroupingData> GroupedSoldProducts
        {
            get { return groupedSoldProducts; }
            set
            {
                groupedSoldProducts = value;
                OnPropertyChanged(nameof(GroupedSoldProducts));
            }
        }

        private IList<GroupingData> groupedPurchaseProducts;

        public IList<GroupingData> GroupedPurchaseProducts
        {
            get { return groupedPurchaseProducts; }
            set
            {
                groupedPurchaseProducts = value;
                OnPropertyChanged(nameof(GroupedPurchaseProducts));

            }
        }

        public StatisticsViewModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            GroupedSoldProducts = new List<GroupingData>();
            GroupedPurchaseProducts = new List<GroupingData>();
            GetGroupingProducts();
        }

        private async void GetGroupingProducts()
        {
            var products = await _dataRepository.GetProducts();

            var groupedSoldProducts = products
                .Where(x => x.IsSold)
                .OrderByDescending(x => DateTime.Parse(x.SaleDate))
                .GroupBy(x => new { DateTime.Parse(x.SaleDate).Year, DateTime.Parse(x.SaleDate).Month })
                .ToList();

            foreach (var item in groupedSoldProducts)
            {
                var groupedData = new GroupingData()
                {
                    Year = item.Key.Year,
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Key.Month),
                    Count = item.Count(),
                    Profit = Math.Round(item.Sum(x => x.Profit.Value), 2),
                    Average = Math.Round((item.Sum(x => x.Profit.Value) / item.Count()), 2)
                };

                GroupedSoldProducts.Add(groupedData);
            }

            var groupedPurchaseProducts = products
                .OrderByDescending(product => DateTime.Parse(product.DateOfPurchase))
                .GroupBy(product => new { DateTime.Parse(product.DateOfPurchase).Year, DateTime.Parse(product.DateOfPurchase).Month })
                .ToList();

            foreach (var item in groupedPurchaseProducts)
            {
                var groupedData = new GroupingData()
                {
                    Year = item.Key.Year,
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Key.Month),
                    Count = item.Count(),
                    Purchase = Math.Round(item.Sum(x => x.PurchasePrice), 2),
                    Average = Math.Round((item.Sum(x => x.PurchasePrice) / item.Count()), 2)
                };

                GroupedPurchaseProducts.Add(groupedData);
            }
        }
    }

    public class GroupingData
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public int Count { get; set; }
        public double Purchase { get; set; }
        public double Profit { get; set; }
        public double Average { get; set; }
    }
}
