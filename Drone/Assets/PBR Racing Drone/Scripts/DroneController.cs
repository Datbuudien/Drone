using UnityEngine;
using UnityEngine.InputSystem;
public class DroneController : MonoBehaviour
{
    [Header ("Drone Config")]
    public float liftForce = 15f;
    private Rigidbody rb; 
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Vector3 pos = transform.position;
        Debug.Log($"X : {pos.x:F2}  Y : {pos.y:F2}   Z : {pos.z:F2} ");   
    }
    void FixedUpdate()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            rb.AddForce(Vector3.up*liftForce);
        }
    }
}
