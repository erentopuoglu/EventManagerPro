using System;

namespace EventManagerPro.Models;

public class Event
{
    public Guid Id { get; set; } = Guid.NewGuid();

    private string _title = "";
    public string Title
    {
        get => _title;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                _title = value.Trim();
        }
    }

    public DateTime Date { get; set; } = DateTime.Today;

    private string _location = "";
    public string Location
    {
        get => _location;
        set
        {
            _location = (value ?? "").Trim();
        }
    }

    
    public void UpdateDetails(string title, DateTime date, string location)
    {
        Title = title;
        Date = date;
        Location = location;
    }
}
