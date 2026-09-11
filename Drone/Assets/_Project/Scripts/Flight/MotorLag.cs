using System;
using DroneSim.Core;

namespace DroneSim.Flight
{
    public class MotorLag
    {
        private readonly float responseTime;
        private float current;

        public MotorLag(float responseTime)
        {
            this.responseTime = Math.Max(responseTime, Constants.MIN_POSITIVE_VALUE);
        }

        public float Step(float target, float deltaTime)
        {
            if (deltaTime <= 0f) return current;

            // Loc bac mot khong phu thuoc tan so cap nhat.
            float alpha = 1f - (float)Math.Exp(-deltaTime / responseTime);
            current += (target - current) * alpha;
            return current;
        }

        public void Reset() => current = 0f;
    }
}
