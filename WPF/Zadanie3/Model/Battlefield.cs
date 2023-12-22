using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Zadanie3.Model
{
    public class Battlefield: INotifyPropertyChanged
    {
        private StateOfField _stateOfField;
        public StateOfField StateOfField 
        { 
            get => _stateOfField;
            set
            {
                _stateOfField = value;
                OnPropertyChanged();
            }
        
        }
        public Player Player { get; set; }
        public int Id { get; set; }
        //inform o zmianie ktora zaszla
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
