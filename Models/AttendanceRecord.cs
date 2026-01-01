using System;

namespace EventManagerPro.Models;

public class AttendanceRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid EventId { get; set; }
    public Guid ParticipantId { get; set; }

    private bool _isPresent;
    public bool IsPresent
    {
        get => _isPresent;
        set
        {
            _isPresent = value;
        }
    }

   
    public void MarkAttendance(bool isPresent)
    {
        IsPresent = isPresent;
    }
}
