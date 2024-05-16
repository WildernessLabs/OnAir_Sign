﻿using Meadow;
using Meadow.Devices;
using System.Threading.Tasks;

namespace MeadowOnAir_Sign.Max7219_
{
    public class MeadowApp : App<F7FeatherV2>
    {
        public override Task Initialize()
        {
            Resolver.Log.Info("Initialize...");

            return Task.CompletedTask;
        }

        public override Task Run()
        {
            Resolver.Log.Info("Run...");

            Resolver.Log.Info("Hello, Meadow Core-Compute!");

            return Task.CompletedTask;
        }
    }
}