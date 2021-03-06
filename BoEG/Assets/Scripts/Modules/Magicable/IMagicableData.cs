﻿using Core;

namespace Modules.Magicable
{
    public interface IMagicableData
    {
        FloatScalar ManaCapacity { get; }
        FloatScalar ManaGeneration { get; }
    }
}