namespace zerofix
{
    internal interface ITransport
    {
        bool SendAsync(byte[] raw);
    }
}