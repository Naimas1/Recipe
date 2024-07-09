using System.ComponentModel.DataAnnotations;

public class EditProfileViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string UserName { get; set; }
}
