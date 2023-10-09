using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repositories;

public class BoxRepository
{
    private NpgsqlDataSource _dataSource;

    public BoxRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<BoxFeedQuery> GetBoxesForFeed()
    {
        string sql = $@"
        SELECT * FROM boxes
        ";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<BoxFeedQuery>(sql);
        }
    }

    public bool UpdateBox(int id, int volume, string name, string color, string description)
    {
        var sql = $@"UPDATE boxes SET volume = @volume, name = @name, color = @color, description = @description
                    WHERE id = @id";

        using var conn = _dataSource.OpenConnection();
        conn.Query<Box>(sql, new { volume, name, color, description, id });
        return true;
    }

    public object CreateBox(int volume, string name, string color, string description)
    {
        var sql =
            $@"INSERT INTO boxes (volume, name, color, description) VALUES (@volume, @name, @color, @description)";

        using var conn = _dataSource.OpenConnection();
        return conn.Query<Box>(sql, new { volume, name, color, description });
    }

    public bool DeleteBox(int id)
    {
        var sql = $@"DELETE FROM boxes WHERE id = @id";

        using var conn = _dataSource.OpenConnection();
        return conn.Execute(sql, new { id }) == 1;
    }
}