using Meadow.Foundation.Web.Maple.Client;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileOnAir_Sign.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MapleClient client { get; private set; }

        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }

        bool _isServerListEmpty;
        public bool IsServerListEmpty
        {
            get => _isServerListEmpty;
            set { _isServerListEmpty = value; OnPropertyChanged(nameof(IsServerListEmpty)); }
        }

        string textSign;
        public string TextSign
        {
            get => textSign;
            set { textSign = value; OnPropertyChanged(nameof(TextSign)); }
        }

        int serverPort;
        public int ServerPort 
        {
            get => serverPort;
            set { serverPort = value; OnPropertyChanged(nameof(ServerPort)); }
        }

        ServerModel _selectedServer;
        public ServerModel SelectedServer
        {
            get => _selectedServer;
            set { _selectedServer = value; OnPropertyChanged(nameof(SelectedServer)); }
        }

        public ObservableCollection<ServerModel> HostList { get; set; }

        public Command SendCommand { private set; get; }

        public Command SearchServersCommand { private set; get; }

        public MainViewModel()
        {
            HostList = new ObservableCollection<ServerModel>();
            HostList.Add(new ServerModel() { Name = "Meadow (192.168.1.69)", IpAddress = "192.168.1.69" });

            ServerPort = 5417;

            client = new MapleClient();
            client.Servers.CollectionChanged += ServersCollectionChanged;

            SendCommand = new Command(async () => await SendSignTextCommandAsync());

            SearchServersCommand = new Command(async () => await GetServers());
        }

        public async Task GetServers()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                IsServerListEmpty = false;

                await client.StartScanningForAdvertisingServers();

                if (HostList.Count == 0)
                {
                    IsServerListEmpty = true;
                }
                else
                {
                    IsServerListEmpty = false;
                    SelectedServer = HostList[0];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        void ServersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (ServerModel server in e.NewItems)
                    {
                        HostList.Add(new ServerModel() { Name = $"{server.Name} ({server.IpAddress})", IpAddress = server.IpAddress });
                        Console.WriteLine($"'{server.Name}' @ ip:[{server.IpAddress}]");
                    }
                    break;
            }
        }

        async Task SendSignTextCommandAsync()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try 
            { 
                bool response = await client.PostAsync(SelectedServer.IpAddress, ServerPort, "SignText", TextSign);

                if (!response)
                {
                    await App.Current.DisplayAlert("Error", "Request failed.", "Close");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}