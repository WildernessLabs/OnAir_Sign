using CommonContracts.Bluetooth;
using Meadow.Gateways.Bluetooth;
using System;

namespace MeadowOnAir_Sign.HackKit
{
    public class BluetoothServer
    {
        private static readonly Lazy<BluetoothServer> instance =
            new Lazy<BluetoothServer>(() => new BluetoothServer());
        public static BluetoothServer Instance => instance.Value;

        Definition bleTreeDefinition;
        CharacteristicBool pairingCharacteristic;
        CharacteristicString OnAirTextCharacteristic;

        private BluetoothServer() { }

        public void Initialize()
        {
            bleTreeDefinition = GetDefinition();
            pairingCharacteristic.ValueSet += PairingCharacteristicValueSet;
            OnAirTextCharacteristic.ValueSet += OnAirTextCharacteristicValueSet;
            MeadowApp.Device.BluetoothAdapter.StartBluetoothServer(bleTreeDefinition);
        }

        private void PairingCharacteristicValueSet(ICharacteristic c, object data)
        {
            DisplayController.Instance.BluetoothScreen((bool)data ? " Paired" : " Not Paired");
        }

        private void OnAirTextCharacteristicValueSet(ICharacteristic c, object data)
        {
            DisplayController.Instance.ShowText((string)data);
        }

        Definition GetDefinition()
        {
            pairingCharacteristic = new CharacteristicBool(
                name: "PAIRING",
                uuid: CharacteristicsConstants.PAIRING,
                permissions: CharacteristicPermission.Read | CharacteristicPermission.Write,
                properties: CharacteristicProperty.Read | CharacteristicProperty.Write);
            OnAirTextCharacteristic = new CharacteristicString(
                name: "ON_AIR_SIGN_TEXT",
                uuid: CharacteristicsConstants.ON_AIR_SIGN_TEXT,
                maxLength: 20,
                permissions: CharacteristicPermission.Read | CharacteristicPermission.Write,
                properties: CharacteristicProperty.Read | CharacteristicProperty.Write);

            var service = new Service(
                name: "Service",
                uuid: 253,
                pairingCharacteristic,
                OnAirTextCharacteristic
            );

            return new Definition("OnAirSign", service);
        }
    }
}