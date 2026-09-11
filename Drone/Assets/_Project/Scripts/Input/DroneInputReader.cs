using DroneSim.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DroneSim.Input
{
    public class DroneInputReader : MonoBehaviour
    {
        [SerializeField] private InputActionAsset actions;

        private InputAction throttleAction;
        private InputAction pitchAction;
        private InputAction rollAction;
        private InputAction yawAction;
        private InputAction armAction;
        private InputAction modeAction;

        void Awake()
        {
            InputActionMap map = actions.FindActionMap(Constants.INPUT_MAP_FLIGHT, true);
            throttleAction = map.FindAction(Constants.INPUT_ACTION_THROTTLE, true);
            pitchAction = map.FindAction(Constants.INPUT_ACTION_PITCH, true);
            rollAction = map.FindAction(Constants.INPUT_ACTION_ROLL, true);
            yawAction = map.FindAction(Constants.INPUT_ACTION_YAW, true);
            armAction = map.FindAction(Constants.INPUT_ACTION_TOGGLE_ARM, true);
            modeAction = map.FindAction(Constants.INPUT_ACTION_TOGGLE_FLIGHT_MODE, true);
        }

        void OnEnable()
        {
            actions.Enable();
            armAction.performed += HandleArm;
            modeAction.performed += HandleMode;
        }

        void OnDisable()
        {
            armAction.performed -= HandleArm;
            modeAction.performed -= HandleMode;
            actions.Disable();
        }

        void Update()
        {
            Throttle = throttleAction.ReadValue<float>();
            Pitch = pitchAction.ReadValue<float>();
            Roll = rollAction.ReadValue<float>();
            Yaw = yawAction.ReadValue<float>();
        }

        public float Throttle { get; private set; }
        public float Pitch { get; private set; }
        public float Roll { get; private set; }
        public float Yaw { get; private set; }

        public event System.Action OnArmToggled;
        public event System.Action OnFlightModeToggled;

        private void HandleArm(InputAction.CallbackContext context) => OnArmToggled?.Invoke();
        private void HandleMode(InputAction.CallbackContext context) => OnFlightModeToggled?.Invoke();
    }
}
