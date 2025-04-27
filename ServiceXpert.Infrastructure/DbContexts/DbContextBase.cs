namespace ServiceXpert.Infrastructure.DbContexts;
internal abstract class DbContextBase
{
    protected static string ToVarcharColumn(int length) => $"VARCHAR({length})";
}
