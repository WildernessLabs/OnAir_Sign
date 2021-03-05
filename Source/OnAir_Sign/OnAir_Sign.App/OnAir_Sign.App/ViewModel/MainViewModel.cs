﻿using Meadow.Foundation.Maple.Client;
using OnAir_Sign.App.Client;
using OnAir_Sign.App.Model;
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
        MapleClient mapleClient;
        SignClient signClient;

        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }

        bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(nameof(IsEmpty)); }
        }

        string _status;
        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        bool _showConfig;
        public bool ShowConfig
        {
            get => _showConfig;
            set { _showConfig = value; OnPropertyChanged(nameof(ShowConfig)); }
        }

        string textSign;
        public string TextSign
        {
            get => textSign;
            set { textSign = value; OnPropertyChanged(nameof(TextSign)); }
        }

        MapleServerModel _selectedServer;
        public MapleServerModel SelectedServer
        {
            get => _selectedServer;
            set { _selectedServer = value; OnPropertyChanged(nameof(SelectedServer)); }
        }

        public ObservableCollection<MapleServerModel> HostList { get; set; }

        public Command SendCommand { private set; get; }

        public Command ConnectCommand { private set; get; }

        public Command SearchServersCommand { private set; get; }

        public MainViewModel()
        {
            HostList = new ObservableCollection<MapleServerModel>();

            //mapleClient = new MapleClient();
            //mapleClient.StartScanningForAdvertisingServers();
            //mapleClient.Servers.CollectionChanged += ServersCollectionChanged;

            signClient = new SignClient();
            signClient.Servers.CollectionChanged += ServersCollectionChanged;
            
            SendCommand = new Command(async (s) => await SendSignTextCommandAsync((string)s));

            ConnectCommand = new Command(() => { ShowConfig = false; IsBusy = false; }); 

            SearchServersCommand = new Command(async () => await GetServers());

            GetServers();
        }

        async Task GetServers()
        {
            IsBusy = true;
            
            IsLoading = true;

            IsEmpty = false;
            Status = "Looking for servers";
            
            await signClient.StartScanningForAdvertisingServers();

            if (HostList.Count == 0)
            {
                Status = "No servers found...";
                IsEmpty = true;
            }
            else
            {
                Status = "Select a server";
                IsEmpty = false;

                SelectedServer = HostList[0];
                ShowConfig = true;
            }

            IsLoading = false;
        }

        void ServersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (ServerModel server in e.NewItems)
                    {
                        HostList.Add(new MapleServerModel(server.Name, server.IpAddress));
                        Console.WriteLine($"'{server.Name}' @ ip:[{server.IpAddress}]");
                    }
                    break;
            }

            IsLoading = false;
        }

        async Task SendSignTextCommandAsync(string text)
        {
            bool isSuccessful = false;

            Status = "Sending command...";

            isSuccessful = await signClient.SetSignTextAsync(SelectedServer, text);

            if (isSuccessful)
            {
                IsBusy = false;
            }
            else
            {
                await GetServers();
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