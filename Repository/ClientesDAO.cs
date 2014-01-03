using Npgsql;
using Siscom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClientesDAO
    {
        #region queries
        private static string INSERT_STATEMENT = @"INSERT INTO clientes(
                                                        nome, tipo_de_documento, documento, ie, isento_ICMS, email_principal, 
                                                        email_secundario, numero, complemento, cep, observacoes, telefone_fixo, 
                                                        telefone_celular, inativo, limite_de_credito, id_cidades, id_bairros, 
                                                        id_enderecos)
                                                        VALUES (:nome, :tipo_de_documento, :documento, :ie, :isento_ICMS, :email_principal, 
                                                        :email_secundario, :numero, :complemento, :cep, :observacoes, :telefone_fixo, 
                                                        :telefone_celular, :inativo, :limite_de_credito, :id_cidades, :id_bairros, 
                                                        :id_enderecos) RETURNING id;";

        private static string UPDATE_STATEMENT = @"UPDATE clientes
                                                        SET nome=:nome, tipo_de_documento=:tipo_de_documento, documento=:documento, ie=:ie, isento_ICMS=:isento_ICMS, 
                                                        email_principal=:email_principal, email_secundario=:email_secundario, numero=:numero, complemento=:complemento, 
                                                        cep=:cep, observacoes=:observacoes, telefone_fixo=:telefone_fixo, telefone_celular=:telefone_celular, inativo=:inativo, 
                                                        limite_de_credito=:limite_de_credito, id_cidades=:id_cidades, id_bairros=:id_bairros, id_enderecos=:id_enderecos
                                                        WHERE id=:condition  RETURNING id;";

        private static string GET_ALL_STATMENT = @"SELECT id, nome, tipo_de_documento, documento, ie, isento_ICMS, email_principal, 
                                                    email_secundario, numero, complemento, cep, observacoes, telefone_fixo, 
                                                    telefone_celular, inativo, limite_de_credito, id_cidades, id_bairros, 
                                                    id_enderecos
                                                    FROM clientes;";

        private static string GET_ALL_POR_ID = @"SELECT id, nome, tipo_de_documento, documento, ie, isento_ICMS, email_principal, 
                                                    email_secundario, numero, complemento, cep, observacoes, telefone_fixo, 
                                                    telefone_celular, inativo, limite_de_credito, id_cidades, id_bairros, 
                                                    id_enderecos
                                                    FROM clientes WHERE id=:condition;";

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
                catch (Exception l) { }
                throw new Exception(String.Format("Erro ao salvar/atualizar Clfor{0} EXCEPT: {1}\n\nINNER EXCEPT: {2}", Environment.NewLine, ex.Message, ex.InnerException));
            }
            finally
            {
                GerenteDeConexoes.closeAll(conn);
            }
        }

        #endregion

        #region Preparação de Statements
        /*INSERT INTO clientes(
                nome, tipo_de_documento, documento, ie, isento_ICMS, email_principal, 
                email_secundario, numero, complemento, cep, observacoes, telefone_fixo, 
                telefone_celular, inativo, limite_de_credito, id_cidades, id_bairros, 
                id_enderecos)
                VALUES (:nome, :tipo_de_documento, :documento, :ie, :isento_ICMS, :email_principal, 
                :email_secundario, :numero, :complemento, :cep, :observacoes, :telefone_fixo, 
                :telefone_celular, :inativo, :limite_de_credito, :id_cidades, :id_bairros, 
                :id_enderecos) RETURNING id*/
        private NpgsqlCommand getStatementInsert(NpgsqlConnection conn, Clientes obj)
        {
            NpgsqlCommand stmt = new NpgsqlCommand(INSERT_STATEMENT, conn);
            stmt.Parameters.AddWithValue(":nome", obj.nome);
            stmt.Parameters.AddWithValue(":tipo_de_documento", obj.tipo_de_documento);
            stmt.Parameters.AddWithValue(":documento", obj.documento);
            stmt.Parameters.AddWithValue(":ie", obj.ie);
            stmt.Parameters.AddWithValue(":isento_ICMS", obj.isento_ICMS);
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
                SET nome=:nome, tipo_de_documento=:tipo_de_documento, documento=:documento, ie=:ie, isento_ICMS=:isento_ICMS, 
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
            stmt.Parameters.AddWithValue(":isento_ICMS", obj.isento_ICMS);
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
    }
}
