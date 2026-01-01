using System;

namespace EventManagerPro.Models;

public class Participant
{
    public Guid Id { get; set; } = Guid.NewGuid();

    private string _fullName = "";
    public string FullName
    {
        get => _fullName;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                _fullName = value.Trim();
        }
    }

    private string _email = "";
    public string Email
    {
        get => _email;
        set
        {
            
            _email = (value ?? "").Trim();
        }
    }

    public void UpdateContact(string fullName, string email)
    {
        FullName = fullName;
        Email = email;
    }
}
