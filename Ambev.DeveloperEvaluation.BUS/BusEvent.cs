namespace Ambev.DeveloperEvaluation.BUS
{
    public class BusEvent<T> where T : struct
    {
        public string eventType;
        public DateTime eventTime;
        public T value;
    }
}
