using EventManagerPro.Data;
using EventManagerPro.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagerPro.Services;

public class DataService
{
    public List<Event> GetEvents()
    {
        using var db = new AppDbContext();
        return db.Events.OrderBy(e => e.Date).ToList();
    }

    public void AddEvent(Event e)
    {
        using var db = new AppDbContext();
        db.Events.Add(e);
        db.SaveChanges();
    }

    public void UpdateEvent(Event e)
    {
        using var db = new AppDbContext();
        db.Events.Update(e);
        db.SaveChanges();
    }

    public void DeleteEvent(Guid id)
    {
        using var db = new AppDbContext();
        var e = db.Events.FirstOrDefault(x => x.Id == id);
        if (e == null) return;

        var records = db.AttendanceRecords.Where(a => a.EventId == id).ToList();
        db.AttendanceRecords.RemoveRange(records);

        db.Events.Remove(e);
        db.SaveChanges();
    }

    public List<Participant> GetParticipants()
    {
        using var db = new AppDbContext();
        return db.Participants.OrderBy(p => p.FullName).ToList();
    }

    public void AddParticipant(Participant p)
    {
        using var db = new AppDbContext();
        db.Participants.Add(p);
        db.SaveChanges();
    }

    public void UpdateParticipant(Participant p)
    {
        using var db = new AppDbContext();
        db.Participants.Update(p);
        db.SaveChanges();
    }

    public void DeleteParticipant(Guid id)
    {
        using var db = new AppDbContext();
        var p = db.Participants.FirstOrDefault(x => x.Id == id);
        if (p == null) return;

        var records = db.AttendanceRecords.Where(a => a.ParticipantId == id).ToList();
        db.AttendanceRecords.RemoveRange(records);

        db.Participants.Remove(p);
        db.SaveChanges();
    }

    public Dictionary<Guid, bool> GetAttendanceMap(Guid eventId)
    {
        using var db = new AppDbContext();
        return db.AttendanceRecords
            .Where(a => a.EventId == eventId)
            .ToDictionary(a => a.ParticipantId, a => a.IsPresent);
    }

    public void SaveAttendance(Guid eventId, IEnumerable<(Guid participantId, bool isPresent)> items)
    {
        using var db = new AppDbContext();

        foreach (var (participantId, isPresent) in items)
        {
            var rec = db.AttendanceRecords
                .FirstOrDefault(a => a.EventId == eventId && a.ParticipantId == participantId);

            if (rec == null)
            {
                db.AttendanceRecords.Add(new AttendanceRecord
                {
                    EventId = eventId,
                    ParticipantId = participantId,
                    IsPresent = isPresent
                });
            }
            else
            {
                rec.IsPresent = isPresent;
                db.AttendanceRecords.Update(rec);
            }
        }

        db.SaveChanges();
    }
}
