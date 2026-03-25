using UnityEngine;
class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    public float distance = 10.0f;
    public float height = 5.0f;
    public float smoothSpeed = 5.0f;
    void LateUpdate(){
        if(target == null) return;
        Vector3 flatForward = target.forward; 
        flatForward.y = 0; 
        flatForward.Normalize();
        Vector3 desiredPosition = target.position - (flatForward*distance) + (Vector3.up*height);
        transform.position = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed*Time.deltaTime);
        transform.LookAt(target);
    }
}