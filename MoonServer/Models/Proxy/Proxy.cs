namespace MoonServer.Models.Proxy
{
    // Going from entity to proxy
    // We can easily have the extra constructor to convert from the entity
    public abstract class Proxy
    {
        public abstract string GetDataType();
        public abstract string FriendlyString();
    }
}