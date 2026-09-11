using DroneSim.Core;
using UnityEngine;

namespace DroneSim.Flight
{
    public static class AeroMath
    {
        public static Vector3 QuadraticDrag(Vector3 relativeVelocity, float dragCoefficient, float area)
        {
            float speed = relativeVelocity.magnitude;
            if (speed < Constants.MIN_POSITIVE_VALUE) return Vector3.zero;

            float magnitude = 0.5f * Constants.AIR_DENSITY * dragCoefficient * area * speed * speed;
            return -relativeVelocity.normalized * magnitude;
        }
    }
}
