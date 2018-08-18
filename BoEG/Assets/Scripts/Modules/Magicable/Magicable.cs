﻿using System;
using Core;
using UnityEngine;

namespace Modules.Magicable
{
    [Serializable]
    public class Magicable : Module, IMagicable
    {
        public Magicable(GameObject self, IMagicableData data) : base(self)
        {
            _data = data;
            _manaPercentage = 1f;
			_mask = 0;
        }

        [SerializeField] [Range(0f, 1f)] private float _manaPercentage;
        private readonly IMagicableData _data;
		
		private byte _mask;
		public void override Serialize(ISerializer serializer)
		{
			serializer.Write(_mask);		
			if(_mask.HasBit(0))
				serializer.Write(_manaPercentage);
			_mask = 0;
		}
		public void override Deserialize(IDeserializer deserializer)
		{
			var mask = serializer.ReadByte();		
			if(mask.HasBit(0))
				_manaPercentage = serializer.ReadFloat();
		}


        public override void PreTick(float deltaTime)
        {
            if (ManaPercentage > 0f && ManaPercentage < 1f)
                ModifyMana(ManaGeneration * deltaTime, Self);
        }

        public override void PostTick(float deltaTime)
        {
        }


        public float ManaPercentage
        {
            get { return _manaPercentage; }
            private set { _manaPercentage = Mathf.Clamp01(value); }
        }

        public float ManaPoints
        {
            get { return _manaPercentage * ManaCapacity; }
            private set { ManaPercentage = value / ManaCapacity; }
        }

        public float ManaCapacity
        {
            get { return _data.ManaCapacity.Evaluate(); }
        }

        public float ManaGeneration
        {
            get { return _data.ManaGeneration.Evaluate(); }
        }

        public void ModifyMana(float modification, GameObject source)
        {
            ManaPoints += modification;
            var args = new ManaModifiedEventArgs(source, Self, modification);
            OnManaModified(args);
        }

        private void OnManaModified(ManaModifiedEventArgs eventArgs)
        {
            if (ManaModified != null)
                ManaModified(eventArgs);
        }

        public event ManaModifiedHandler ManaModified;
    }

    public delegate void ManaModifiedHandler(ManaModifiedEventArgs eventArgs);

    public class ManaModifiedEventArgs : EndgameEventArgs
    {
        public ManaModifiedEventArgs(GameObject source, GameObject owner, float modified) : base(source, owner)
        {
            Modified = modified;
        }

        public float Modified { get; private set; }
    }

    public class EndgameEventArgs : EventArgs
    {
        public EndgameEventArgs(GameObject source, GameObject owner)
        {
            Source = source;
            Owner = owner;
        }

        /// <summary>
        /// The source of the Event
        /// </summary>
        public GameObject Source { get; private set; }

        /// <summary>
        /// The owner of the Event
        /// </summary>
        public GameObject Owner { get; private set; }
    }
}