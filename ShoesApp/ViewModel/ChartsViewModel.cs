using ShoesApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoesApp.ViewModel
{
    public class ChartsViewModel : BaseViewModel
    {
        public enum ChartTypes
        {
            purchase, sold
        }

        #region Fields
        private IDataRepository _dataRepository;
        #endregion

        #region Properties
        private IList<KeyValuePair<string, double>> _chart;

        public IList<KeyValuePair<string, double>> Chart
        {
            get { return _chart; }
            set
            {
                _chart = value;
                OnPropertyChanged(nameof(Chart));
            }
        }

        private string _chartTitle;

        public string ChartTitle
        {
            get { return _chartTitle; }
            set 
            {
                _chartTitle = value;
                OnPropertyChanged(nameof(ChartTitle));
            }
        }


        #endregion

        #region Constructor
        public ChartsViewModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            SetChart(ChartTypes.purchase);
        }
        #endregion

        private async void SetChart(ChartTypes type)
        {
            var products = await _dataRepository.GetProducts();

            var chartData = products
                .GroupBy(x => new { DateTime.Parse(x.DateOfPurchase).Year, DateTime.Parse(x.DateOfPurchase).Month })
                .Select(x => new
                {
                    Key = string.Format($"{x.Key.Year}, {x.Key.Month}"),
                    Value = type switch
                    {
                        ChartTypes.purchase => Math.Round(x.Sum(product => product.PurchasePrice), 2),
                        ChartTypes.sold => Math.Round(x.Sum(product => product.SellingPrice.Value), 2),
                        _ => throw new ArgumentOutOfRangeException(nameof(type)),
                    }
                })
                .ToList();

            //Value = Math.Round(x.Sum(product => product.PurchasePrice), 2)
            //    })
            //    .ToList();

            Chart = new List<KeyValuePair<string, double>>();

            foreach (var item in chartData)
            {
                var ones = KeyValuePair.Create<string, double>(item.Key, item.Value);
                Chart.Add(ones);
            }
        }
    }
}
