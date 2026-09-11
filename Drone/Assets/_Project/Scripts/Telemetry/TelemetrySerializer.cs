using System.Globalization;
using System.Text;

namespace DroneSim.Telemetry
{
    public static class TelemetrySerializer
    {
        private const string FORMAT = "F3";
        private const char DELIMITER = ';';
        private static readonly CultureInfo CULTURE = CultureInfo.InvariantCulture;

        public static string Serialize(TelemetryFrame frame, StringBuilder buffer)
        {
            Write(frame, buffer);
            return buffer.ToString();
        }

        public static void Write(TelemetryFrame frame, StringBuilder buffer)
        {
            buffer.Clear();
            buffer.Append(frame.Sequence).Append(DELIMITER);
            Append(buffer, frame.TimeStamp);
            Append(buffer, frame.PositionX);
            Append(buffer, frame.PositionY);
            Append(buffer, frame.PositionZ);
            Append(buffer, frame.Roll);
            Append(buffer, frame.Pitch);
            Append(buffer, frame.Yaw);
            Append(buffer, frame.VelocityX);
            Append(buffer, frame.VelocityY);
            Append(buffer, frame.VelocityZ);
            Append(buffer, frame.AltitudeAgl);
            Append(buffer, frame.GroundSpeed);
            Append(buffer, frame.BatteryPercent);
            buffer.Append(frame.IsArmed ? '1' : '0');
        }

        public static void Write(TelemetryFrame frame, StringBuilder buffer, char[] numberBuffer)
        {
            buffer.Clear();
            Append(buffer, frame.Sequence, numberBuffer);
            Append(buffer, frame.TimeStamp, numberBuffer);
            Append(buffer, frame.PositionX, numberBuffer);
            Append(buffer, frame.PositionY, numberBuffer);
            Append(buffer, frame.PositionZ, numberBuffer);
            Append(buffer, frame.Roll, numberBuffer);
            Append(buffer, frame.Pitch, numberBuffer);
            Append(buffer, frame.Yaw, numberBuffer);
            Append(buffer, frame.VelocityX, numberBuffer);
            Append(buffer, frame.VelocityY, numberBuffer);
            Append(buffer, frame.VelocityZ, numberBuffer);
            Append(buffer, frame.AltitudeAgl, numberBuffer);
            Append(buffer, frame.GroundSpeed, numberBuffer);
            Append(buffer, frame.BatteryPercent, numberBuffer);
            buffer.Append(frame.IsArmed ? '1' : '0');
        }

        private static void Append(StringBuilder buffer, float value)
        {
            buffer.Append(value.ToString(FORMAT, CULTURE)).Append(DELIMITER);
        }

        private static void Append(StringBuilder buffer, float value, char[] numberBuffer)
        {
            value.TryFormat(numberBuffer, out int written, FORMAT, CULTURE);
            buffer.Append(numberBuffer, 0, written).Append(DELIMITER);
        }

        private static void Append(StringBuilder buffer, int value, char[] numberBuffer)
        {
            value.TryFormat(numberBuffer, out int written, default, CULTURE);
            buffer.Append(numberBuffer, 0, written).Append(DELIMITER);
        }
    }
}
