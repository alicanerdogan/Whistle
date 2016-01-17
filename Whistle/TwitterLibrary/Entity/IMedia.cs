namespace TwitterLibrary.Entity
{
    public interface IMedia
    {
        long Id { get; }
        Media.TwitterMediaType MediaType { get; }
    }
}