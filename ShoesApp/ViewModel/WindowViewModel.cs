namespace ShoesApp.ViewModel
{
    public class WindowViewModel : BaseViewModel
    {
        #region ViewModels
        public StatisticsViewModel StatisticsViewModel { get; set; }
        public ChartsViewModel ChartsViewModel { get; set; }

        private ProductsViewModel productsViewModel;

        public ProductsViewModel ProductsViewModel
        {
            get { return productsViewModel; }
            set 
            {
                productsViewModel = value;
                OnPropertyChanged(nameof(ProductsViewModel));
            }
        }
        #endregion

        #region Construktor
        public WindowViewModel(ProductsViewModel productsViewModel, StatisticsViewModel statisticsViewModel, ChartsViewModel chartsViewModel)
        {
            ProductsViewModel = productsViewModel;
            StatisticsViewModel = statisticsViewModel;
            ChartsViewModel = chartsViewModel;
        }
        #endregion
    }
}