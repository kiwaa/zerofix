using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zerofix
{
    public static class Tags
    {
        public const int BeginString = 8;
        public const int BodyLength = 9;
        public const int CheckSum = 10;
        public const int MsgSeqNum = 34;
        public const int MsgType = 35;
        public const int SenderCompID = 49;
        public const int SendingTime = 52;
        public const int TargetCompID = 56;
        public const int TestReqID = 112;
    }
    public static class SessionSettings
    {
        public const string BEGINSTRING = "BeginString";
        public const string SENDERCOMPID = "SenderCompID";
        public const string TARGETCOMPID = "TargetCompID";
        public const string CONNECTION_TYPE = "ConnectionType";
        public const string CONNECTION_TYPE_INITIATOR = "initiator";
        public const string CONNECTION_TYPE_ACCEPTOR = "acceptor";

        public const string SOCKET_ACCEPT_HOST = "SocketAcceptHost";
        public const string SOCKET_ACCEPT_PORT = "SocketAcceptPort";
        public const string SOCKET_CONNECT_HOST = "SocketConnectHost";
        public const string SOCKET_CONNECT_PORT = "SocketConnectPort";


    }
    public static class FixSettings
    {
        public const string DateTimeFormat = "yyyyMMdd-HH:mm:ss.fff";
    }
}
