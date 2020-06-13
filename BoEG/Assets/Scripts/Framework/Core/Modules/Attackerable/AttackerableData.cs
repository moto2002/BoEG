using System;
using UnityEngine;

namespace Framework.Core.Modules
{
    [Serializable]
    public struct AttackerableData : IAttackerableData
    {
#pragma warning disable 649
        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackDamage;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackInterval;
        [SerializeField] private bool _isRanged;
#pragma warning restore 649

        public float AttackInterval
        {
            get { return _attackInterval; }
        }

        public float AttackRange
        {
            get { return _attackRange; }
        }

        public float AttackDamage
        {
            get { return _attackDamage; }
        }

        public float AttackSpeed
        {
            get { return _attackSpeed; }
        }

        public bool IsRanged
        {
            get { return _isRanged; }
        }
    }
}