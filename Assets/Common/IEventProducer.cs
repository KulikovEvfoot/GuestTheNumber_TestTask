namespace Common
{
    public interface IEventProducer<in T>
    {
        void Attach(T observer);
        void Detach(T observer);
    }
}