using CommonContracts.Bluetooth;
using Meadow;
using Meadow.Gateways.Bluetooth;

namespace MeadowOnAir_Sign.Core.Connectivity;

public class BluetoothServer
{
    private CommandController commandController;

    private ICharacteristic pairingCharacteristic;
    private ICharacteristic OnAirTextCharacteristic;

    public BluetoothServer()
    {
        commandController = Resolver.Services.Get<CommandController>();
    }

    private void PairingCharacteristicValueSet(ICharacteristic c, object data)
    {
        commandController.FireTextUpdate((bool)data ? " Paired" : " Not Paired");
    }

    private void OnAirTextCharacteristicValueSet(ICharacteristic c, object data)
    {
        commandController.FireTextUpdate((string)data);
    }

    public Definition GetDefinition()
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

        pairingCharacteristic.ValueSet += PairingCharacteristicValueSet;
        OnAirTextCharacteristic.ValueSet += OnAirTextCharacteristicValueSet;

        var service = new Service(
            name: "Service",
            uuid: 253,
            pairingCharacteristic,
            OnAirTextCharacteristic
        );

        return new Definition("OnAirSign", service);
    }
}