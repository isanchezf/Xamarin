using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class AddContactViewModel : INotifyPropertyChanged
    {
        public AddContactViewModel()
        {
            LaunchWebSiteCommand = new Command(LaunchWebSite, () => !IsBusy);
            SaveContactCommand = new Command(async ()=> await SaveWebContact(), ()=>!IsBusy);
        }

        public Command SaveContactCommand { get; }
        public Command LaunchWebSiteCommand { get; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        bool bestFriend;
        public bool BestFriend
        {
            get => bestFriend;
            set
            {
                bestFriend = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        string name = "Ivan Sanchez";
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        string website = "http://mipage.com";
        public string Website { get => website;
            set
            {
                website = value;
                OnPropertyChanged();
            }
        }

        bool isBusy;
        public bool IsBusy { get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
                SaveContactCommand.ChangeCanExecute();
                LaunchWebSiteCommand.ChangeCanExecute();
            }
        }

        public string DisplayMessage => $"Your new friend is named {Name} and {(bestFriend ? "is" : "is not")} your best friend";

        public void LaunchWebSite()
        {
            try
            {
                Device.OpenUri(new Uri(website));
            }
            catch (Exception)
            {

                throw;
            }
        }

        async Task SaveWebContact()
        {
            IsBusy = true;
            await Task.Delay(4000);
            IsBusy = false;
            await Application.Current.MainPage.DisplayAlert("Save Contact", "The contact has been saved", "Ok");
        }
    }
}
