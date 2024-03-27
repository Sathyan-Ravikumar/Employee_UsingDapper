﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace Employee.Model.Data
{
    public class ContextFile
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public ContextFile(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Connection");
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
