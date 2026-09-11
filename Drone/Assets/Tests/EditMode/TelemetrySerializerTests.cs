using System.Globalization;
using System.Text;
using DroneSim.Telemetry;
using NUnit.Framework;

namespace DroneSim.Tests
{
    public class TelemetrySerializerTests
    {
        private const int TELEMETRY_FIELD_COUNT = 15;

        [Test]
        public void TelemetrySerializer_VietnameseLocale_UsesDotSeparator()
        {
            CultureInfo previousCulture = CultureInfo.CurrentCulture;

            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("vi-VN");
                TelemetryFrame frame = new TelemetryFrame { PositionX = 1.25f };
                string serialized = TelemetrySerializer.Serialize(frame, new StringBuilder());

                StringAssert.Contains("1.250", serialized);
                StringAssert.DoesNotContain("1,250", serialized);
            }
            finally
            {
                CultureInfo.CurrentCulture = previousCulture;
            }
        }

        [Test]
        public void TelemetrySerializer_CompleteFrame_EmitsFifteenFields()
        {
            string serialized = TelemetrySerializer.Serialize(
                new TelemetryFrame(),
                new StringBuilder());

            Assert.That(serialized.Split(';').Length, Is.EqualTo(TELEMETRY_FIELD_COUNT));
        }
    }
}
