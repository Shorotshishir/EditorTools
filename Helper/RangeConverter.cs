using System;
using UnityEngine;

namespace siratim.Tools
{
    public class RangeConverter
    {
        // const float decimalTolerance = 0.0000001f;
        float decimalTolerance = Mathf.Epsilon;

        public static float Convert(float oldMin, float oldMax ,
                                    float newMin, float newMax, float value)
        {
            var denominator = oldMax - oldMin;
            if (Math.Abs(denominator) < Mathf.Epsilon) denominator = 1; // preventing divide by zero 
            var result = ((value - oldMin) / denominator)
                * (newMax - newMin)
                + newMin;
            return result;
        }
    }
}
