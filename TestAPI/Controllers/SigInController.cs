using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SigInController : ControllerBase
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


        [HttpGet(Name = "SigIn")]
        public IEnumerable<SigIn> Post(string login, string password)
        {
            toDB($"execute sigin '{login}', '{password}', 2, 'Тестирование', 'API','','./ava/admin.png'");
            yield return new SigIn
            {
                status = "Ok",
            };
        }
    }
}