namespace infrastructure;

public class Utilities
{
    //private static readonly Uri Uri = new Uri(Environment.GetEnvironmentVariable("pgconn")!);

    public static readonly string
        ProperlyFormattedConnectionString =
            "Server=cornelius.db.elephantsql.com;Database=muauksua;User Id=muauksua;Password=kWAW0X3ToR7w_FcEK4vyihdS8V643d4T;Port=5432;Pooling=true;MaxPoolSize=3;";
    // string.Format(
    // "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=true;MaxPoolSize=3;",
    // Uri.Host,
    // Uri.AbsolutePath.Trim('/'),
    // Uri.UserInfo.Split(':')[0],
    // Uri.UserInfo.Split(':')[1],
    // Uri.Port > 0 ? Uri.Port : 5432);
}
