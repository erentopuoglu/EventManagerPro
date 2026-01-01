using System;
using System.Collections.ObjectModel;
using EventManagerPro.Models;
using EventManagerPro.Services;

namespace EventManagerPro.ViewModels;

public class EventsViewModel : BaseViewModel
{
    private readonly DataService _data;

    public ObservableCollection<Event> Events { get; } = new();

    
    private Event? _selectedEvent;
    public Event? SelectedEvent
    {
        get => _selectedEvent;
        set
        {
            _selectedEvent = value;
            OnPropertyChanged();

            UpdateCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();

            if (_selectedEvent != null)
            {
                NewTitle = _selectedEvent.Title;
                NewLocation = _selectedEvent.Location;
                NewDate = _selectedEvent.Date;

                OnPropertyChanged(nameof(NewTitle));
                OnPropertyChanged(nameof(NewLocation));
                OnPropertyChanged(nameof(NewDate));
            }
        }
    }


    public string NewTitle { get; set; } = "";
    public string NewLocation { get; set; } = "";
    public DateTime NewDate { get; set; } = DateTime.Today;

    public RelayCommand AddCommand { get; }
    public RelayCommand UpdateCommand { get; }
    public RelayCommand DeleteCommand { get; }
    public RelayCommand ClearCommand { get; }
    public RelayCommand RefreshCommand { get; }

    public EventsViewModel(DataService data)
    {
        _data = data;

        AddCommand = new RelayCommand(Add);
        UpdateCommand = new RelayCommand(Update, () => SelectedEvent != null);
        DeleteCommand = new RelayCommand(Delete, () => SelectedEvent != null);
        ClearCommand = new RelayCommand(Clear);
        RefreshCommand = new RelayCommand(Load);

        Load();
    }

    public void Load()
    {
        Events.Clear();
        foreach (var e in _data.GetEvents())
            Events.Add(e);
    }

    private void Add()
    {
        if (string.IsNullOrWhiteSpace(NewTitle)) return;

        _data.AddEvent(new Event
        {
            Title = NewTitle.Trim(),
            Location = NewLocation.Trim(),
            Date = NewDate
        });

        Clear();
        Load();
    }

    private void Update()
    {
        if (SelectedEvent == null) return;

        SelectedEvent.UpdateDetails(NewTitle, NewDate, NewLocation);

        _data.UpdateEvent(SelectedEvent);
        Load();
    }


    private void Delete()
    {
        if (SelectedEvent == null) return;

        _data.DeleteEvent(SelectedEvent.Id);
        SelectedEvent = null;
        Load();
    }

    private void Clear()
    {
        NewTitle = "";
        NewLocation = "";
        NewDate = DateTime.Today;
        OnPropertyChanged(nameof(NewTitle));
        OnPropertyChanged(nameof(NewLocation));
        OnPropertyChanged(nameof(NewDate));
    }
}
