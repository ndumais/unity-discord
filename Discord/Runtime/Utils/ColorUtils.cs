//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.Discord.Utils
{
    public static class ColorUtils
    {
        public static int ToColorCode(this Color32 color)
        {
            return (color.r * 256 * 256) + (color.g * 256) + color.b;
        }
    }
}
