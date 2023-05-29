using HotelManagementApi.Models;

namespace HotelManagementApi.Repository
{
    public interface IControllerMethods
    {
        //methods for hotel controller
        //created methods for get,getbyid,getbyname,addhotel ,update hotel ,delete hotel
        IEnumerable<Hotel> GetAllHotels();
        Hotel GetHotelById(int id);
        Hotel AddHotel(Hotel hotel);
        Hotel UpdateHotel(Hotel hotel ,int id);
        Hotel DeleteHotel(int id);

       //methods for Room Controller
        IEnumerable<Room> GetAllRooms();
        Room GetRoomById(int id);
        Room AddRoom(Room room);
        Room UpdateRoom(Room room, int id);
        Room DeleteRoom(int id);

    }
}
