namespace F2Core
{
    public class Message
    {
        public string SenderPrefix { get; set; }
        public string SenderId { get; set; }
        public string RankColor { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }
        public Enumerations.MessageType Type { get; set; }
    }
}
