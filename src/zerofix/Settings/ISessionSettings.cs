namespace zerofix
{
    public interface ISessionSettings
    {
        bool Has(string key);
        string GetString(string key);
        bool GetLong(string key);
    }
}