namespace zerofix
{
    public interface IApplication
    {
        void OnCreate(Session session);
        void OnMessage(FixMessage msg);
    }
}