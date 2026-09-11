using System;
using DroneSim.Config;
using DroneSim.Core;
using UnityEngine;

namespace DroneSim.Drone
{
    public class DroneRig : MonoBehaviour
    {
        private const string INVALID_ROTOR_COUNT_MESSAGE = "DroneRig requires exactly four rotors.";

        [SerializeField] private Rigidbody body;
        [SerializeField] private RotorUnit[] rotors;
        [SerializeField] private DroneConfig config;

        private Transform tf;
        private float throttleCommand;
        private Vector3 axisCommand;
        private float thrustScale = 1f;

        void Awake()
        {
            if (rotors == null || rotors.Length != Constants.ROTOR_COUNT)
            {
                throw new InvalidOperationException(INVALID_ROTOR_COUNT_MESSAGE);
            }

            tf = transform;
            body.mass = config.Mass.Mass;
            body.centerOfMass = config.Mass.CenterOfMass;
            body.inertiaTensor = config.Mass.InertiaTensor;
            body.linearDamping = 0f;
            body.angularDamping = config.Aero.AngularDamping;
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

            for (int i = 0; i < rotors.Length; i++)
            {
                rotors[i].Initialize(tf, config.Thrust.MotorResponseTime);
            }
        }

        void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            float maxThrust = config.Thrust.MaxThrustPerRotor * thrustScale;
            float totalThrust = 0f;
            float yawThrustDifference = 0f;

            for (int i = 0; i < rotors.Length; i++)
            {
                RotorUnit rotor = rotors[i];
                Vector3 localPosition = rotor.LocalMountPosition;

                float rollTerm = Mathf.Sign(localPosition.x) * axisCommand.x;
                float pitchTerm = -Mathf.Sign(localPosition.z) * axisCommand.y;
                float yawTerm = (rotor.Clockwise ? -1f : 1f) * axisCommand.z;

                rotor.SetCommand(
                    throttleCommand + rollTerm + pitchTerm + yawTerm,
                    maxThrust,
                    deltaTime);

                body.AddForceAtPosition(
                    tf.up * rotor.CurrentThrust,
                    tf.TransformPoint(localPosition));

                totalThrust += rotor.CurrentThrust;
                yawThrustDifference += (rotor.Clockwise ? -1f : 1f) * rotor.CurrentThrust;
            }

            body.AddTorque(tf.up * (yawThrustDifference * config.Thrust.YawTorqueFactor));
            TotalThrust = totalThrust;
        }

        public Rigidbody Body => body;
        public RotorUnit[] Rotors => rotors;
        public float TotalThrust { get; private set; }

        public void SetCommands(float throttle, Vector3 rollPitchYaw)
        {
            throttleCommand = throttle;
            axisCommand = rollPitchYaw;
        }

        public void SetThrustScale(float normalizedScale)
        {
            thrustScale = Mathf.Clamp01(normalizedScale);
        }
    }
}
