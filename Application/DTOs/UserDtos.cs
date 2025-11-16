namespace Application.DTOs;

public class UserLoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class UserGetDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
}
