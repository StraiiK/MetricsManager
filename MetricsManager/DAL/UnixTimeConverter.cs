using Dapper;
using System;
using System.Data;

namespace MetricsManager.DAL
{
    public static class UnixTimeConverter
    {
        public static DateTimeOffset FromUnixTime(long value) => DateTimeOffset.FromUnixTimeMilliseconds((long)value);
        public static long ToUnixTime(DateTimeOffset value) => value.ToUnixTimeMilliseconds();
    }
}
