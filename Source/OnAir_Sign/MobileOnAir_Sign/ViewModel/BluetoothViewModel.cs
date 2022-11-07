using CommonContracts.Bluetooth;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace MobileOnAir_Sign.ViewModel
{
    public class BluetoothViewModel : BaseViewModel
    {
        int listenTimeout = 5000;

        ushort DEVICE_ID = 253;

        IAdapter adapter;
        IService service;

        ICharacteristic pairingCharacteristic;
        ICharacteristic OnAirTextCharacteristic;

        public ObservableCollection<IDevice> DeviceList { get; set; }

        IDevice deviceSelected;
        public IDevice DeviceSelected
        {
            get => deviceSelected;
            set { deviceSelected = value; OnPropertyChanged(nameof(DeviceSelected)); }
        }

        bool isScanning;
        public bool IsScanning
        {
            get => isScanning;
            set { isScanning = value; OnPropertyChanged(nameof(IsScanning)); }
        }

        bool isConnected;
        public bool IsConnected
        {
            get => isConnected;
            set { isConnected = value; OnPropertyChanged(nameof(IsConnected)); }
        }

        bool isDeviceListEmpty;
        public bool IsDeviceListEmpty
        {
            get => isDeviceListEmpty;
            set { isDeviceListEmpty = value; OnPropertyChanged(nameof(IsDeviceListEmpty)); }
        }

        public ICommand ToggleConnectionCommand { get; set; }

        public ICommand SearchForDevicesCommand { get; set; }

        public ICommand SetTextCommand { get; private set; }

        public BluetoothViewModel()
        {
            DeviceList = new ObservableCollection<IDevice>();

            adapter = CrossBluetoothLE.Current.Adapter;
            adapter.ScanTimeout = listenTimeout;
            adapter.ScanMode = ScanMode.LowLatency;
            adapter.DeviceConnected += AdapterDeviceConnected;
            adapter.DeviceDiscovered += AdapterDeviceDiscovered;
            adapter.DeviceDisconnected += AdapterDeviceDisconnected;

            ToggleConnectionCommand = new Command(async () => await ToggleConnection());

            SearchForDevicesCommand = new Command(async () => await SearchForDevices());

            SetTextCommand = new Command(async () => await SetOnAirSignText());
        }

        void AdapterDeviceDisconnected(object sender, DeviceEventArgs e)
        {
            IsConnected = false;
        }

        async void AdapterDeviceConnected(object sender, DeviceEventArgs e)
        {
            IsConnected = true;

            IDevice device = e.Device;

            var services = await device.GetServicesAsync();

            foreach (var serviceItem in services)
            {
                if (UuidToUshort(serviceItem.Id.ToString()) == DEVICE_ID)
                {
                    service = serviceItem;
                }
            }

            pairingCharacteristic = await service.GetCharacteristicAsync(Guid.Parse(CharacteristicsConstants.PAIRING));
            OnAirTextCharacteristic = await service.GetCharacteristicAsync(Guid.Parse(CharacteristicsConstants.ON_AIR_SIGN_TEXT));

            SetPairingStatus();
        }

        async void AdapterDeviceDiscovered(object sender, DeviceEventArgs e)
        {
            if (DeviceList.FirstOrDefault(x => x.Name == e.Device.Name) == null &&
                !string.IsNullOrEmpty(e.Device.Name))
            {
                DeviceList.Add(e.Device);
            }

            if (e.Device.Name == "OnAirSign")
            {
                await adapter.StopScanningForDevicesAsync();
                IsDeviceListEmpty = false;
                DeviceSelected = e.Device;
            }
        }

        async Task ScanTimeoutTask()
        {
            await Task.Delay(listenTimeout);
            await adapter.StopScanningForDevicesAsync();
            IsScanning = false;
        }

        async Task ToggleConnection()
        {
            try
            {
                if (IsConnected)
                {
                    IsConnected = false;
                    await SetPairingStatus();
                    await adapter.DisconnectDeviceAsync(DeviceSelected);

                }
                else
                {
                    await adapter.ConnectToDeviceAsync(DeviceSelected);
                    IsConnected = true;
                }
            }
            catch (DeviceConnectionException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task SearchForDevices()
        {
            try
            {
                IsScanning = true;

                var tasks = new Task[]
                {
                    ScanTimeoutTask(),
                    adapter.StartScanningForDevicesAsync()
                };

                await Task.WhenAny(tasks);
            }
            catch (DeviceConnectionException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task SetPairingStatus()
        {
            byte[] array = new byte[1];
            array[0] = IsConnected ? (byte)1 : (byte)0;

            await pairingCharacteristic.WriteAsync(array);
        }

        async Task SetOnAirSignText() 
        {
            var array = System.Text.Encoding.ASCII.GetBytes(TextSign);

            await OnAirTextCharacteristic.WriteAsync(array);
        }

        protected int UuidToUshort(string uuid)
        {
            return int.Parse(uuid.Substring(4, 4), System.Globalization.NumberStyles.HexNumber); ;
        }
    }
}