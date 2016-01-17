namespace Hoarder
{
    public interface IHoardPolicy
    {
        bool IsValid(IHoardItem item);
    }
}