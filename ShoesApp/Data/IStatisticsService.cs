using ShoesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesApp.Data
{
    public interface IStatisticsService
    {
        Task<Product> GetFirstPurchase();
        Task<Product> GetLatestPurchase();
        Task<Product> GetLatestSale();
        Task<int> GetDaysOfFirstPurchase();
        Task<int> GetDaysOfLatestPurchase();
        Task<int> GetDaysOfLatestSale();
        Task<double> GetBestProfit();
        Task<double> GetLowestProfit();
        Task<double> GetBiggestPurchase();
        Task<double> GetLowestPurchase();
    }
}
