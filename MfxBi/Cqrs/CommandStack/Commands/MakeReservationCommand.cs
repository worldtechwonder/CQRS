using System;
using Memento;

namespace MfxBi.Cqrs.CommandStack.Commands
{
    public class MakeReservationCommand : Command
    {
        public MakeReservationCommand(int roomid, string name, DateTime day, int hour, int mins, int slots)
        {
            RoomId = roomid;
            FullName = name;
            When = day;
            NumberOfSlots = slots;
            Hour = hour;
            Mins = mins;
        }

        public int RoomId { get; private set; }
        public string FullName { get; private set; }
        public DateTime When { get; private set; }
        public int NumberOfSlots { get; private set; }
        public int Hour { get; private set; }
        public int Mins { get; private set; }

        // SignalR extension
        public string SignalRConnectionId { get; set; }
    }
}