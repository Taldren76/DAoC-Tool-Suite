namespace DAoCToolSuite.LogTool
{
    internal static class Ratio
    {
        internal static string ToRatioString(this double input)
        {
            if (input == 0) return "0";
            if (input == 1) return "1:1";
            if (input > 1)
            {
                return $"{input:N2}:1";
            }
            else //(input < 0)
            {
                double output = 1 / input;
                return $"1:{output:N2}";
            }
        }
    }
}
