using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        #region[Работа с базой данных]

        public static SqlConnection SqlConnection()
        {
            return new SqlConnection(@"Data Source=KAURCEV-SERVER\SQLEXPRESS; Initial Catalog =teatre; Integrated Security=True");
        }

        public static void toDB(string stroke)
        {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(stroke, SqlConnection());
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
        }
        public static DataTable fromDB(string stroke)
        {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(stroke, SqlConnection());
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                return dataTable;
        }
        #endregion


        [HttpGet(Name = "GetAuth")]
        public IEnumerable<Auth> Get(string login, string password)
        {
            DataTable dataTable = fromDB($"select * from _users where _login = '{login}' and _password = '{password}'");
            return Enumerable.Range(0, dataTable.Rows.Count).Select(index => new Auth
            {
                userid = Convert.ToInt32(dataTable.Rows[index][0]),
                name = dataTable.Rows[index][4].ToString(),
                surname = dataTable.Rows[index][5].ToString(),
                firstname = dataTable.Rows[index][6].ToString(),
            }).ToArray();
        }
    }
}