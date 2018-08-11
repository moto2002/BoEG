﻿using System;
using Core;
using Modules.Magicable;
using Modules.MiscEvents;
using UnityEngine;

namespace Modules.Healthable
{
    [Serializable]
    public class Healthable : Module, IHealthable
    {
        public Healthable(GameObject self, IHealthableData data) : base(self)
        {
            _data = data;
            _healthPercentage = 1f;
        }

        [SerializeField] [Range(0f, 1f)] private float _healthPercentage;
        private readonly IHealthableData _data;


        public override void PreTick(float deltaTime)
        {
            if (HealthPercentage > 0f && HealthPercentage < 1f)
                ModifyHealth(HealthGeneration * deltaTime);
        }

        public override void PostTick(float deltaTime)
        {
            if (HealthPercentage <= 0f)
                Die();
        }


        public float HealthPercentage
        {
            get { return _healthPercentage; }
            private set { _healthPercentage = Mathf.Clamp01(value); }
        }

        public float HealthPoints
        {
            get { return _healthPercentage * HealthCapacity; }
            private set { HealthPercentage = value / HealthCapacity; }
        }

        public float HealthCapacity
        {
            get { return _data.HealthCapacity.Evaluate(); }
        }

        public float HealthGeneration
        {
            get { return _data.HealthGeneration.Evaluate(); }
        }

        public void ModifyHealth(float modification)
        {
            HealthPoints += modification;
            OnHealthModified();
        }

        private void OnHealthModified()
        {
            if (HealthModified != null)
                HealthModified();
        }

        public event DEFAULT_HANDLER HealthModified;

        public void TakeDamage(Damage damage)
        {
            ModifyHealth(-damage.Value);
            var args = new DamageEventArgs(Self, damage);
            OnDamageTaken(args);
            //TODO, replace with a kill callback in TakeDamage
            if (HealthPercentage <= 0f)
                Die(damage.Source);
        }

        private void OnDamageTaken(DamageEventArgs args)
        {
            if (DamageTaken != null)
                DamageTaken(args);
        }

        public event DamageEventHandler DamageTaken;
        
        
        public void Die()
        {
            OnDied();
            Self.SetActive(false);
        }
        //TODO, replace with a kill callback in TakeDamage
        private void Die(GameObject source)
        {
            Die();
            var misc = source.GetComponent<IMiscEvent>();
            if(misc != null)
                misc.Kill(Self);
        }

        private void OnDied()
        {
            if (Died != null)
                Died();
        }

        public event DEFAULT_HANDLER Died;
    }
    public delegate void DamageEventHandler(DamageEventArgs args);
    public class DamageEventArgs : EndgameEventArgs
    {
        public DamageEventArgs(GameObject target, Damage damage) : base(damage.Source, target)
        {
            Damage = damage;
        }
        public Damage Damage { get; private set; }   
    }
}