using System;
using Memento.Messaging.Postie;
using Memento.Persistence;
using MfxBi.Cqrs.QueryStack.ReadModel.Model;
using MfxBi.Cqrs.QueryStack.Services;
using MfxBi.Models;

namespace MfxBi.Application
{
    public class HomeService
    {
        public IRepository Repository { get; private set; }
        public IBus Bus { get; private set; }

        public HomeViewModel GetHomeViewModel(DateTime when)
        {
            var room = RoomHelpers.FindById(1);
            if (room == null)
            {
                return new HomeViewModel { Room = new Room {Name = "NO ROOM FOUND"} }; 
            }
            var items = RoomHelpers.GetGridForRoom(1, when);
            var model = new HomeViewModel {Room = room, DayOfSchedule = when, Items = items };
            return model;
        }
    }
}