﻿namespace WebApplication1.Extensions
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrEmpty(s.Trim());
        }
    }
}
