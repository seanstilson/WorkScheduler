using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkScheduler.ViewModels
{
    public class BasePageViewModel : INotifyPropertyChanged
    {
        public BasePageViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
