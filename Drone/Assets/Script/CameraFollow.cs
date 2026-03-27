using UnityEngine;
class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    public Vector3 offset = new Vector3(0,0.5f,-1.0f);
    public float smoothSpeed = 5.0f;
    // public float X = 0.75f;
    // public float Y = 0.75f;
    // public float Z = 0.75f;
    void LateUpdate(){
        if(target == null) return;
        // Vector3 flatForward = target.forward; 
        // flatForward.y = 0; 
        // flatForward.Normalize();
        Vector3 desiredPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed*Time.deltaTime);

        // Debug.DrawRay(transform.position, transform.forward * 15f, Color.red);
        // Debug.DrawRay(target.position, target.forward * 15f, Color.green);

        // Luôn nhìn vào tâm target
        // transform.LookAt(target.position+target.right*X+target.up*Y+target.forward*Z);
        transform.LookAt(target.position+target.right*100f);
    }
}