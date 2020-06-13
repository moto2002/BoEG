﻿using System;
using UnityEngine;

namespace Util
{
    public class LaneWaypointManager : MonoBehaviour
    {
        private const string SingletonString = "SingletonString";
        private static LaneWaypointManager _instance;

        private static readonly Transform[] NullWaypoints = new Transform[0];

        [SerializeField] private WaypointInfo[] _waypointInfo;

        private static LaneWaypointManager Instance
        {
            get
            {
                if (_instance != null) return _instance;
                //If either of these fail, we want to throw an exception, so we dont check for null
                _instance = GameObject.Find(SingletonString).GetComponent<LaneWaypointManager>();
                return _instance;
            }
        }

        public Transform[] GetWaypoints(WaypointData data)
        {
            foreach (var wpi in _waypointInfo)
                if (wpi.Data == data)
                    return wpi.Waypoints;
            return NullWaypoints;
        }


        [Serializable]
        private struct WaypointInfo
        {
            [SerializeField] private WaypointData _data;
            [SerializeField] private Transform[] _waypoints;

            public WaypointData Data => _data;

            public Transform[] Waypoints => _waypoints;
        }
    }
}