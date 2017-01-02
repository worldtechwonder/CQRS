using Memento.Persistence;

namespace MfxBi.Cqrs.Api
{
    public class DomainService
    {
        public DomainService(IEventStore eventStore, IRepository repository)
        {
            EventStore = eventStore;
            Repository = repository;
        }

        public IEventStore EventStore { get; private set; }
        public IRepository Repository { get; private set; }
    }
}