﻿using System;
using Framework.Core.Modules;
using UnityEngine;

namespace Modules.Teamable
{
    [Serializable]
    public class Teamable : MonoBehaviour, ITeamable, IInitializable<TeamData>
    {

        [SerializeField] private TeamData _team;

        public TeamData Team => _team;

        public void SetTeam(TeamData team)
        {
            _team = team;
        }

        public bool SameTeam(ITeamable teamable) => _team == teamable.Team;

        public void Initialize(TeamData module)
        {
            _team = module;
        }
    }
}