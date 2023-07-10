using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ConexaoBD
{
    class Program
    {
        static void Main(string[] args)
        {
            string origemConnectionString = "string de conexão do banco de origem";
            string destinoConnectionString = "string de conexão do banco de destino";

            using (SqlConnection origemConnection = new SqlConnection(origemConnectionString))
            using (SqlConnection destinoConnection = new SqlConnection(destinoConnectionString))
            {
                try
                {
                    origemConnection.Open();
                    destinoConnection.Open();

                    string queryOrigem = "query de origem";

                    using (SqlCommand commandOrigem = new SqlCommand(queryOrigem, origemConnection))

                    using (SqlDataReader readerOrigem = commandOrigem.ExecuteReader())
                    {
                        while (readerOrigem.Read())
                        {
                            //colunas
                            int codigo = (int)readerOrigem["codigo"];
                            string filtroPadrao = (string)readerOrigem["filtroPadrao"];

                            string queryDestino = $"query de destino";

                            using (SqlCommand commandDestino = new SqlCommand(queryDestino, destinoConnection))
                            {
                                commandDestino.Parameters.AddWithValue("@filtroPadrao", filtroPadrao);
                                commandDestino.Parameters.AddWithValue("@codigo", codigo);

                                commandDestino.ExecuteNonQuery();
                            }
                        }
                    }

                    Console.WriteLine("Atualização concluída com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar ou atualizar o banco de dados: " + ex.Message);
                }
            }

            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
