using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLogin.Controllers
{
    public class DataBase
    {
        public dynamic AcaoDataBase(string consulta, int qtdColunas, int qtdLinhas, string TipoConsulta)
        {
            //CONEXAO POR SERVIDOR
            //SqlConnectionStringBuilder builder2 = new SqlConnectionStringBuilder();
            //builder2.DataSource = "servidor";
            //builder2.UserID = "usuario";
            //builder2.Password = "senha";
            //builder2.InitialCatalog = "banco de dados";

            //CONEXAO LOCAL
            SqlConnection builder = new SqlConnection("Data Source=localhost\\SQLEXPRESS; Initial Catalog = DB_PROJECT; Integrated Security=SSPI");

            if (TipoConsulta == "SELECT")
            {
                var resultadoConsulta = new List<dynamic>();
                int contadorColunas = 0;
                int contadorItens = 0;
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append(consulta);
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (contadorItens < qtdLinhas)
                                {

                                    while (contadorColunas < qtdColunas)
                                    {
                                        resultadoConsulta.Add(string.Format("{0}", reader[contadorColunas]));
                                        contadorColunas = contadorColunas + 1;
                                    }
                                    contadorColunas = 0;
                                    contadorItens = contadorItens + 1;
                                }

                            }
                        }
                    }
                }

                return resultadoConsulta;
            }
            else
            {
                //INSERT OU UPDATE
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(consulta);
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();

                        command.ExecuteNonQuery();

                    }
                }

                return null;
            }
        }
    }
}
