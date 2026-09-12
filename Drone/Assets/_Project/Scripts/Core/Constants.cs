namespace DroneSim.Core
{
    public static class Constants
    {
        public const float GRAVITY = 9.81f;
        public const float AIR_DENSITY = 1.225f;
        public const int ROTOR_COUNT = 4;

        public const string TELEMETRY_HOST = "127.0.0.1";
        public const int TELEMETRY_PORT = 5000;
        public const float TELEMETRY_INTERVAL = 1f / 30f;
        public const int TELEMETRY_BUFFER_CAPACITY = 512;
        public const int TELEMETRY_NUMBER_BUFFER_CAPACITY = 64;

        public const string INPUT_MAP_FLIGHT = "Flight";
        public const string INPUT_ACTION_THROTTLE = "Throttle";
        public const string INPUT_ACTION_PITCH = "Pitch";
        public const string INPUT_ACTION_ROLL = "Roll";
        public const string INPUT_ACTION_YAW = "Yaw";
        public const string INPUT_ACTION_TOGGLE_ARM = "ToggleArm";
        public const string INPUT_ACTION_TOGGLE_FLIGHT_MODE = "ToggleFlightMode";

        public const float THROTTLE_CHANGE_RATE = 0.5f;
        public const float ARMED_IDLE_COMMAND = 0.08f;
        public const float MAX_AXIS_MIX_COMMAND = 0.25f;

        public const string TAG_GROUND = "Ground";

        public const float MIN_POSITIVE_VALUE = 0.0001f;
        public const float MIN_BATTERY_CURRENT_RATIO = 0.1f;
        public const float MILLIAMP_HOURS_TO_AMP_SECONDS = 3.6f;
        public const float PERCENT_SCALE = 100f;
    }
}
