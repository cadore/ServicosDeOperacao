using Npgsql;
using Siscom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FuncionariosDAO
    {
        #region queries

        private static string GET_ALL_STATMENT = @"SELECT id, nome, senha, salario_fixo, comissao, vendas, administrador,
                                                    relatorios, acesso_inativo, inativo FROM funcionarios;";

        private static string GET_ALL_POR_ID = @"SELECT id, nome, senha, salario_fixo, comissao, vendas, administrador,
                                                    relatorios, acesso_inativo, inativo FROM funcionarios WHERE id=:condition;";

        private static string INSERT_STATEMENT = @"INSERT INTO funcionarios(id, nome, senha, salario_fixo, comissao, vendas, administrador, relatorios, acesso_inativo, inativo)
                                                    VALUES (:id, :nome, :senha, :salario_fixo, :comissao, :vendas, :administrador, :relatorios, :acesso_inativo, :inativo)
                                                    RETURNING id;";

        private static string UPDATE_STATEMENT = @"UPDATE funcionarios SET id=:id, nome=:nome, senha=:senha, salario_fixo=:salario_fixo, comissao=:comissao, vendas=:vendas, 
                                                    administrador=:administrador, relatorios=:relatorios, acesso_inativo=:acesso_inativo, inativo=:inativo WHERE id=:condition RETURNING id;";

        #endregion

        #region Metodo recuperar Todos registros
        public List<Funcionarios> recuperarTodos()
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
        //id, nome, senha, salario_fixo, comissao, vendas, administrador, relatorios, acesso_inativo, inativo
        public Funcionarios recuperarPorId(long id)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand stmt = null;
            NpgsqlDataReader dr = null;

            Funcionarios cc = null;

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
                    string _senha = dr[2].ToString();
                    decimal _salario_fixo = Convert.ToDecimal(dr[3]);
                    decimal _comissao = Convert.ToDecimal(dr[4]);
                    bool _vendas = Convert.ToBoolean(dr[5]);
                    bool _administrador = Convert.ToBoolean(dr[6]);
                    bool _relatorios = Convert.ToBoolean(dr[7]);
                    bool _acesso_inativo = Convert.ToBoolean(dr[8]);
                    bool _inativo = Convert.ToBoolean(dr[9]);

                    cc = new Funcionarios()
                    {
                        id = _id,
                        nome = _nome,
                        senha = _senha,
                        salario_fixo = _salario_fixo,
                        comissao = _comissao,
                        vendas = _vendas,
                        administrador = _administrador,
                        relatorios = _relatorios,
                        acesso_inativo = _acesso_inativo,
                        inativo = _inativo
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
        //id, nome, senha, salario_fixo, comissao, vendas, administrador, relatorios, acesso_inativo, inativo
        private List<Funcionarios> converteParaLista(NpgsqlDataReader dr)
        {
            List<Funcionarios> lista = new List<Funcionarios>();
            while (dr.Read())
            {
                long _id = Convert.ToInt64(dr[0]);
                string _nome = dr[1].ToString();
                string _senha = dr[2].ToString();
                decimal _salario_fixo = Convert.ToDecimal(dr[3]);
                decimal _comissao = Convert.ToDecimal(dr[4]);
                bool _vendas = Convert.ToBoolean(dr[5]);
                bool _administrador = Convert.ToBoolean(dr[6]);
                bool _relatorios = Convert.ToBoolean(dr[7]);
                bool _acesso_inativo = Convert.ToBoolean(dr[8]);
                bool _inativo = Convert.ToBoolean(dr[9]);

                Funcionarios f = new Funcionarios()
                {
                    id = _id,
                    nome = _nome,
                    senha = _senha,
                    salario_fixo = _salario_fixo,
                    comissao = _comissao,
                    vendas = _vendas,
                    administrador = _administrador,
                    relatorios = _relatorios,
                    acesso_inativo = _acesso_inativo,
                    inativo = _inativo
                };
                lista.Add(f);
            }
            return lista;
        }
        #endregion

        #region Metodo Salvar/Atualizar
        public int salvar(Funcionarios obj)
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
        /*id, nome, senha, salario_fixo, comissao, vendas, administrador, relatorios, acesso_inativo, inativo*/
        private NpgsqlCommand getStatementInsert(NpgsqlConnection conn, Funcionarios obj)
        {
            NpgsqlCommand stmt = new NpgsqlCommand(INSERT_STATEMENT, conn);
            stmt.Parameters.AddWithValue(":nome", obj.nome);
            stmt.Parameters.AddWithValue(":senha", obj.senha);
            stmt.Parameters.AddWithValue(":salario_fixo", obj.salario_fixo);
            stmt.Parameters.AddWithValue(":comissao", obj.comissao);
            stmt.Parameters.AddWithValue(":vendas", obj.vendas);
            stmt.Parameters.AddWithValue(":administrador", obj.administrador);
            stmt.Parameters.AddWithValue(":relatorios", obj.relatorios);
            stmt.Parameters.AddWithValue(":acesso_inativo", obj.acesso_inativo);
            stmt.Parameters.AddWithValue(":inativo", obj.inativo);

            return stmt;
        }

        /*id, nome, senha, salario_fixo, comissao, vendas, administrador, relatorios, acesso_inativo, inativo*/
        private NpgsqlCommand getStatementUpdate(NpgsqlConnection conn, Funcionarios obj)
        {
            NpgsqlCommand stmt = new NpgsqlCommand(UPDATE_STATEMENT, conn);
            stmt.Parameters.AddWithValue(":nome", obj.nome);
            stmt.Parameters.AddWithValue(":senha", obj.senha);
            stmt.Parameters.AddWithValue(":salario_fixo", obj.salario_fixo);
            stmt.Parameters.AddWithValue(":comissao", obj.comissao);
            stmt.Parameters.AddWithValue(":vendas", obj.vendas);
            stmt.Parameters.AddWithValue(":administrador", obj.administrador);
            stmt.Parameters.AddWithValue(":relatorios", obj.relatorios);
            stmt.Parameters.AddWithValue(":acesso_inativo", obj.acesso_inativo);
            stmt.Parameters.AddWithValue(":inativo", obj.inativo);
            stmt.Parameters.AddWithValue(":condition", obj.id);

            return stmt;
        }

        #endregion
    }
}
