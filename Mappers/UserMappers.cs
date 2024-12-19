using DigiGall.Dtos.User;
using DigiGall.Models;

namespace DigiGall.Mappers
{
    public static class UserMappers
    {
        // Mapping from CreateUserDto to User
        public static User ToUserFromCreateDto(this CreateUserDto createUserDto)
        {
            return new User
            {
                Email = createUserDto.Email,
                NamaLengkap = createUserDto.NamaLengkap,
                Password = createUserDto.Password,
                Asrama = createUserDto.Asrama,  
                SaldoDigigall = 0,  
                Role = "User",  
                Transaksis = new List<Transaksi>(),
                PemberianQuests = new List<PemberianQuest>()
            };
        }

        // Mapping from User to UserDto
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                Email = userModel.Email,
                NamaLengkap = userModel.NamaLengkap,
                Password = userModel.Password,
                Asrama = userModel.Asrama,
                SaldoDigigall = userModel.SaldoDigigall,
                Role = userModel.Role,
                Transaksis = userModel.Transaksis,
                PemberianQuests = userModel.PemberianQuests
            };
        }
    }
}
