using DroneSim.Flight;
using NUnit.Framework;
using UnityEngine;

namespace DroneSim.Tests
{
    public class FlightMathTests
    {
        private const float COMPARISON_TOLERANCE = 0.001f;

        [Test]
        public void MotorLag_ConstantTarget_ConvergesRegardlessOfTimestep()
        {
            MotorLag coarseLag = new MotorLag(0.12f);
            MotorLag fineLag = new MotorLag(0.12f);
            float coarseOutput = RunMotorLag(coarseLag, 0.02f, 50);
            float fineOutput = RunMotorLag(fineLag, 0.005f, 200);

            Assert.That(coarseOutput, Is.EqualTo(fineOutput).Within(COMPARISON_TOLERANCE));
        }

        [Test]
        public void AeroMath_DoubleSpeed_QuadruplesDrag()
        {
            Vector3 slowDrag = AeroMath.QuadraticDrag(Vector3.right * 2f, 0.9f, 0.045f);
            Vector3 fastDrag = AeroMath.QuadraticDrag(Vector3.right * 4f, 0.9f, 0.045f);

            Assert.That(
                fastDrag.magnitude,
                Is.EqualTo(slowDrag.magnitude * 4f).Within(COMPARISON_TOLERANCE));
        }

        [Test]
        public void AeroMath_ZeroVelocity_ReturnsZero()
        {
            Vector3 drag = AeroMath.QuadraticDrag(Vector3.zero, 0.9f, 0.045f);

            Assert.That(Mathf.Approximately(drag.sqrMagnitude, 0f), Is.True);
        }

        [Test]
        public void PidController_ZeroError_ReturnsZero()
        {
            PidController controller = new PidController(0.09f, 0.04f, 0.0012f, 0.3f);

            Assert.That(Mathf.Approximately(controller.Step(0f, 0.02f), 0f), Is.True);
        }

        [Test]
        public void PidController_SustainedError_IntegralClamped()
        {
            PidController controller = new PidController(0f, 1f, 0f, 0.3f);

            controller.Step(1f, 1f);
            float output = controller.Step(1f, 1f);

            Assert.That(output, Is.EqualTo(0.3f).Within(COMPARISON_TOLERANCE));
        }

        [Test]
        public void PidController_FirstSample_DoesNotCreateDerivativeKick()
        {
            PidController controller = new PidController(0f, 0f, 1f, 0.3f);

            float output = controller.Step(1f, 0.02f);

            Assert.That(Mathf.Approximately(output, 0f), Is.True);
        }

        [Test]
        public void BatteryModel_FullDrain_ReachesZero()
        {
            BatteryModel model = new BatteryModel(1f, 14.8f, 12.8f, 1f);

            model.Drain(1f, 3.6f);

            Assert.That(Mathf.Approximately(model.StateOfCharge, 0f), Is.True);
        }

        private static float RunMotorLag(MotorLag motorLag, float deltaTime, int stepCount)
        {
            float output = 0f;
            for (int i = 0; i < stepCount; i++)
            {
                output = motorLag.Step(1f, deltaTime);
            }

            return output;
        }
    }
}
