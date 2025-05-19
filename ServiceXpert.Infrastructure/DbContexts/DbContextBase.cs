namespace ServiceXpert.Infrastructure.DbContexts;
internal abstract class DbContextBase
{
    public static string VarcharMax { get => "VARCHAR(MAX)"; }

    protected static string ToVarcharColumn(int length) => $"VARCHAR({length})";
}
