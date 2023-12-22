using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using Zadanie3.Command;
using Zadanie3.Model;

namespace Zadanie3.ViewModel
{
    /// <summary>
    /// ViewModel reprezentujący logikę gry i interakcję widoku z modelem danych.
    /// </summary>
    public class GameViewModel : INotifyPropertyChanged
    {
        // Fields
        private IsReady _isPlayer1Ready;
        private IsReady _isPlayer2Ready;

        private Player _playerTurn;
        private Player _playerWin;

        // Properties
        public ObservableCollection<Battlefield> PolaGracza1 { get; }
        public ObservableCollection<Battlefield> PolaGracza2 { get; }
        public ObservableCollection<Battlefield> DoStrzelania1 { get; }
        public ObservableCollection<Battlefield> DoStrzelania2 { get; }

        public IsReady IsPlayer1Ready
        {
            get { return _isPlayer1Ready; }
            set
            {
                _isPlayer1Ready = value;
                OnPropertyChanged();
            }
        }

        public IsReady IsPlayer2Ready
        {
            get { return _isPlayer2Ready; }
            set
            {
                _isPlayer2Ready = value;
                OnPropertyChanged();
            }
        }

        public Player PlayerTurn
        {
            get => _playerTurn;
            set
            {
                _playerTurn = value;
                OnPropertyChanged();
            }
        }

        public Player PlayerWin
        {
            get => _playerWin;
            set
            {
                _playerWin = value;
                OnPropertyChanged();
            }
        }

        // Events
        public event PropertyChangedEventHandler? PropertyChanged;

        // Commands
        public RelayCommand<Battlefield> AddShipCommand { get; }
        public RelayCommand<Battlefield> ShootShipCommand { get; }
        public RelayCommand<object> SubmitReadyPlayer1Command { get; }
        public RelayCommand<object> SubmitReadyPlayer2Command { get; }
        public RelayCommand<object> RestartGameCommand { get; }

        public GameViewModel()
        {
            PolaGracza1 = new ObservableCollection<Battlefield>();
            PolaGracza2 = new ObservableCollection<Battlefield>();
            DoStrzelania1 = new ObservableCollection<Battlefield>();
            DoStrzelania2 = new ObservableCollection<Battlefield>();

            AddShipCommand = new RelayCommand<Battlefield>(AddShip);
            ShootShipCommand = new RelayCommand<Battlefield>(ShootShip);
            SubmitReadyPlayer1Command = new RelayCommand<object>(SubmitReadyPlayer1);
            SubmitReadyPlayer2Command = new RelayCommand<object>(SubmitReadyPlayer2);
            RestartGameCommand = new RelayCommand<object>(RestartGame);

            InitBattlefield();
        }

        private void RestartGame(object obj)
        {
            InitBattlefield();
        }

        private void SubmitReadyPlayer1(object obj)
        {
            IsPlayer1Ready = IsReady.Ready;
        }

        private void SubmitReadyPlayer2(object obj)
        {
            IsPlayer2Ready = IsReady.Ready;
        }


        /// <summary>
        /// Metoda obsługująca strzał w statek na planszy.
        /// </summary>
        /// <param name="battlefield">Pole, na które kliknął gracz.</param>
        private void ShootShip(Battlefield battlefield)
        {
            if (IsPlayer1Ready == IsReady.NotReady)
            {
                return;
            }

            if (IsPlayer2Ready == IsReady.NotReady)
            {
                return;
            }
            
            if (PlayerWin != Player.None)
            {
                return;
            }

            ValidateShoot(battlefield);
        }

        private void ValidateShoot(Battlefield battlefield)
        {
            switch (PlayerTurn)
            {
                case Player.Player1 when battlefield.Player == Player.Player1:
                case Player.Player2 when battlefield.Player == Player.Player2:
                    return;
            }

            var found = battlefield.Player == Player.Player1
                ? PolaGracza1.FirstOrDefault(x => x.Id == battlefield.Id)
                : PolaGracza2.FirstOrDefault(x => x.Id == battlefield.Id);
            if (found == null)
            {
                return;
            }

            var doStrzelania = battlefield.Player == Player.Player1
                ? DoStrzelania1.FirstOrDefault(x => x.Id == found.Id)
                : DoStrzelania2.FirstOrDefault(x => x.Id == found.Id);
            if (doStrzelania == null)
            {
                return;
            }

            if (found.StateOfField != StateOfField.Occupied)
            {
                found.StateOfField = StateOfField.Miss;
                doStrzelania.StateOfField = StateOfField.Miss;
                PlayerTurn = Player.Player1 == PlayerTurn ? Player.Player2 : Player.Player1;
            }
            else
            {
                found.StateOfField = StateOfField.Hit;
                doStrzelania.StateOfField = StateOfField.Hit;
                CzyNieWygral(found);
            }
        }

        private void CzyNieWygral(Battlefield found)
        {
            var czyGraczWygral = found.Player == Player.Player1
                ? PolaGracza1.All(x => x.StateOfField != StateOfField.Occupied)
                : PolaGracza2.All(x => x.StateOfField != StateOfField.Occupied);
            if (czyGraczWygral)
            {
                PlayerWin = found.Player == Player.Player1 ? Player.Player2 : Player.Player1;
            }
        }

        /// <summary>
        /// Metoda obsługująca dodawanie statku na planszę.
        /// </summary>
        /// <param name="battlefield">Pole, na które kliknął gracz.</param>
        private void AddShip(Battlefield battlefield)
        {
            if (battlefield.Player == Player.Player1)
            {
                if (IsPlayer1Ready == IsReady.Ready)
                {
                    return;
                }

                var found = PolaGracza1.FirstOrDefault(x => x.Id == battlefield.Id);
                if (found != null)
                {
                    found.StateOfField = StateOfField.Occupied;
                }
            }
            else
            {
                if (IsPlayer2Ready == IsReady.Ready)
                {
                    return;
                }

                var found = PolaGracza2.FirstOrDefault(x => x.Id == battlefield.Id);
                if (found != null)
                {
                    found.StateOfField = StateOfField.Occupied;
                }
            }
        }

        /// <summary>
        /// Inicjalizuje pola planszy oraz ustawia stan gotowości graczy.
        /// </summary>
        private void InitBattlefield()
        {
            const int numberOfButtons = 100;
            PolaGracza1.Clear();
            PolaGracza2.Clear();
            DoStrzelania1.Clear();
            DoStrzelania2.Clear();

            IsPlayer1Ready = IsReady.NotReady;
            IsPlayer2Ready = IsReady.NotReady;
            PlayerWin = Player.None;

            PlayerTurn = Player.Player1;

            //tworzenie pol na planszy dla graczy
            for (int i = 0; i < numberOfButtons; i++)
            {
                PolaGracza1.Add(new Battlefield { Id = i, Player = Player.Player1 });
                //Tworzenie pola gracz1
            }

            for (int i = 0; i < numberOfButtons; i++)
            {
                PolaGracza2.Add(new Battlefield { Id = i, Player = Player.Player2 });
            }

            //Tworzenie pol do strzelania
            for (int i = 0; i < numberOfButtons; i++)
            {
                DoStrzelania1.Add(
                    new Battlefield { Id = i, Player = Player.Player1, StateOfField = StateOfField.Empty });
            }

            for (int i = 0; i < numberOfButtons; i++)
            {
                DoStrzelania2.Add(
                    new Battlefield { Id = i, Player = Player.Player2, StateOfField = StateOfField.Empty });
            }
        }

        /// <summary>
        /// Wywołuje zdarzenie PropertyChanged, informujące o zmianie właściwości.
        /// </summary>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}