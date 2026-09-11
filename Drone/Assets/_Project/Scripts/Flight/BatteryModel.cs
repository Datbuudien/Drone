using System;
using DroneSim.Core;

namespace DroneSim.Flight
{
    public class BatteryModel
    {
        private readonly float capacityAmpSeconds;
        private readonly float nominalVoltage;
        private readonly float minVoltage;
        private readonly float hoverCurrent;

        private float consumedAmpSeconds;

        public float StateOfCharge => Clamp01(1f - consumedAmpSeconds / capacityAmpSeconds);
        public float Voltage => minVoltage + (nominalVoltage - minVoltage) * StateOfCharge;

        public BatteryModel(
            float capacityMilliAmpHours,
            float nominalVoltage,
            float minVoltage,
            float hoverCurrent)
        {
            capacityAmpSeconds = Math.Max(
                capacityMilliAmpHours * Constants.MILLIAMP_HOURS_TO_AMP_SECONDS,
                Constants.MIN_POSITIVE_VALUE);
            this.nominalVoltage = nominalVoltage;
            this.minVoltage = minVoltage;
            this.hoverCurrent = Math.Max(hoverCurrent, 0f);
        }

        public void Drain(float thrustRatio, float deltaTime)
        {
            if (deltaTime <= 0f) return;

            float current = hoverCurrent * Math.Max(thrustRatio, Constants.MIN_BATTERY_CURRENT_RATIO);
            consumedAmpSeconds += current * deltaTime;
        }

        private static float Clamp01(float value)
        {
            return Math.Max(0f, Math.Min(value, 1f));
        }
    }
}
