﻿using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Windows;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace RPG
{
    /// <summary>
    /// Логика взаимодействия для Location1.xaml
    /// </summary>
    public partial class Location1 : Window
    {
        static Hero hero = new Hero("Test Hero");
        TaskCompletionSource<bool> End = new TaskCompletionSource<bool>();
        Enemy enemy = new Enemy()
        {
            Name = "Skelet",
            Level = 1,
            Experience = 20,
            Health = 15,
            Mana = 15
        };
        IAbility temp2 = hero;

        void ShowStaticHero()
        {
            ShowStatistics.Items.Add("");
            ShowStatistics.Items.Add("Static Hero");
            ShowStatistics.Items.Add("Name " + hero.Name);
            ShowStatistics.Items.Add("Health " + hero.Health);
            ShowStatistics.Items.Add("Mana " + hero.Mana);
            ShowStatistics.Items.Add("Level: " + hero.Level);
            ShowStatistics.Items.Add("Experience: " + hero.Experience);
            ShowStatistics.Items.Add("Strength: " + hero.Strength);
            ShowStatistics.Items.Add("Agility " + hero.Agility);
            ShowStatistics.Items.Add("Intellect: " + hero.Intellect);
            ShowStatistics.Items.Add("Vitality: " + hero.Vitality);
            ShowStatistics.Items.Add("");
        }

        AttackSpell attackSpell = new AttackSpell()
        {
            Name = "test",
            Damage = 10,
            ManaCoast = 5
        };

        Buff buffSpell = new Buff()
        {
            Name = "test Buff",
            BuffValue = 5,
            ManaCoast = 5
        };


        public Location1()
        {
            hero.Ability.Add(attackSpell);
            hero.Ability.Add(buffSpell);
            InitializeComponent();
            EndBattle();
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            ShowStatistics.Items.Add("");
            enemy.Health -= hero.Damage();
            ShowStatistics.Items.Add("Hero deals damage: " + hero.Damage());
            ShowStatistics.Items.Add("Enemy Health: " + enemy.Health);
            ShowStatistics.Items.Add("");
            if (!CheckHP())
            {
                DamageEnemy();
            }
        }

        private void DamageEnemy()
        {
            hero.Health -= enemy.Damage();
            ShowStatistics.Items.Add("Enemy deals damage: " + enemy.Damage());
            ShowStatistics.Items.Add("Health Hero: " + hero.Health);
            ShowStatistics.Items.Add("");
            CheckHP();
        }
        private bool CheckHP()
        {
            if (hero.Health <= 0 || enemy.Health <= 0)
            {
                End.SetResult(true);
                return true;
            }
            return false;
        }
        private async void EndBattle()
        {
            await End.Task;
            ShowStatistics.Items.Add("Finish");
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.json)|*.json|(*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
                Editor.HeroEdit(hero, saveFileDialog.FileName);
        }
        private void Loading_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.json)|*.json|(*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ShowStatistics.Items.Add("File downloaded");
                hero = Editor.HeroDeserialize(openFileDialog.FileName);
            }
            else
            {
                ShowStatistics.Items.Add("File not uploaded");
            }
        }

        private void Buff_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var ability = (Ability)sender;
        }
    }
}