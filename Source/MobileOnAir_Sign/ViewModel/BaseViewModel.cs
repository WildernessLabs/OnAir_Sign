using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MobileOnAir_Sign.ViewModel;

public class BaseViewModel : INotifyPropertyChanged
{
    bool isBusy;
    public bool IsBusy
    {
        get => isBusy;
        set { isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
    }

    string textSign;
    public string TextSign
    {
        get => textSign;
        set { textSign = value; OnPropertyChanged(nameof(TextSign)); }
    }

    #region INotifyPropertyChanged Implementation
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    #endregion
}