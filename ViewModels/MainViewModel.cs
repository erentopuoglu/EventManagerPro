using EventManagerPro.Services;

namespace EventManagerPro.ViewModels;

public class MainViewModel
{
    public DataService Data { get; } = new();

    public EventsViewModel EventsVM { get; }
    public ParticipantsViewModel ParticipantsVM { get; }
    public AttendanceViewModel AttendanceVM { get; }

    public MainViewModel()
    {
        EventsVM = new EventsViewModel(Data);
        ParticipantsVM = new ParticipantsViewModel(Data);

       
        AttendanceVM = new AttendanceViewModel(Data, EventsVM.Events, ParticipantsVM.Participants);


        EventsVM.RefreshCommand.Execute(null);
        ParticipantsVM.RefreshCommand.Execute(null);
    }
}
