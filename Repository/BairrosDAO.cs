using Npgsql;
using ServicoDeOperacaoClienteUsuarios.Util;
using Siscom.Entities;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class BairrosDAO
    {       
        private static string GET_ALL_STATMENT = @"SELECT id, bairro FROM bairro;";

        private static string GET_ALL_POR_ID = @"SELECT id, bairro FROM bairro WHERE id=:condition;";

        #region Metodo recuperar Todos registros
        public List<Bairros> recuperarTodos()
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(GET_ALL_STATMENT, conn);
                dr = stmt.ExecuteReader();

                return converteParaLista(dr);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(String.Format("    {0}\n    {1}", ex.Message, ex.InnerException));
            }
            finally
            {
                GerenteDeConexoes.closeAll(conn);
            }
        }
        #endregion

        #region Metodo recuperar 1 registro por id
        //SELECT id, bairro FROM bairro WHERE id=:condition;
        public Bairros recuperarPorId(long id)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;

            Bairros cc = null;

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(GET_ALL_POR_ID, conn);
                stmt.Parameters.AddWithValue("condition", id);

                dr = stmt.ExecuteReader();

                if (dr.Read())
                {
                    long _id = Convert.ToInt64(dr[0]);
                    string _bairro = dr[1].ToString();

                    cc = new Bairros()
                    {
                        id = _id,
                        bairro = _bairro
                    };
                }

                return cc;
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(String.Format("    {0}\n    {1}", ex.Message, ex.InnerException));
            }
            finally
            {
                GerenteDeConexoes.closeAll(conn);
            }
        }
        #endregion

        #region Metodo converte datareader para list<T>
        //SELECT id, bairro FROM bairro;
        private List<Bairros> converteParaLista(NpgsqlDataReader dr)
        {
            List<Bairros> lista = new List<Bairros>();
            while (dr.Read())
            {
                long _id = Convert.ToInt64(dr[0]);
                string _bairro = dr[1].ToString();

                Bairros centro = new Bairros();
                centro.id = _id;
                centro.bairro = _bairro;

                lista.Add(centro);
            }
            return lista;
        }
        #endregion
    
    }
}
