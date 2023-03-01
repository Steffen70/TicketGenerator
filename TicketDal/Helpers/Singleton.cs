namespace TicketDal.Helpers
{
    public abstract class Singleton<T> where T : class, new()
    {
        protected static T? Instance;

        public static T I => Instance ??= new T();
    }
}
