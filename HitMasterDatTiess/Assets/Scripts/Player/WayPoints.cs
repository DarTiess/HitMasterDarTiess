using UnityEngine;

namespace Player.States
{
    public class WayPoints : IWayPoints
    {
        private Transform[] _waypoints;
        private int _index;

        public WayPoints(Transform[] waypoints)
        {
            _waypoints = waypoints;
            _index = 0;
        }

        public Transform GetWayPoint()
        {
            Transform returned = null;
            if (_index < _waypoints.Length)
            {
                returned = _waypoints[_index];
                _index++;
            }

            return returned;
        }
    }
}