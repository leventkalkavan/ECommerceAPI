namespace ECommerceAPI.Application.DTOs.User;

public class UserDto : BaseDto
{
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
}