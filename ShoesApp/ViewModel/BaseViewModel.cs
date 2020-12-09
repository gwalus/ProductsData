using System.ComponentModel;

namespace ShoesApp.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public void OnPropertyChanged(string memberName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
