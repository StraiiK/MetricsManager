﻿using MetricsAgent.DAL.InterfaceDal;
using MetricsAgent.Models;
using System.Collections.Generic;
using System.Data.SQLite;
using System;

namespace MetricsAgent.DAL
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private IConnectionManager _connectionManager;

        public RamMetricsRepository(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public IList<BaseMetricModel> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connectionManager.CreateOpenedConnection();
            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "SELECT * FROM RamMetrics WHERE time >= @fromTime AND time <= @toTime";

            cmd.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());
            cmd.Prepare();

            var retirnList = new List<BaseMetricModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    retirnList.Add(new BaseMetricModel
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(2))
                    });
                }
            }

            return retirnList;
        }

        public IList<BaseMetricModel> GetAll()
        {
            using var connection = _connectionManager.CreateOpenedConnection();
            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "SELECT * FROM RamMetrics";

            var returnList = new List<BaseMetricModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new BaseMetricModel
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(2))
                    });
                }
            }
            return returnList;
        }

        public void Create(BaseMetricModel item)
        {
            using var connection = _connectionManager.CreateOpenedConnection();
            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "INSERT INTO RamMetrics(value, time) VALUES(@value, @time)";

            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
    }
}
