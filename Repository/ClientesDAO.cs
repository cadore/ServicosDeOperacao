using Npgsql;
using Siscom.Entities;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class ClientesDAO
    {
        #region queries
        private static string INSERT_STATEMENT = @"INSERT INTO clientes(
                                                        nome, tipo_de_documento, documento, ie, isento_icms, email_principal, 
                                                        email_secundario, numero, complemento, cep, observacoes, telefone_fixo, 
                                                        telefone_celular, inativo, limite_de_credito, id_cidades, id_bairros, 
                                                        id_enderecos)
                                                        VALUES (:nome, :tipo_de_documento, :documento, :ie, :isento_icms, :email_principal, 
                                                        :email_secundario, :numero, :complemento, :cep, :observacoes, :telefone_fixo, 
                                                        :telefone_celular, :inativo, :limite_de_credito, :id_cidades, :id_bairros, 
                                                        :id_enderecos) RETURNING id;";

        private static string UPDATE_STATEMENT = @"UPDATE clientes
                                                        SET nome=:nome, tipo_de_documento=:tipo_de_documento, documento=:documento, ie=:ie, isento_icms=:isento_icms, 
                                                        email_principal=:email_principal, email_secundario=:email_secundario, numero=:numero, complemento=:complemento, 
                                                        cep=:cep, observacoes=:observacoes, telefone_fixo=:telefone_fixo, telefone_celular=:telefone_celular, inativo=:inativo, 
                                                        limite_de_credito=:limite_de_credito, id_cidades=:id_cidades, id_bairros=:id_bairros, id_enderecos=:id_enderecos
                                                        WHERE id=:condition  RETURNING id;";

        private static string GET_ALL_STATMENT = @"SELECT id, nome, tipo_de_documento, documento, ie, isento_icms, email_principal, 
                                                    email_secundario, numero, complemento, cep, observacoes, telefone_fixo, 
                                                    telefone_celular, inativo, limite_de_credito, id_cidades, id_bairros, 
                                                    id_enderecos FROM clientes;";

        private static string GET_ALL_POR_ID = @"SELECT id, nome, tipo_de_documento, documento, ie, isento_icms, email_principal, 
                                                    email_secundario, numero, complemento, cep, observacoes, telefone_fixo, 
                                                    telefone_celular, inativo, limite_de_credito, id_cidades, id_bairros, 
                                                    id_enderecos FROM clientes WHERE id=:condition;";

        private static string GET_ALL_POR_NOME = @"SELECT id, nome, tipo_de_documento, documento, ie, isento_icms, email_principal, 
                                                    email_secundario, numero, complemento, cep, observacoes, telefone_fixo, 
                                                    telefone_celular, inativo, limite_de_credito, id_cidades, id_bairros, 
                                                    id_enderecos FROM clientes WHERE nome ILIKE :condition;";

        private static string GET_ALL_POR_DOCUMENTO = @"SELECT id, nome, tipo_de_documento, documento, ie, isento_icms, email_principal, 
                                                    email_secundario, numero, complemento, cep, observacoes, telefone_fixo, 
                                                    telefone_celular, inativo, limite_de_credito, id_cidades, id_bairros, 
                                                    id_enderecos FROM clientes WHERE documento=:condition;";
        #endregion

        #region Metodo Salvar/Atualizar
        public int salvar(Clientes obj)
        {
            if (obj == null)
            {
                throw new Exception("Informe o Cliente e seus dados para salvar!");
            }

            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlTransaction trans = null;

            Object rs = null;
            try
            {
                conn = GerenteDeConexoes.getConnection();
                trans = (NpgsqlTransaction)conn.BeginTransaction();

                //insert
                if (obj.id == 0)
                {
                    stmt = getStatementInsert(conn, obj);                    
                }
                //update
                else
                {
                    stmt = getStatementUpdate(conn, obj);
                }

                rs = stmt.ExecuteScalar();

                trans.Commit();

                return Convert.ToInt32(rs);
            }
            catch (NpgsqlException ex)
            {
                try { trans.Rollback(); }
                catch (Exception l) 
                {
                    Console.WriteLine(l.Message);
                }
                throw new Exception(String.Format("Erro ao salvar/atualizar Clifor{0} EXCEPT: {1}\n\nINNER EXCEPT: {2}", Environment.NewLine, ex.Message, ex.InnerException));
            }
            finally
            {
                GerenteDeConexoes.closeAll(conn);
            }
        }

        #endregion

        #region Preparação de Statements
        /*nome, tipo_de_documento, documento, ie, isento_icms, email_principal, 
                email_secundario, numero, complemento, cep, observacoes, telefone_fixo, 
                telefone_celular, inativo, limite_de_credito, id_cidades, id_bairros, 
                id_enderecos)
                VALUES (:nome, :tipo_de_documento, :documento, :ie, :isento_icms, :email_principal, 
                :email_secundario, :numero, :complemento, :cep, :observacoes, :telefone_fixo, 
                :telefone_celular, :inativo, :limite_de_credito, :id_cidades, :id_bairros, 
                :id_enderecos*/
        private NpgsqlCommand getStatementInsert(NpgsqlConnection conn, Clientes obj)
        {
            NpgsqlCommand stmt = new NpgsqlCommand(INSERT_STATEMENT, conn);
            stmt.Parameters.AddWithValue(":nome", obj.nome);
            stmt.Parameters.AddWithValue(":tipo_de_documento", obj.tipo_de_documento);
            stmt.Parameters.AddWithValue(":documento", obj.documento);
            stmt.Parameters.AddWithValue(":ie", obj.ie);
            stmt.Parameters.AddWithValue(":isento_icms", obj.isento_ICMS);
            stmt.Parameters.AddWithValue(":email_principal", obj.email_principal);
            stmt.Parameters.AddWithValue(":email_secundario", obj.email_secundario);
            stmt.Parameters.AddWithValue(":numero", obj.numero);
            stmt.Parameters.AddWithValue(":complemento", obj.complemento);
            stmt.Parameters.AddWithValue(":cep", obj.cep);
            stmt.Parameters.AddWithValue(":observacoes", obj.observacoes);
            stmt.Parameters.AddWithValue(":telefone_fixo", obj.telefone_fixo);
            stmt.Parameters.AddWithValue(":telefone_celular", obj.telefone_celular);
            stmt.Parameters.AddWithValue(":inativo", obj.inativo);
            stmt.Parameters.AddWithValue(":limite_de_credito", obj.limite_de_credito);
            stmt.Parameters.AddWithValue(":id_cidades", obj.id_cidades);
            stmt.Parameters.AddWithValue(":id_bairros", obj.id_bairros);
            stmt.Parameters.AddWithValue(":id_enderecos", obj.id_enderecos);

            return stmt;
        }

        /*UPDATE clientes
                SET nome=:nome, tipo_de_documento=:tipo_de_documento, documento=:documento, ie=:ie, isento_icms=:isento_icms, 
                email_principal=:email_principal, email_secundario=:email_secundario, numero=:numero, complemento=:complemento, 
                cep=:cep, observacoes=:observacoes, telefone_fixo=:telefone_fixo, telefone_celular=:telefone_celular, inativo=:inativo, 
                limite_de_credito=:limite_de_credito, id_cidades=:id_cidades, id_bairros=:id_bairros, id_enderecos=:id_enderecos
                WHERE id=:condition  RETURNING id;*/
        private NpgsqlCommand getStatementUpdate(NpgsqlConnection conn, Clientes obj)
        {
            NpgsqlCommand stmt = new NpgsqlCommand(UPDATE_STATEMENT, conn);
            stmt.Parameters.AddWithValue(":nome", obj.nome);
            stmt.Parameters.AddWithValue(":tipo_de_documento", obj.tipo_de_documento);
            stmt.Parameters.AddWithValue(":documento", obj.documento);
            stmt.Parameters.AddWithValue(":ie", obj.ie);
            stmt.Parameters.AddWithValue(":isento_icms", obj.isento_ICMS);
            stmt.Parameters.AddWithValue(":email_principal", obj.email_principal);
            stmt.Parameters.AddWithValue(":email_secundario", obj.email_secundario);
            stmt.Parameters.AddWithValue(":numero", obj.numero);
            stmt.Parameters.AddWithValue(":complemento", obj.complemento);
            stmt.Parameters.AddWithValue(":cep", obj.cep);
            stmt.Parameters.AddWithValue(":observacoes", obj.observacoes);
            stmt.Parameters.AddWithValue(":telefone_fixo", obj.telefone_fixo);
            stmt.Parameters.AddWithValue(":telefone_celular", obj.telefone_celular);
            stmt.Parameters.AddWithValue(":inativo", obj.inativo);
            stmt.Parameters.AddWithValue(":limite_de_credito", obj.limite_de_credito);
            stmt.Parameters.AddWithValue(":id_cidades", obj.id_cidades);
            stmt.Parameters.AddWithValue(":id_bairros", obj.id_bairros);
            stmt.Parameters.AddWithValue(":id_enderecos", obj.id_enderecos);
            stmt.Parameters.AddWithValue(":condition", obj.id);

            return stmt;
        }

        #endregion

        #region Metodo recuperar Todos registros
        public List<Clientes> recuperarTodos()
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

        #region Metodo recupera por nome/documento/id
        public List<Clientes> recuperarPorNome(string nome)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(GET_ALL_POR_NOME, conn);
                stmt.Parameters.AddWithValue("condition", String.Format("%{0}%", nome));
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

        public List<Clientes> recuperarPorDocumento(string documento)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(GET_ALL_POR_DOCUMENTO, conn);
                stmt.Parameters.AddWithValue("condition", documento);
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

        
        /*  nome, tipo_de_documento, documento, ie, isento_icms, email_principal, 
            email_secundario, numero, complemento, cep, observacoes, telefone_fixo, 
            telefone_celular, inativo, limite_de_credito, id_cidades, id_bairros, 
            id_enderecos */

        public Clientes recuperarPorId(long id)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;

            Clientes cc = null;

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(GET_ALL_POR_ID, conn);
                stmt.Parameters.AddWithValue("condition", id);

                dr = stmt.ExecuteReader();

                if (dr.Read())
                {
                    long _id = Convert.ToInt64(dr[0]);
                    string _nome = dr[1].ToString();
                    string _tipo_de_documento = dr[2].ToString();
                    string _documento = dr[3].ToString();
                    string _ie = dr[4].ToString();
                    bool _isento_icms = Convert.ToBoolean(dr[5]);
                    string _email_principal = dr[6].ToString();
                    string _email_secundario = dr[7].ToString();
                    string _numero = dr[8].ToString();
                    string _complemento = dr[9].ToString();
                    string _cep = dr[10].ToString();
                    string _observacoes = dr[11].ToString();
                    string _telefone_fixo = dr[12].ToString();
                    string _telefone_celular = dr[13].ToString();
                    bool _inativo = Convert.ToBoolean(dr[14]);
                    decimal _limite_de_credito = Convert.ToDecimal(dr[15]);
                    long _id_cidades = Convert.ToInt64(dr[16]);
                    long _id_bairros = Convert.ToInt64(dr[17]);
                    long _id_enderecos = Convert.ToInt64(dr[18]); 

                    cc = new Clientes()
                    {
                        id = _id,
                        nome = _nome,
                        tipo_de_documento = _tipo_de_documento,
                        documento = _documento,
                        ie = _ie,
                        isento_ICMS = _isento_icms,
                        email_principal = _email_principal,
                        email_secundario = _email_secundario,
                        numero = _numero,
                        complemento = _complemento,
                        cep = _cep,
                        observacoes = _observacoes,
                        telefone_fixo =_telefone_fixo,
                        telefone_celular = _telefone_celular,
                        inativo = _inativo,
                        limite_de_credito = _limite_de_credito,
                        id_cidades = _id_cidades,
                        id_bairros = _id_bairros,
                        id_enderecos = _id_enderecos
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

        #region count
        public int count(string value, string coluna)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;
            int _count = 0;

            string COUNT_STATEMENT = String.Format("SELECT count({0}) FROM clientes WHERE {0}=:condition", coluna);

            try
            {
                conn = GerenteDeConexoes.getConnection();
                stmt = new NpgsqlCommand(COUNT_STATEMENT, conn);
                stmt.Parameters.AddWithValue("condition", value);
                dr = stmt.ExecuteReader();

                if (dr.Read())
                {
                    _count = Convert.ToInt32(dr[0]);
                }
                return _count;
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
        private List<Clientes> converteParaLista(NpgsqlDataReader dr)
        {
            List<Clientes> lista = new List<Clientes>();
            while (dr.Read())
            {
                long _id = Convert.ToInt64(dr[0]);
                string _nome = dr[1].ToString();
                string _tipo_de_documento = dr[2].ToString();
                string _documento = dr[3].ToString();
                string _ie = dr[4].ToString();
                bool _isento_icms = Convert.ToBoolean(dr[5]);
                string _email_principal = dr[6].ToString();
                string _email_secundario = dr[7].ToString();
                string _numero = dr[8].ToString();
                string _complemento = dr[9].ToString();
                string _cep = dr[10].ToString();
                string _observacoes = dr[11].ToString();
                string _telefone_fixo = dr[12].ToString();
                string _telefone_celular = dr[13].ToString();
                bool _inativo = Convert.ToBoolean(dr[14]);
                decimal _limite_de_credito = Convert.ToDecimal(dr[15]);
                long _id_cidades = Convert.ToInt64(dr[16]);
                long _id_bairros = Convert.ToInt64(dr[17]);
                long _id_enderecos = Convert.ToInt64(dr[18]); 

                Clientes cliente = new Clientes() 
                {
                    id = _id,
                    nome = _nome,
                    tipo_de_documento = _tipo_de_documento,
                    documento = _documento,
                    ie = _ie,
                    isento_ICMS = _isento_icms,
                    email_principal = _email_principal,
                    email_secundario = _email_secundario,
                    numero = _numero,
                    complemento = _complemento,
                    cep = _cep,
                    observacoes = _observacoes,
                    telefone_fixo = _telefone_fixo,
                    telefone_celular = _telefone_celular,
                    inativo = _inativo,
                    limite_de_credito = _limite_de_credito,
                    id_cidades = _id_cidades,
                    id_bairros = _id_bairros,
                    id_enderecos = _id_enderecos 
                };

                lista.Add(cliente);
            }
            return lista;
        }
        #endregion
    }
}
