using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiOnline.ApiBots.Services
{
    public class TriangleRepository
    {
        private readonly IDbConnection _db;

        public TriangleRepository()
        {
            _db = new SqlConnection("server=localhost\\SQLEXPRESS;user=exchange;password=exchange1;database=Exchange");
        }

        public async Task AddTriangleData(DateTime date, string pairs, decimal pair1price, decimal pair2price, decimal pair3price)
        {
            var parameters = new DynamicParameters();
            parameters.Add("pairs", pairs);
            parameters.Add("pair1price", pair1price);
            parameters.Add("pair2price", pair2price);
            parameters.Add("pair3price", pair3price);
            parameters.Add("date", date);

            await _db.ExecuteAsync("AddTriangleData", parameters, commandType: CommandType.StoredProcedure);
        }

    }
}
