using System;

namespace DroneSim.Flight
{
    public class PidController
    {
        private readonly float kp;
        private readonly float ki;
        private readonly float kd;
        private readonly float integralLimit;

        private float integral;
        private float previousError;
        private bool hasPreviousError;

        public PidController(float kp, float ki, float kd, float integralLimit)
        {
            this.kp = kp;
            this.ki = ki;
            this.kd = kd;
            this.integralLimit = Math.Abs(integralLimit);
        }

        public float Step(float error, float deltaTime)
        {
            if (deltaTime <= 0f) return 0f;

            integral = Math.Max(
                -integralLimit,
                Math.Min(integral + error * deltaTime, integralLimit));
            float derivative = hasPreviousError
                ? (error - previousError) / deltaTime
                : 0f;

            previousError = error;
            hasPreviousError = true;

            return kp * error + ki * integral + kd * derivative;
        }

        public void Reset()
        {
            integral = 0f;
            previousError = 0f;
            hasPreviousError = false;
        }
    }
}
