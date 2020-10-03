namespace zerofix
{
    public interface ISessionSettings
    {
        bool Has(string key);
        string Get(string key);
    }
}