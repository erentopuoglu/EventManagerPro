using System.Collections.ObjectModel;
using EventManagerPro.Models;
using EventManagerPro.Services;

namespace EventManagerPro.ViewModels;

public class ParticipantsViewModel : BaseViewModel
{
	private readonly DataService _data;

	public ObservableCollection<Participant> Participants { get; } = new();

    private Participant? _selectedParticipant;
    public Participant? SelectedParticipant
    {
        get => _selectedParticipant;
        set
        {
            _selectedParticipant = value;
            OnPropertyChanged();

            UpdateCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();

      
            if (_selectedParticipant != null)
            {
                NewFullName = _selectedParticipant.FullName;
                NewEmail = _selectedParticipant.Email;

                OnPropertyChanged(nameof(NewFullName));
                OnPropertyChanged(nameof(NewEmail));
            }
        }
    }


    public string NewFullName { get; set; } = "";
	public string NewEmail { get; set; } = "";

	public RelayCommand AddCommand { get; }
	public RelayCommand UpdateCommand { get; }
	public RelayCommand DeleteCommand { get; }
	public RelayCommand ClearCommand { get; }
	public RelayCommand RefreshCommand { get; }

	public ParticipantsViewModel(DataService data)
	{
		_data = data;

		AddCommand = new RelayCommand(Add);
		UpdateCommand = new RelayCommand(Update, () => SelectedParticipant != null);
		DeleteCommand = new RelayCommand(Delete, () => SelectedParticipant != null);
		ClearCommand = new RelayCommand(Clear);
		RefreshCommand = new RelayCommand(Load);

		Load();
	}

	public void Load()
	{
		Participants.Clear();
		foreach (var p in _data.GetParticipants())
			Participants.Add(p);
	}

	private void Add()
	{
		if (string.IsNullOrWhiteSpace(NewFullName)) return;

		_data.AddParticipant(new Participant
		{
			FullName = NewFullName.Trim(),
			Email = NewEmail.Trim()
		});

		Clear();
		Load();
	}

    private void Update()
    {
        if (SelectedParticipant == null) return;

        SelectedParticipant.UpdateContact(NewFullName, NewEmail);

        _data.UpdateParticipant(SelectedParticipant);
        Load();
    }


    private void Delete()
	{
		if (SelectedParticipant == null) return;

		_data.DeleteParticipant(SelectedParticipant.Id);
		SelectedParticipant = null;
		Load();
	}

	private void Clear()
	{
		NewFullName = "";
		NewEmail = "";
		OnPropertyChanged(nameof(NewFullName));
		OnPropertyChanged(nameof(NewEmail));
	}
}
