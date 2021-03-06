﻿using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public struct FloatScalar
    {
        public FloatScalar(float flat, float gain = default(float))
        {
            _base = flat;
            _gain = gain;
        }

        [SerializeField] private float _base;
        [SerializeField] private float _gain;

        public float Base
        {
            get { return _base; }
        }

        public float Gain
        {
            get { return _gain; }
        }

        public float Evaluate(int levelDelta = 0)
        {
            return Base + levelDelta * Gain;
        }
    }
}