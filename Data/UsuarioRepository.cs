using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using apiPhotos.Models;
using Microsoft.Extensions.Configuration;

namespace apiPhotos.Data
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PhotosConnection");
        }

        public async Task<Usuario> GetUser(int IdUser)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = sql.CreateCommand())
                    {
                        cmd.CommandText = $@"
                        SELECT
                            id_user,
                            nome_user,
                            email_user
                        FROM
                            dbo.TB_USUARIO
                        WHERE
                            id_user = @IdUser
                        ";

                        cmd.Parameters.AddWithValue("@IdUser", IdUser);

                        cmd.CommandType = System.Data.CommandType.Text;
                        Usuario user = null;
                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                user = new Usuario()
                                {
                                    Id_User = (int)reader["id_user"],
                                    Nome = (string)reader["nome_user"],
                                    Email = (string)reader["email_user"]
                                };
                            }
                        }
                        return user;
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}