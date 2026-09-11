using DroneSim.Drone;
using UnityEngine;

namespace DroneSim.Visual
{
    public class PropellerVisual : MonoBehaviour
    {
        [SerializeField] private DroneRig rig;
        [SerializeField] private float maxSpinSpeed = 3000f;

        void Update()
        {
            RotorUnit[] rotors = rig.Rotors;
            float deltaTime = Time.deltaTime;

            for (int i = 0; i < rotors.Length; i++)
            {
                RotorUnit rotor = rotors[i];
                float direction = rotor.Clockwise ? 1f : -1f;
                float speed = rotor.NormalizedOutput * maxSpinSpeed * direction;
                rotor.PropellerMesh.Rotate(0f, 0f, speed * deltaTime, Space.Self);
            }
        }
    }
}
