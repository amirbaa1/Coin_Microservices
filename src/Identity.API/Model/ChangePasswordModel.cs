namespace Identity.API.Model;

public class ChangePasswordModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string NewPassword { get; set; }
}