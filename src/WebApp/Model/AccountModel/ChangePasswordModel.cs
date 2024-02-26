using System.ComponentModel.DataAnnotations;

namespace WebApp.Model.AccountModel;

public class ChangePasswordModel
{

    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}