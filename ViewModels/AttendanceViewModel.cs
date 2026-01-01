using System;
using System.Linq;
using System.Collections.ObjectModel;
using EventManagerPro.Models;
using EventManagerPro.Services;

namespace EventManagerPro.ViewModels;

public class AttendanceViewModel : BaseViewModel
{
    private readonly DataService _data;
    private readonly ObservableCollection<Event> _events;
    private readonly ObservableCollection<Participant> _participants;

    public ObservableCollection<Event> Events => _events;

    public ObservableCollection<AttendanceItem> AttendanceItems { get; } = new();

    public ObservableCollection<string> PresentNames { get; } = new();
    public ObservableCollection<string> AbsentNames { get; } = new();

    private Event? _selectedEvent;
    public Event? SelectedEvent
    {
        get => _selectedEvent;
        set
        {
            _selectedEvent = value;
            OnPropertyChanged();

            Rebuild();
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public RelayCommand SaveCommand { get; }

    public AttendanceViewModel(
        DataService data,
        ObservableCollection<Event> events,
        ObservableCollection<Participant> participants)
    {
        _data = data;
        _events = events;
        _participants = participants;

        SaveCommand = new RelayCommand(Save, () => SelectedEvent != null);
    }

    private void RefreshSummaryLists()
    {
        PresentNames.Clear();
        AbsentNames.Clear();

        foreach (var item in AttendanceItems)
        {
            if (item.IsPresent) PresentNames.Add(item.FullName);
            else AbsentNames.Add(item.FullName);
        }
    }

    public void Rebuild()
    {
        AttendanceItems.Clear();
        if (SelectedEvent == null)
        {
            RefreshSummaryLists();
            return;
        }

        var map = _data.GetAttendanceMap(SelectedEvent.Id);

        foreach (var p in _participants)
        {
            map.TryGetValue(p.Id, out bool present);

            AttendanceItems.Add(new AttendanceItem(RefreshSummaryLists)
            {
                ParticipantId = p.Id,
                FullName = p.FullName,
                IsPresent = present
            });
        }

        RefreshSummaryLists();
    }

    private void Save()
    {
        if (SelectedEvent == null) return;

        _data.SaveAttendance(
            SelectedEvent.Id,
            AttendanceItems.Select(x => (x.ParticipantId, x.IsPresent))
        );

        Rebuild();
    }

    public class AttendanceItem : BaseViewModel
    {
        public Guid ParticipantId { get; set; }
        public string FullName { get; set; } = "";

        private bool _isPresent;
        private readonly Action _onChanged;

        public AttendanceItem(Action onChanged)
        {
            _onChanged = onChanged;
        }

        public bool IsPresent
        {
            get => _isPresent;
            set
            {
                _isPresent = value;
                OnPropertyChanged();
                _onChanged?.Invoke();
            }
        }
    }
}
