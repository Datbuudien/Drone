using DroneSim.Config;
using DroneSim.Core;
using DroneSim.Flight;
using UnityEngine;

namespace DroneSim.Drone
{
    public class DroneBattery : MonoBehaviour
    {
        [SerializeField] private DroneRig rig;
        [SerializeField] private DroneConfig config;

        private BatteryModel model;
        private float hoverThrust;

        void Awake()
        {
            BatterySpec battery = config.Battery;
            model = new BatteryModel(
                battery.CapacityMilliAmpHours,
                battery.NominalVoltage,
                battery.MinVoltage,
                battery.HoverCurrentAmp);
            hoverThrust = Mathf.Max(
                config.Mass.Mass * Constants.GRAVITY,
                Constants.MIN_POSITIVE_VALUE);
        }

        void FixedUpdate()
        {
            float thrustRatio = rig.TotalThrust / hoverThrust;
            model.Drain(thrustRatio, Time.fixedDeltaTime);
            rig.SetThrustScale(ThrustScale);
        }

        public float StateOfCharge => model.StateOfCharge;
        public float Voltage => model.Voltage;
        public float ThrustScale => Mathf.Clamp01(
            Voltage / Mathf.Max(config.Battery.NominalVoltage, Constants.MIN_POSITIVE_VALUE));
    }
}
