using DroneSim.Input;
using UnityEngine;

namespace DroneSim.Drone
{
    public class DroneEngine : MonoBehaviour
    {
        [SerializeField] private DroneInputReader input;
        [SerializeField] private DroneRig rig;
        [SerializeField] private float groundedSpeedThreshold = 0.05f;

        void OnEnable() => input.OnArmToggled += ToggleArm;

        void OnDisable() => input.OnArmToggled -= ToggleArm;

        public bool IsArmed { get; private set; }

        public event System.Action<bool> OnArmStateChanged;

        private void ToggleArm()
        {
            if (IsArmed && rig.Body.linearVelocity.magnitude > groundedSpeedThreshold) return;

            IsArmed = !IsArmed;
            OnArmStateChanged?.Invoke(IsArmed);
        }
    }
}
