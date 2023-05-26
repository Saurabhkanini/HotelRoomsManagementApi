using HotelManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementApi.Repository
{
    public class IntrefaceMethodsImplementaion : IControllerMethods
    {
        private readonly HotelDbContext hotelDbContext;
        public IntrefaceMethodsImplementaion(HotelDbContext htdb)
        {
            this.hotelDbContext = htdb;

        }
        public IEnumerable<Hotel> GetAllHotels()
        {
            var allHotels = hotelDbContext.Hotels
                     .Include(x => x.Rooms)
                         .ThenInclude(r => r.Reservations)
                     .Include(x => x.Rooms)
                         .ThenInclude(r => r.Amenitiess)
                     .ToList();
            return allHotels;
        }
        public Hotel GetHotelById(int id)
        {
            var hotel = hotelDbContext.Hotels.Find(id);
            return hotel;
        }
        public Hotel GetHotelByName(string name)
        {
            var hotel = hotelDbContext.Hotels.FirstOrDefault(x => x.HotelName == name);
            return hotel;
        }
        public Hotel AddHotel(Hotel hotel)
        {
            hotelDbContext.Hotels.Add(hotel);
            hotelDbContext.SaveChanges();
            return hotel;
        }
        public Hotel UpdateHotel(Hotel hotel, int id)
        {
            var hotell = hotelDbContext.Hotels.Find(id);
            hotell.Address = hotel.Address;
            hotell.City = hotel.City;
            hotel.HotelCapacity = hotel.HotelCapacity;
            hotell.Country = hotel.Country;
            hotell.HotelName = hotel.HotelName;
            hotelDbContext.Hotels.Update(hotell);
            hotelDbContext.SaveChanges();
            return hotell;
        }
        public Hotel DeleteHotel(int id)
        {
            var hotel = hotelDbContext.Hotels.Find(id);
            hotelDbContext.Hotels.Remove(hotel);
            hotelDbContext.SaveChanges();
            return hotel;
        }

        public Room AddRoom(Room room)
        {
            hotelDbContext.Rooms.Add(room);
            hotelDbContext.SaveChanges();
            return room;
        }



        public Room DeleteRoom(int id)
        {
            var room = hotelDbContext.Rooms.Find(id);
            hotelDbContext.Rooms.Remove(room);
            hotelDbContext.SaveChanges();
            return room;
        }



        public IEnumerable<Room>  GetAllRooms()
        {
            var allRooms=hotelDbContext.Rooms.ToList();
            return allRooms;
        }
        public Room GetRoomById(int id)
        {
            var room=hotelDbContext.Rooms.Find(id);
            return room;
        }
        public Room UpdateRoom(Room room, int id)
        {
            var rooms = hotelDbContext.Rooms.Find(id);
            rooms.Price = room.Price;   
            rooms.Availability = room.Availability;
            rooms.RoomType = room.RoomType; 
            rooms.HotelId = room.HotelId;
            hotelDbContext.Rooms.Update(rooms);
            hotelDbContext.SaveChanges();
            return rooms;
        }
    }
}
