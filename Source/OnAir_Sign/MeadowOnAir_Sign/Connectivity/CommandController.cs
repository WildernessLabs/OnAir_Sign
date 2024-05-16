using Meadow;
using System;

namespace MeadowOnAir_Sign.Core;

public class CommandController
{
    public event EventHandler<bool> PairingValueSet = default!;
    public event EventHandler<string> TextValueSet = default!;

    public CommandController()
    {
        Resolver.Services.Add(this);
    }

    public void FirePairing(bool pairing)
    {
        PairingValueSet?.Invoke(this, pairing);
    }

    public void FireTextUpdate(string text)
    {
        TextValueSet?.Invoke(this, text);
    }
}