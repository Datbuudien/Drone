using DroneSim.Config;
using DroneSim.Flight;
using DroneSim.Input;
using UnityEngine;

namespace DroneSim.Drone
{
    public enum FlightMode
    {
        Angle,
        Acro
    }

    public class FlightController : MonoBehaviour
    {
        [SerializeField] private DroneRig rig;
        [SerializeField] private DroneEngine engine;
        [SerializeField] private DroneInputReader input;
        [SerializeField] private DroneConfig config;
        [SerializeField] private FlightMode mode = FlightMode.Angle;

        private PidController rollRatePid;
        private PidController pitchRatePid;
        private PidController yawRatePid;
        private Transform tf;

        void Awake()
        {
            tf = transform;
            PidSpec pid = config.Pid;
            rollRatePid = new PidController(pid.RateKp, pid.RateKi, pid.RateKd, pid.IntegralLimit);
            pitchRatePid = new PidController(pid.RateKp, pid.RateKi, pid.RateKd, pid.IntegralLimit);
            yawRatePid = new PidController(pid.RateKp, pid.RateKi, pid.RateKd, pid.IntegralLimit);
        }

        void OnEnable() => input.OnFlightModeToggled += ToggleMode;

        void OnDisable() => input.OnFlightModeToggled -= ToggleMode;

        void FixedUpdate()
        {
            if (!engine.IsArmed)
            {
                rig.SetCommands(0f, Vector3.zero);
                ResetPids();
                return;
            }

            float deltaTime = Time.fixedDeltaTime;
            Vector3 desiredRate = mode == FlightMode.Angle
                ? ComputeAngleModeRate()
                : ComputeAcroRate();
            Vector3 currentRate = tf.InverseTransformDirection(rig.Body.angularVelocity) * Mathf.Rad2Deg;
            float rateScale = Mathf.Max(config.Pid.MaxYawRate, DroneSim.Core.Constants.MIN_POSITIVE_VALUE);

            Vector3 command = new Vector3(
                rollRatePid.Step((desiredRate.x - currentRate.z) / rateScale, deltaTime),
                pitchRatePid.Step((desiredRate.y - currentRate.x) / rateScale, deltaTime),
                yawRatePid.Step((desiredRate.z - currentRate.y) / rateScale, deltaTime));

            rig.SetCommands(input.Throttle, command);
        }

        public FlightMode Mode => mode;

        private Vector3 ComputeAngleModeRate()
        {
            PidSpec pid = config.Pid;
            float targetRoll = input.Roll * pid.MaxTiltAngle;
            float targetPitch = input.Pitch * pid.MaxTiltAngle;

            // Suy goc than tu huong world-up trong he truc cuc bo.
            Vector3 localUp = tf.InverseTransformDirection(Vector3.up);
            float currentRoll = Mathf.Atan2(localUp.x, localUp.y) * Mathf.Rad2Deg;
            float currentPitch = Mathf.Atan2(-localUp.z, localUp.y) * Mathf.Rad2Deg;

            return new Vector3(
                (targetRoll - currentRoll) * pid.AngleP,
                (targetPitch - currentPitch) * pid.AngleP,
                input.Yaw * pid.MaxYawRate);
        }

        private Vector3 ComputeAcroRate()
        {
            PidSpec pid = config.Pid;
            return new Vector3(
                input.Roll * pid.MaxYawRate,
                input.Pitch * pid.MaxYawRate,
                input.Yaw * pid.MaxYawRate);
        }

        private void ResetPids()
        {
            rollRatePid.Reset();
            pitchRatePid.Reset();
            yawRatePid.Reset();
        }

        private void ToggleMode()
        {
            mode = mode == FlightMode.Angle ? FlightMode.Acro : FlightMode.Angle;
            ResetPids();
        }
    }
}
