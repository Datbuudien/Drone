using UnityEngine;

namespace DroneSim.Rig
{
    public class DroneCameraRig : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 0.5f, -1.5f);
        [SerializeField] private float smoothTime = 0.12f;
        [SerializeField] private float lookAheadDistance = 5f;

        private Transform tf;
        private Vector3 velocity;

        void Awake() => tf = transform;

        void LateUpdate()
        {
            if (target == null) return;

            Vector3 desiredPosition = target.TransformPoint(offset);
            tf.position = Vector3.SmoothDamp(
                tf.position,
                desiredPosition,
                ref velocity,
                smoothTime);
            tf.LookAt(target.position + target.forward * lookAheadDistance);
        }
    }
}
