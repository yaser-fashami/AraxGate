using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using AraxGate.Core.Domain.Entities;

namespace AraxGate.WebUI.Models.ViewModels;

public class UserViewModel
{
    public User User { get; set; }
	public IFormFile? Avatar { get; set; }
    [ValidateNever]
    public byte? avatar_remove { get; set; }
    public string[] Role { get; set; }
}
