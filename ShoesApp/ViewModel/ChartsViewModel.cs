using ShoesApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoesApp.ViewModel
{
    public class ChartsViewModel : BaseViewModel
    {
        private IDataRepository _dataRepository;

        private IList<KeyValuePair<string, double>> chart;

        public IList<KeyValuePair<string, double>> Chart
        {
            get { return chart; }
            set
            {
                chart = value;
                OnPropertyChanged(nameof(Chart));
            }
        }

        public ChartsViewModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            SetChart();
        }

        private async void SetChart()
        {
            var products = await _dataRepository.GetProducts();

            var chartData = products
                .GroupBy(x => new { DateTime.Parse(x.DateOfPurchase).Year, DateTime.Parse(x.DateOfPurchase).Month })
                .Select(x => new
                {
                    Key = string.Format($"{x.Key.Year}, {x.Key.Month}"),
                    Value = Math.Round(x.Sum(product => product.PurchasePrice), 2)
                })
                .ToList();

            Chart = new List<KeyValuePair<string, double>>();

            foreach (var item in chartData)
            {
                var ones = KeyValuePair.Create<string, double>(item.Key, item.Value);
                Chart.Add(ones);
            }
        }
    }
}
