namespace SharedLib
{
    public class Options
    {
        public enum ResponseStatus : int
        {
            Ok = 1,
            Created = 2,
            Updated = 3,
            BadRequest = 4,
            NotFound = 5,
            Error = 6
        }
    }
}
