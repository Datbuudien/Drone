using UnityEngine;

namespace DroneSim.Environment
{
    public class WindProvider : MonoBehaviour
    {
        [SerializeField] private Vector3 baseWind = new Vector3(1.5f, 0f, 0f);
        [SerializeField] private float gustStrength = 2f;
        [SerializeField] private float gustFrequency = 0.35f;

        void FixedUpdate()
        {
            float time = Time.time * gustFrequency;
            float gustX = Mathf.PerlinNoise(time, 0f) * 2f - 1f;
            float gustY = Mathf.PerlinNoise(0f, time) * 2f - 1f;
            float gustZ = Mathf.PerlinNoise(time, time) * 2f - 1f;
            CurrentWind = baseWind + new Vector3(gustX, gustY, gustZ) * gustStrength;
        }

        public Vector3 CurrentWind { get; private set; }
    }
}
