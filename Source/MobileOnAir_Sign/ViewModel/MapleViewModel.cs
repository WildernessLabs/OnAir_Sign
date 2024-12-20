﻿using Meadow.Foundation.Web.Maple;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace MobileOnAir_Sign.ViewModel;

public class MapleViewModel : BaseViewModel
{
    public MapleClient client { get; private set; }

    int serverPort;
    public int ServerPort
    {
        get => serverPort;
        set { serverPort = value; OnPropertyChanged(nameof(ServerPort)); }
    }

    bool isScanning;
    public bool IsScanning
    {
        get => isScanning;
        set { isScanning = value; OnPropertyChanged(nameof(IsScanning)); }
    }

    bool isServerListEmpty;
    public bool IsServerListEmpty
    {
        get => isServerListEmpty;
        set { isServerListEmpty = value; OnPropertyChanged(nameof(IsServerListEmpty)); }
    }

    string ipAddress;
    public string IpAddress
    {
        get => ipAddress;
        set { ipAddress = value; OnPropertyChanged(nameof(IpAddress)); }
    }

    ServerModel _selectedServer;
    public ServerModel SelectedServer
    {
        get => _selectedServer;
        set
        {
            if (value == null) return;
            _selectedServer = value;
            IpAddress = _selectedServer.IpAddress;
            OnPropertyChanged(nameof(SelectedServer));
        }
    }

    public ObservableCollection<ServerModel> HostList { get; set; }

    public ICommand SendCommand { private set; get; }

    public ICommand SearchServersCommand { private set; get; }

    public MapleViewModel()
    {
        HostList = new ObservableCollection<ServerModel>();
        //HostList.Add(new ServerModel() { Name = "Meadow (192.168.1.84)", IpAddress = "192.168.1.84" });

        ServerPort = 5417;

        client = new MapleClient();
        client.Servers.CollectionChanged += ServersCollectionChanged;

        SendCommand = new Command(async () => await SendSignTextCommandAsync());

        SearchServersCommand = new Command(async () => await GetServers());
    }

    async Task GetServers()
    {
        if (IsScanning)
            return;
        IsScanning = true;

        try
        {
            IsServerListEmpty = false;

            await client.StartScanningForAdvertisingServers();

            //HostList.Add(new ServerModel() { Name = "Meadow (192.168.1.81)", IpAddress = "192.168.1.81" });

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
            IsScanning = false;
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
            bool response = await client.PostAsync(SelectedServer != null ? SelectedServer.IpAddress : IpAddress, ServerPort, "SignText", TextSign);

            if (!response)
            {
                Console.WriteLine("Error: Request failed.");
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
}