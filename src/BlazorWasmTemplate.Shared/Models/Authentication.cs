using System.ComponentModel.DataAnnotations;

namespace BlazorWasmTemplate.Shared.Models;

public class Authentication
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
}

