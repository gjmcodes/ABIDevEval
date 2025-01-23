

namespace Ambev.DeveloperEvaluation.BUS
{
    public interface IBUS
    {
        Task SendEvent<T>(BusEvent<T> busEvent) where T : struct;
    }
    public class Bus : IBUS
    {
       

        public Task SendEvent<T>(BusEvent<T> busEvent) where T : struct
        {
            return Task.CompletedTask;
        }
    }
}
