using Dapper;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repositories;

public class AccountRepository : RepositoryBase
{
    private readonly NpgsqlDataSource _dataSource;

    public AccountRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<AccountSafeQuery> GetAllAccounts()
    {
        //TODO: remove GetAllItems
        //return GetAllItems<AccountQuery>("account"); dev only
        const string sql = "SELECT id, email, name, rank FROM account";
        
        using var conn = _dataSource.OpenConnection();
        return conn.Query<AccountSafeQuery>(sql);
    }

    public int CreateAccount(string accountName, string accountEmail, string accountPassword, int accountRank)
    {
        var parameters = new
        {
            name = accountName,
            email = accountEmail,
            password = accountPassword,
            rank = accountRank
        };

        return CreateItem<int>("account", parameters);//TODO: check if it works, fix if not
    }

    public bool UpdateAccount(AccountQuery account)
    {
        return UpdateEntity("account", account, "id");
    }

    public bool DeleteAccount(int accountId)
    {
        return DeleteItem("account", accountId);
    }

    public AccountQuery? GetAccountByName(string name)
    {
        return GetSingleItemByParameters<AccountQuery>("account", new { name });
    }
}