namespace WishMe.Service.Database
{
    public interface IDbConfig
    {
        string Url { get; }

        string DatabaseName { get; }
    }
}
