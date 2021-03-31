﻿using Meadow.Foundation.Maple.Client;
using OnAir_Sign.App.Client;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnAir_Sign.App.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        SignClient signClient;

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

            signClient = new SignClient();
            signClient.Servers.CollectionChanged += ServersCollectionChanged;
            
            SendCommand = new Command(async () => await SendSignTextCommandAsync());

            SearchServersCommand = new Command(async () => await GetServers());
        }

        public async Task GetServers()
        {
            IsBusy = true;

            IsServerListEmpty = false;

            await signClient.StartScanningForAdvertisingServers();

            if (HostList.Count == 0)
            {
                IsServerListEmpty = true;
            }
            else
            {
                IsServerListEmpty = false;
                SelectedServer = HostList[0];
            }

            IsBusy = false;
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

            IsBusy = false;
        }

        async Task SendSignTextCommandAsync()
        {
            IsBusy = true;

            var response = await signClient.SetSignText(SelectedServer, TextSign);

            if (!response.IsSuccessStatusCode)
            {
                await App.Current.DisplayAlert("Error", response.StatusCode.ToString(), "Close");
            }

            IsBusy = false;
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