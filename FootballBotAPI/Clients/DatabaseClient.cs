using Npgsql;

namespace FootballBotAPI.Clients
{
    public class DatabaseClient
    {
        NpgsqlConnection connection; 
        public DatabaseClient() 
        { 
            connection = new NpgsqlConnection(Constants.Connection);
        }


        public async Task InsertFavouriteTeamAsync(long userId, string teamName)
        {
            var answer = await GetFavouriteTeamAsync(userId);
            if (answer == null)
            {
                var sql = $"INSERT INTO public.\"FavouriteTeams\" (\"UserId\", \"TeamName\") VALUES (@UserId, @TeamName)";

                await using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("UserId", userId);
                    command.Parameters.AddWithValue("TeamName", teamName);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
                return;
            }
            
            if (!(await GetFavouriteTeamAsync(userId)).Equals(teamName))
            {
                await ChangeFavouriteTeamAsync(userId, teamName);
                return;
            }

            return;
        }


        public async Task DeleteFavouriteTeamAsync(long userId)
        {
            var sql = $"delete from public.\"FavouriteTeams\" where \"UserId\" = @UserId";

            await using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("UserId", userId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }

        }


        public async Task ChangeFavouriteTeamAsync(long userId, string teamName)
        {
            var sql = $"UPDATE public.\"FavouriteTeams\" SET \"TeamName\" = @TeamName WHERE \"UserId\" = @UserId";

            await using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("UserId", userId);
                command.Parameters.AddWithValue("TeamName", teamName);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }


        public async Task<string> GetFavouriteTeamAsync(long userId)
        {
            var sql = $"SELECT \"TeamName\" FROM public.\"FavouriteTeams\" WHERE \"UserId\" = @UserId ";

            await using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("UserId", userId);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        string answer = reader.GetString(0);
                        await connection.CloseAsync();
                        return answer;
                    }
                }

                await connection.CloseAsync();
                return null;
            }
            
        }
    }
}
