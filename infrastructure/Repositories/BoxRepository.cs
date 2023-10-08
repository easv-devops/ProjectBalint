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

    public Box UpdateBox(int id, int volume, string name, string color, string description)
    {
        throw new NotImplementedException();
    }

    public object CreateBox(int volume, string name, string color, string description)
    {
        throw new NotImplementedException();
    }

    public void DeleteBox(int id)
    {
        throw new NotImplementedException();
    }
}

