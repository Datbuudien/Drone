using UnityEngine;

namespace DroneSim.Config
{
    [System.Serializable]
    public class MassSpec
    {
        public float Mass = 0.5f;
        public float ArmLength = 0.12f;
        public Vector3 CenterOfMass = Vector3.zero;
        public Vector3 InertiaTensor = new Vector3(0.0045f, 0.0080f, 0.0045f);
    }

    [System.Serializable]
    public class ThrustSpec
    {
        public float MaxThrustPerRotor = 3f;
        public float MotorResponseTime = 0.12f;
        public float YawTorqueFactor = 0.018f;
    }

    [System.Serializable]
    public class AeroSpec
    {
        public float DragCoefficientHorizontal = 0.9f;
        public float DragCoefficientVertical = 1.3f;
        public float ReferenceArea = 0.045f;
        public float AngularDamping = 0.15f;
    }

    [System.Serializable]
    public class PidSpec
    {
        public float RateKp = 0.09f;
        public float RateKi = 0.04f;
        public float RateKd = 0.0012f;
        public float AngleP = 5f;
        public float MaxTiltAngle = 35f;
        public float MaxYawRate = 180f;
        public float IntegralLimit = 0.3f;
    }

    [System.Serializable]
    public class BatterySpec
    {
        public float CapacityMilliAmpHours = 1500f;
        public float NominalVoltage = 14.8f;
        public float MinVoltage = 12.8f;
        public float HoverCurrentAmp = 9f;
    }

    [CreateAssetMenu(fileName = "DroneConfig", menuName = "DroneSim/Drone Config")]
    public class DroneConfig : ScriptableObject
    {
        [SerializeField] private MassSpec massSpec = new MassSpec();
        [SerializeField] private ThrustSpec thrustSpec = new ThrustSpec();
        [SerializeField] private AeroSpec aeroSpec = new AeroSpec();
        [SerializeField] private PidSpec pidSpec = new PidSpec();
        [SerializeField] private BatterySpec batterySpec = new BatterySpec();

        public MassSpec Mass => massSpec;
        public ThrustSpec Thrust => thrustSpec;
        public AeroSpec Aero => aeroSpec;
        public PidSpec Pid => pidSpec;
        public BatterySpec Battery => batterySpec;
    }
}
