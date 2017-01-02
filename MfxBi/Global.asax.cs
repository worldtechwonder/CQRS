using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BankDemo;
using Memento.Messaging.Postie;
using Memento.Persistence;
using Memento.Persistence.EmbeddedRavenDB;
using MfxBi.Cqrs.CommandStack.Sagas;
using MfxBi.Cqrs.QueryStack.Denormalizers;
using Microsoft.Practices.Unity;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace MfxBi
{
    public class MvcApplication : HttpApplication
    {
        public static IBus Bus { get; private set; }
        public static IRepository AggregateRepository { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Initialize the event store (RAVENDB)
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
            var documentStore = new EmbeddableDocumentStore
            {
                ConnectionStringName = "EventStore-MfxBi",
                UseEmbeddedHttpServer = true
            };
            documentStore.Configuration.Port = 8080;
            documentStore.Initialize();

            // Configure the FX
            var container = MementoStartup
                .UnityConfig<InMemoryBus, EmbeddedRavenDbEventStore, EmbeddableDocumentStore>(
                    documentStore);
            
            // Save global references to the FX core elements
            Bus = container.Resolve<IBus>();
            AggregateRepository = container.Resolve<IRepository>();

            // Add sagas and handlers to the bus
            Bus.RegisterSaga<ReservationSaga>();
            Bus.RegisterHandler<BookingDenormalizer>();
        }
    }
}
