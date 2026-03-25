using UnityEngine;
using UnityEngine.InputSystem;
using System.Net; 
using System.Net.Sockets;
using System.Text;
public class DroneController : MonoBehaviour
{
    [Header ("Drone Config")]
    public float liftForce = 15f;
    private Rigidbody rb; 
    private UdpClient udpClient;
    private IPEndPoint endPoint;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        udpClient = new UdpClient();
        endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),5000);
    }
    void Update()
    {
        Vector3 pos = transform.position;
        Debug.Log($"X : {pos.x:F2}  Y : {pos.y:F2}   Z : {pos.z:F2} ");   
        string mess = $"X:{pos.x:F2}  Y: {pos.y:F2}  Z: {pos.z:F2} ";
        byte[] bytes = Encoding.UTF8.GetBytes(mess);
        udpClient.Send(bytes,bytes.Length,endPoint);
    }
    void FixedUpdate()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            rb.AddForce(Vector3.up*liftForce);
        }
    }
    void OnApplicationQuit()
    {
        if(udpClient !=null) udpClient.Close();
    }
}
