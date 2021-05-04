using MetricsAgent.Metrics;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DataAccessLayer
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly string _connectionString;

        public CpuMetricsRepository(IDatabaseSettingsProvider dbProvider)
        {
            _connectionString = dbProvider.GetConnectionString();
        }

        public void Create(CpuMetric item)
        {
             using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var cmd = new SqliteCommand(_connectionString)
            {
                CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)"
            };
            cmd.Parameters.AddWithValue("@value", item.Value);

            
            cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());
         
            cmd.Prepare();
           
            cmd.ExecuteNonQuery();
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var cmd = new SqliteCommand(_connectionString);

            cmd.CommandText = string.Format("SELECT * FROM cpumetrics WHERE time BETWEEN {0} AND {1}",
                from.ToUnixTimeSeconds(),
                to.ToUnixTimeSeconds());

            var returnList = new List<CpuMetric>();

            using SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                returnList.Add(new CpuMetric
                {
                    Id = reader.GetInt32(0),
                    Value = reader.GetInt32(1),
                    Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2))
                });
            }

            return returnList;
        }

     
        public void Delete(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var cmd = new SqliteCommand(_connectionString);
            
            cmd.CommandText = "DELETE FROM cpumetrics WHERE id=@id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Update(CpuMetric item)
        {
            using var connection = new SqliteConnection(_connectionString);
            using var cmd = new SqliteCommand(_connectionString);
           
            cmd.CommandText = "UPDATE cpumetrics SET value = @value, time = @time WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public IList<CpuMetric> GetAll()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var cmd = new SqliteCommand(_connectionString);

           
            cmd.CommandText = "SELECT * FROM cpumetrics";

            var returnList = new List<CpuMetric>();

            using (SqliteDataReader reader = cmd.ExecuteReader())
            {
               
                while (reader.Read())
                {
                    
                    returnList.Add(new CpuMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64(2))
                    });
                }
            }

            return returnList;
        }

        public CpuMetric GetById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var cmd = new SqliteCommand(_connectionString);
            cmd.CommandText = "SELECT * FROM cpumetrics WHERE id=@id";
            using (SqliteDataReader reader = cmd.ExecuteReader())
            {
               
                if (reader.Read())
                {
                    
                    return new CpuMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64(2))
                    };
                }

              
                return null;
            }
        }
    }
}
