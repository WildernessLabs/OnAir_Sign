using Meadow.Gateways.Bluetooth;
using Meadow.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeadowOnAir_Sign.HackKit.Connectivity
{
    public class BluetoothServer
    {
        private static readonly Lazy<BluetoothServer> instance =
            new Lazy<BluetoothServer>(() => new BluetoothServer());
        public static BluetoothServer Instance => instance.Value;

        Definition bleTreeDefinition;
        CharacteristicBool pairingCharacteristic;
        CharacteristicString TextCharacteristic;

        public bool IsInitialized { get; private set; }

        private BluetoothServer() { }

        public void Initialize()
        {
            //bleTreeDefinition = GetDefinition();
            pairingCharacteristic.ValueSet += PairingCharacteristicValueSet;
            TextCharacteristic.ValueSet += TextCharacteristicValueSet;
            MeadowApp.Device.BluetoothAdapter.StartBluetoothServer(bleTreeDefinition);

            IsInitialized = true;
        }

        private void PairingCharacteristicValueSet(ICharacteristic c, object data)
        {

        }

        private void TextCharacteristicValueSet(ICharacteristic c, object data)
        {
            throw new NotImplementedException();
        }

        //Definition GetDefinition()
        //{
        //    pairingCharacteristic = new CharacteristicBool(
        //        name: "PAIRING",
        //        uuid: CharacteristicsConstants.PAIRING,
        //        permissions: CharacteristicPermission.Read | CharacteristicPermission.Write,
        //        properties: CharacteristicProperty.Read | CharacteristicProperty.Write);
        //    ledToggleCharacteristic = new CharacteristicBool(
        //        name: "LED_TOGGLE",
        //        uuid: CharacteristicsConstants.LED_TOGGLE,
        //        permissions: CharacteristicPermission.Read | CharacteristicPermission.Write,
        //        properties: CharacteristicProperty.Read | CharacteristicProperty.Write);
        //    ledBlinkCharacteristic = new CharacteristicBool(
        //        name: "LED_BLINK",
        //        uuid: CharacteristicsConstants.LED_BLINK,
        //        permissions: CharacteristicPermission.Read | CharacteristicPermission.Write,
        //        properties: CharacteristicProperty.Read | CharacteristicProperty.Write);
        //    ledPulseCharacteristic = new CharacteristicBool(
        //        name: "LED_PULSE",
        //        uuid: CharacteristicsConstants.LED_PULSE,
        //        permissions: CharacteristicPermission.Read | CharacteristicPermission.Write,
        //        properties: CharacteristicProperty.Read | CharacteristicProperty.Write);
        //    TextCharacteristic = new CharacteristicString(
        //        name: "BME688_DATA",
        //        uuid: CharacteristicsConstants.BME688_DATA,
        //        maxLength: 20,
        //        permissions: CharacteristicPermission.Read,
        //        properties: CharacteristicProperty.Read);
        //    bh1750DataCharacteristic = new CharacteristicString(
        //        name: "BH1750_DATA",
        //        uuid: CharacteristicsConstants.BH1750_DATA,
        //        maxLength: 20,
        //        permissions: CharacteristicPermission.Read,
        //        properties: CharacteristicProperty.Read);

        //    var service = new Service(
        //        name: "Service",
        //        uuid: 253,
        //        pairingCharacteristic,
        //        ledToggleCharacteristic,
        //        ledBlinkCharacteristic,
        //        ledPulseCharacteristic,
        //        TextCharacteristic,
        //        bh1750DataCharacteristic
        //    );

        //    return new Definition("ProjectLab", service);
        //}
    }
}