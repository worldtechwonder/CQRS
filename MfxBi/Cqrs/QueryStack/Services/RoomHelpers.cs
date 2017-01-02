using System;
using System.Collections.Generic;
using System.Linq;
using MfxBi.Cqrs.QueryStack.ReadModel;
using MfxBi.Cqrs.QueryStack.ReadModel.Model;

namespace MfxBi.Cqrs.QueryStack.Services
{
    public class RoomHelpers
    {
        public static void Add(string name)
        {
            var room = new Room() {Name = name};
            using (var db = new MfxbiDatabase())
            {
                db.Rooms.Add(room);
                db.SaveChanges();
            }
        }

        public static IList<Room> All()
        {
            using (var db = new ProjectionManager())
            {
                var rooms = (from r in db.Rooms select r).ToList();
                return rooms;
            }
        }

        public static Room FindById(int id)
        {
            using (var db = new ProjectionManager())
            {
                var room = (from r in db.Rooms where r.Id == id select r).FirstOrDefault();
                return room;
            }
        }

        public static RoomConfiguration FindCurrentConfigForRoom(int id, DateTime day)
        {
            using (var db = new ProjectionManager())
            {
                var all = (from rc in db.RoomConfigurations
                    where rc.RooomId == id && rc.Enabled
                    select rc).ToList();

                // Filter to see the first that applies 
                // (MIGHT BE A MORE SOPHISTICATED ALGORITHM)
                foreach (var config in all)
                {
                    if (config.IsInEffect(day))
                        return config;
                }
                return RoomConfiguration.Default();
            }
        }

        public static IList<ScheduleItem> GetGridForRoom(int roomId, DateTime day)
        {
            var roomConfig = FindCurrentConfigForRoom(roomId, day);

            using (var db = new ProjectionManager())
            {
                var bookings = (from b in db.BookingSummaries
                    where b.Day == day
                    select b).ToList();

                // Map to grid
                var hourStart = DateTime.Today
                    .AddHours(roomConfig.StartingHour)
                    .AddMinutes(roomConfig.StartingMins);
                var end = DateTime.Today.AddHours(roomConfig.EndingHour);
                var hour = hourStart;
                BookingSummary previousBooking = null;
                var previousBookingSlotsLeft = 0;

                var slots = new List<ScheduleItem>();
                while (hour < end)
                {
                    var currentHour = hour;
                    var slot = new ScheduleItem(hour.Hour, hour.Minute, null);
                    if (previousBooking == null)
                    {
                        var existing = (from b in bookings
                                        where b.Day == day && b.StartHour == currentHour.Hour && b.StartMins == currentHour.Minute
                                        select b).FirstOrDefault();
                        if (existing != null)
                        {
                            slot.CurrentBooking = existing;
                            slot.IsAvailable = false;
                            slot.Text = existing.DisplayName;
                            if (existing.NumberOfSlots > 1)
                                previousBooking = existing;
                            previousBookingSlotsLeft = existing.NumberOfSlots - 1;
                        }
                    }
                    else
                    {
                        slot.CurrentBooking = previousBooking;
                        slot.IsAvailable = false;
                        slot.Text = "";
                        slot.IsFirstSlotOfBooking = false;
                        previousBookingSlotsLeft--;
                        if (previousBookingSlotsLeft == 0)
                            previousBooking = null;
                    }

                    slots.Add(slot);
                    hour = hour.AddMinutes(roomConfig.SlotLengthInMins);
                }
                return slots;
            }
        }
    }
}