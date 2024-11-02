using UnityEngine;

namespace CodeBase.Common.Extensions
{
    public static class ColorExtensions
    {
        public static string ToHex(this Color color) => 
            $"#{(byte)color.r*255:X2}{(byte)color.g*255:X2}{(byte)color.b*255:X2}FF";

        public static string Paint(this Color color, string message) => 
            $"<color={color.ToHex()}>{message}</color>";
    }
}