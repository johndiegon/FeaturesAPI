namespace CrossCutting.Security
{
    public interface ISettings
    {
        string TokenSecret { get; set; }
    }
}
