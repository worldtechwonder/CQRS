using System;
using Memento.Messaging;
using Memento.Messaging.Postie;
using Memento.Messaging.Postie.Unity;
using Memento.Persistence;
using Microsoft.Practices.Unity;

namespace MfxBi
{
    public class MementoStartup
    {
        public static UnityContainer UnityConfig(Type busType, Type eventStoreType, Type eventStoreDocumentType, object documentStore)
        {
            var container = new UnityContainer();
            container.RegisterType<ITypeResolver, UnityTypeResolver>(new InjectionConstructor(container));
            container.RegisterType(typeof (IBus), busType);
            container.RegisterType(typeof(IEventDispatcher), busType);

            //container.RegisterType<IBus, InMemoryBus>();
            //container.RegisterType<IEventDispatcher, InMemoryBus>();
            container.RegisterType<IRepository, Repository>(new InjectionConstructor(eventStoreType));

            //container.RegisterType<IRepository, Repository>(
            //    new InjectionConstructor(typeof(EmbeddedRavenDbEventStore)));

            container.RegisterType(typeof (IEventStore), eventStoreType,
                new InjectionConstructor(eventStoreDocumentType, typeof(IEventDispatcher)));
            //container.RegisterType<IEventStore, EmbeddedRavenDbEventStore>(
            //    new InjectionConstructor(typeof(EmbeddableDocumentStore), typeof(IEventDispatcher)));

            container.RegisterInstance(documentStore);

            return container;
        }

        public static UnityContainer UnityConfig<TBus, TEventStore, TEventStoreDocument>(TEventStoreDocument documentStore)
        {
            var container = new UnityContainer();
            container.RegisterType<ITypeResolver, UnityTypeResolver>(new InjectionConstructor(container));
            container.RegisterType(typeof(IBus), typeof(TBus));
            container.RegisterType(typeof(IEventDispatcher), typeof(TBus));

            //container.RegisterType<IBus, InMemoryBus>();
            //container.RegisterType<IEventDispatcher, InMemoryBus>();
            container.RegisterType<IRepository, Repository>(new InjectionConstructor(typeof(TEventStore)));

            //container.RegisterType<IRepository, Repository>(
            //    new InjectionConstructor(typeof(EmbeddedRavenDbEventStore)));

            container.RegisterType(typeof(IEventStore), typeof(TEventStore),
                new InjectionConstructor(typeof(TEventStoreDocument), typeof(IEventDispatcher)));
            //container.RegisterType<IEventStore, EmbeddedRavenDbEventStore>(
            //    new InjectionConstructor(typeof(EmbeddableDocumentStore), typeof(IEventDispatcher)));

            container.RegisterInstance(documentStore);

            return container;
        }
    }
}