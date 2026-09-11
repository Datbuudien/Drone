using DroneSim.Config;
using DroneSim.Environment;
using DroneSim.Flight;
using UnityEngine;

namespace DroneSim.Drone
{
    public class DroneAerodynamics : MonoBehaviour
    {
        [SerializeField] private Rigidbody body;
        [SerializeField] private DroneConfig config;
        [SerializeField] private WindProvider wind;

        private Transform tf;

        void Awake() => tf = transform;

        void FixedUpdate()
        {
            Vector3 relativeVelocity = body.linearVelocity - wind.CurrentWind;
            Vector3 localVelocity = tf.InverseTransformDirection(relativeVelocity);

            AeroSpec aero = config.Aero;
            Vector3 horizontalVelocity = new Vector3(localVelocity.x, 0f, localVelocity.z);
            Vector3 verticalVelocity = new Vector3(0f, localVelocity.y, 0f);

            Vector3 localDrag =
                AeroMath.QuadraticDrag(
                    horizontalVelocity,
                    aero.DragCoefficientHorizontal,
                    aero.ReferenceArea) +
                AeroMath.QuadraticDrag(
                    verticalVelocity,
                    aero.DragCoefficientVertical,
                    aero.ReferenceArea);

            body.AddForce(tf.TransformDirection(localDrag));
        }
    }
}
