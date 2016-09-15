using System;

namespace ACTransit.Training.Web.Domain.Extensions
{
    public class MathUtil
    {
        public static string RoundShort(decimal number)
        {
            return (Math.Round(number, 0) == number) ? number.ToString("0") : number.ToString("0.0");
        }
    }
}
