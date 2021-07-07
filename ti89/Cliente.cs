using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ti89
{
    public class Cliente // declarar variaveis
    {
        public int id;
        public string mensagem;
        public bool achou = false;

        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        //=========================================================construtor
        public Cliente() { }
        public Cliente(int nCodigo, string nNome, string nEmail)
        {
            this.ID = nCodigo;
            this.Nome = nNome;
            this.Email = nEmail;
        }
        public Cliente(string nNome, string nEmail) // inserir
        {
            this.Nome = nNome;
            this.Email = nEmail;
        }
        //=========================================================metodo
        public void Inserir() // inserir
        {
            MySqlCommand comm = new MySqlCommand();
            comm.Connection = Banco.Abrir();
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sp_InserirUpdate";
            comm.Parameters.AddWithValue("_id", 0);
            comm.Parameters.AddWithValue("_nome", Nome);
            comm.Parameters.AddWithValue("_email", Email);
            comm.Parameters.AddWithValue("_acao", MySqlDbType.Int32).Value = 1;
            comm.ExecuteNonQuery();
            mensagem = "Registro realizado com sucesso!"; // retornar o auto_increment
            comm = new MySqlCommand("select max(id) from cadastro", Banco.Abrir());
            id = (int)comm.ExecuteScalar();
        }
        //=========================================================consultar
        public void Consultar(int _Id)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.Connection = Banco.Abrir();
            comm.CommandText = "select * from cadastro where id ="+_Id;
            MySqlDataReader dr = comm.ExecuteReader();

            if (!dr.HasRows)
            {
                mensagem = "Registro não encontrado!";
                achou = false;
                return;
            }
            else
            {
                achou = true;
                while (dr.Read())
                {
                    ID = dr.GetInt32(0);
                    Nome = dr.GetString(1);
                    Email = dr.GetString(2);
                }
            }
        }
        //=========================================================Alterar
        public void Alterar()
        {
            MySqlCommand comm = new MySqlCommand();
            comm.Connection = Banco.Abrir();
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sp_InserirUpdate";
            comm.Parameters.AddWithValue("_id", ID);
            comm.Parameters.AddWithValue("_nome", Nome);
            comm.Parameters.AddWithValue("_email", Email);
            comm.Parameters.AddWithValue("_acao", MySqlDbType.Int32).Value = 2;
            comm.ExecuteNonQuery();
            mensagem = "Registro alterado com sucesso!";
        }
        //=========================================================Excluir
        public void Deletar(int _ID)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.Connection = Banco.Abrir();
            comm.CommandText = "delete from cadastro where id = " + _ID;
            comm.ExecuteNonQuery();
            mensagem = "Registro deletado com sucesso!";
        }
    }
}
