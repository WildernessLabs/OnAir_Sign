﻿using Meadow;
using System.Threading.Tasks;

namespace MeadowOnAir_Sign.Desktop;

public class Program
{
    public static async Task Main(string[] args)
    {
        await MeadowOS.Start(args);
    }
}