using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Notes.Commands;
using Notes.Models;

namespace Notes.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand AddNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }

        private ObservableCollection<Note?> _notes;
        private string _title;
        private string _content;

        public ObservableCollection<Note?> Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        private Note? _selectedNote;
        public Note? SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
            }
        }

        public MainViewModel()
        {
            Notes = new ObservableCollection<Note?>();

            AddNoteCommand = new RelayCommand<Note>(AddNote);
            DeleteNoteCommand = new RelayCommand<Note>(DeleteNote);
        }


        private void AddNote(Note note)
        {
            Notes.Add(new Note()
            {
                Title = Title,
                Content = Content,
            });

            Title = string.Empty;
            Content = string.Empty;
        }

        private void DeleteNote(Note note)
        {
            Notes.Remove(SelectedNote);
            SelectedNote = null;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
