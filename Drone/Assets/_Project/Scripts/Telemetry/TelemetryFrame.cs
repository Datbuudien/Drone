namespace DroneSim.Telemetry
{
    [System.Serializable]
    public struct TelemetryFrame
    {
        public int Sequence;
        public float TimeStamp;
        public float PositionX;
        public float PositionY;
        public float PositionZ;
        public float Roll;
        public float Pitch;
        public float Yaw;
        public float VelocityX;
        public float VelocityY;
        public float VelocityZ;
        public float AltitudeAgl;
        public float GroundSpeed;
        public float BatteryPercent;
        public bool IsArmed;
    }
}
