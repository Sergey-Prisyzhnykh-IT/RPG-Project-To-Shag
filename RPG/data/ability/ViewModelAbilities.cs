﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using RPG.data.helpers;

namespace RPG.data.ability
{
    class ViewModelAbilities : INotifyPropertyChanged
    {
        private Ability _selectedAbility;

        public event PropertyChangedEventHandler PropertyChanged;

        private List<Ability> _abilities = AbilityHelper.GetAllAbilities();
        public Ability SelectedAbility
        {
            get { return _selectedAbility; }
            set
            {
                _selectedAbility = value;
                OnPropertyChanged("SelectedAbility");
            }
        }
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public ObservableCollection<Ability> Abilities { get; set; }

        public ViewModelAbilities()
        {
            Abilities = new ObservableCollection<Ability>();
            foreach (var ability in _abilities)
            {
                Abilities.Add(ability);
            }
        }
    }
}