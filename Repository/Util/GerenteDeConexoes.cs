using Npgsql;
using System;
namespace Repository
{
        public class GerenteDeConexoes
        {

            //Informacões para conexão com banco de dados POSTGRES.
            private static string DATABASE = "siscomdb";
            private static string STR_SERVER = "127.0.0.1";
            private static string USER = "postgres";
            private static string PASSWORD = "p@ssw0rd";
            private static string PORT = "5432";

            //private static Logger log = Logger.getLogger(GerenteDeConexoes.class);

            public static NpgsqlConnection getConnection()
            {
                NpgsqlConnection conn = null;
                try
                {
                    conn = new NpgsqlConnection(String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", STR_SERVER, PORT, USER, PASSWORD, DATABASE));
                    conn.Open();
                    return conn;
                }
                catch (NpgsqlException ex)
                {
                    throw new Exception(String.Format("EXCEPT: {0}\n\nINNER EXCEPT: {1}", ex.Message, ex.InnerException));
                }
            }


            public static void closeAll(NpgsqlConnection conn)
            {
                try
                {
                    if (conn != null)
                    {
                        conn.Close();
                        //log.debug("Fechada a conexão com banco de dados!");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("EXCEPT: {0}\n\nINNER EXCEPT: {1}", ex.Message, ex.InnerException));
                }
            }
        }
 }
