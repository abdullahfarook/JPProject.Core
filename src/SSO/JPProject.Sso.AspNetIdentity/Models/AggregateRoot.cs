using System.Collections.Generic;

namespace JPProject.Sso.AspNetIdentity.Models
{
    public abstract class AggregateRoot 
    {
        private readonly List<Bk.Common.EventBus.Events.IDomainEvent> _domainEvents = new List<Bk.Common.EventBus.Events.IDomainEvent>();
        public IReadOnlyList<Bk.Common.EventBus.Events.IDomainEvent> DomainEvents => _domainEvents;

        protected void RaiseDomainEvent(Bk.Common.EventBus.Events.IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
