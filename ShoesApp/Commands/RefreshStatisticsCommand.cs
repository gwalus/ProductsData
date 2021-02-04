using ShoesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoesApp.Commands
{
    public class RefreshStatisticsCommand : ICommand
    {
        private readonly StatisticsViewModel _viewModel;

        public RefreshStatisticsCommand(StatisticsViewModel viewModel)
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
            _viewModel.RefreshContent();
        }
    }
}
