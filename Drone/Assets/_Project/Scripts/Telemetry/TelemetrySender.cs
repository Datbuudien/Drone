using System.Net.Sockets;
using System.Text;
using DroneSim.Core;
using DroneSim.Drone;
using UnityEngine;

namespace DroneSim.Telemetry
{
    public class TelemetrySender : MonoBehaviour
    {
        [SerializeField] private DroneRig rig;
        [SerializeField] private DroneEngine engine;
        [SerializeField] private DroneBattery battery;
        [SerializeField] private string host = Constants.TELEMETRY_HOST;
        [SerializeField] private int port = Constants.TELEMETRY_PORT;

        private UdpClient client;
        private StringBuilder textBuffer;
        private char[] numberBuffer;
        private char[] characterBuffer;
        private byte[] packetBuffer;
        private float accumulator;
        private int sequence;

        void Awake()
        {
            client = new UdpClient();
            client.Connect(host, port);
            textBuffer = new StringBuilder(
                Constants.TELEMETRY_BUFFER_CAPACITY,
                Constants.TELEMETRY_BUFFER_CAPACITY);
            numberBuffer = new char[Constants.TELEMETRY_NUMBER_BUFFER_CAPACITY];
            characterBuffer = new char[Constants.TELEMETRY_BUFFER_CAPACITY];
            packetBuffer = new byte[Constants.TELEMETRY_BUFFER_CAPACITY];
        }

        void FixedUpdate()
        {
            accumulator += Time.fixedDeltaTime;
            if (accumulator < Constants.TELEMETRY_INTERVAL) return;

            accumulator -= Constants.TELEMETRY_INTERVAL;
            SendFrame();
        }

        void OnDestroy() => CloseClient();

        void OnApplicationQuit() => CloseClient();

        private void SendFrame()
        {
            TelemetrySerializer.Write(CreateFrame(), textBuffer, numberBuffer);

            int characterCount = textBuffer.Length;
            textBuffer.CopyTo(0, characterBuffer, 0, characterCount);
            int byteCount = Encoding.UTF8.GetBytes(
                characterBuffer,
                0,
                characterCount,
                packetBuffer,
                0);
            client.Send(packetBuffer, byteCount);
        }

        private TelemetryFrame CreateFrame()
        {
            Vector3 position = rig.Body.position;
            Vector3 eulerAngles = rig.Body.rotation.eulerAngles;
            Vector3 velocity = rig.Body.linearVelocity;

            TelemetryFrame frame = new TelemetryFrame
            {
                Sequence = sequence,
                TimeStamp = Time.fixedTime,
                PositionX = position.x,
                PositionY = position.y,
                PositionZ = position.z,
                Roll = Mathf.DeltaAngle(0f, eulerAngles.z),
                Pitch = Mathf.DeltaAngle(0f, eulerAngles.x),
                Yaw = Mathf.DeltaAngle(0f, eulerAngles.y),
                VelocityX = velocity.x,
                VelocityY = velocity.y,
                VelocityZ = velocity.z,
                AltitudeAgl = Mathf.Max(position.y, 0f),
                GroundSpeed = Mathf.Sqrt(velocity.x * velocity.x + velocity.z * velocity.z),
                BatteryPercent = battery.StateOfCharge * Constants.PERCENT_SCALE,
                IsArmed = engine.IsArmed
            };

            sequence++;
            return frame;
        }

        private void CloseClient()
        {
            if (client == null) return;

            client.Close();
            client = null;
        }
    }
}
