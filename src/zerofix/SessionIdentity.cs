namespace zerofix
{
    public record SessionIdentity
    {
        public string BeginString { get; init; }
        public string SenderCompID { get; init; }
        public string TargetCompID { get; init; }

        public SessionIdentity(string beginString, string senderCompId, string targetCompId) =>
            (BeginString, SenderCompID, TargetCompID) = (beginString, senderCompId, targetCompId);
    }
}