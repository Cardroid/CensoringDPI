namespace GoodByeDPIDotNet.Interface
{
    public interface IGoodByeDPIOptions
    {
        string Path { get; }
        bool IsAdmin { get; }
        string GetArgument();
    }
}
