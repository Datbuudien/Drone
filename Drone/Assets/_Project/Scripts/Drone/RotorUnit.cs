using DroneSim.Flight;
using UnityEngine;

namespace DroneSim.Drone
{
    public class RotorUnit : MonoBehaviour
    {
        [SerializeField] private Transform mountPoint;
        [SerializeField] private Transform propellerMesh;
        [SerializeField] private bool clockwise;

        private MotorLag lag;
        private Vector3 localMountPosition;

        public bool Clockwise => clockwise;
        public Transform PropellerMesh => propellerMesh;
        public float CurrentThrust { get; private set; }
        public float NormalizedOutput { get; private set; }
        public Vector3 LocalMountPosition => localMountPosition;

        public void Initialize(Transform bodyRoot, float responseTime)
        {
            localMountPosition = bodyRoot.InverseTransformPoint(mountPoint.position);
            lag = new MotorLag(responseTime);
        }

        public void SetCommand(float normalizedCommand, float maxThrust, float deltaTime)
        {
            NormalizedOutput = lag.Step(Mathf.Clamp01(normalizedCommand), deltaTime);
            // Luc day ti le binh phuong RPM.
            CurrentThrust = NormalizedOutput * NormalizedOutput * maxThrust;
        }
    }
}
