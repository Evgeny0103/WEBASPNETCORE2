using MetricsAgent.Metrics;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MetricsAgent.DataAccessLayer
{
    
        public class NetworkMetricsRepository : INetworkMetricsRepository
        {
            private readonly string _connectionString;

            public NetworkMetricsRepository(IDatabaseSettingsProvider dbProvider)
            {
                _connectionString = dbProvider.GetConnectionString();
            }

            public void Create(NetworkMetric item)
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();

                using var cmd = new SqliteCommand(_connectionString)
                {
                    CommandText = "INSERT INTO networkmetrics(value, time) VALUES(@value, @time)"
                };

                cmd.Parameters.AddWithValue("@value", item.Value);
                cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

            public IList<NetworkMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();
                using var cmd = new SqliteCommand(_connectionString)
                {
                    CommandText = string.Format("SELECT * FROM networkmetrics WHERE time BETWEEN {0} AND {1}",
                        from.ToUnixTimeSeconds(),
                        to.ToUnixTimeSeconds())
                };


                var returnList = new List<NetworkMetric>();

                using SqliteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2)).LocalDateTime
                    });
                }

                return returnList;
            }
        }
    }

