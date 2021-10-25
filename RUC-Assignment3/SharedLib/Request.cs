namespace SharedLib
{
    public class Request
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public long Date { get; set; }
        public object? Body { get; set; }
    }
}

