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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = @"D:\Курсяк\RPG\RPG\bin\Debug\netcoreapp3.1\Hero.json";
        static Hero hero = new Hero("Test Hero");
        Enemy enemy = new Enemy()
        {
            Name = "Skelet",
            Level = 1,
            Experience = 20,
            Health = 15,
            Mana = 10
        };
        IAbility temp2 = hero;

        void ShowStaticHero() 
        {
            TxtEditor.Items.Add("");
            TxtEditor.Items.Add("Static Hero");
            TxtEditor.Items.Add("Name " + hero.Name);
            TxtEditor.Items.Add("Health " + hero.Health);
            TxtEditor.Items.Add("Mana " + hero.Mana);
            TxtEditor.Items.Add("Level: " + hero.Level);
            TxtEditor.Items.Add("Experience: " + hero.Experience);
            TxtEditor.Items.Add("Strength: " + hero.Strength);
            TxtEditor.Items.Add("Agility " + hero.Agility);
            TxtEditor.Items.Add("Intellect: " + hero.Intellect);
            TxtEditor.Items.Add("Vitality: " + hero.Vitality);
            TxtEditor.Items.Add("");
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

       
        public MainWindow()
        {
            hero.Ability.Add(attackSpell);
            hero.Ability.Add(buffSpell);
            InitializeComponent();

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            TxtEditor.Items.Add("\t\t\t\t\tSTART GAME");
        }

        private void StaticHero_Click(object sender, RoutedEventArgs e)
        {
            ShowStaticHero();
        }

        private void StaticEnemy_Click(object sender, RoutedEventArgs e)
        {
            TxtEditor.Items.Add("");
            TxtEditor.Items.Add("Static Enemy");
            //var enemy = Editor.EnemyDeserialize(@"D:\Курсяк\Project\Project\bin\Debug\netcoreapp3.1\Skelet.json").Result;
            //TxtEditor.Items.Add(File.ReadAllText(path));
            TxtEditor.Items.Add("Name: " + enemy.Name);
            TxtEditor.Items.Add("Health: " + enemy.Health);
            TxtEditor.Items.Add("Level: " + enemy.Level);
            TxtEditor.Items.Add("Experience: " + enemy.Experience);
            TxtEditor.Items.Add("Mana: " + enemy.Mana);
            TxtEditor.Items.Add("");
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            TxtEditor.Items.Add("");
            enemy.Health -= hero.Damage();
            TxtEditor.Items.Add("Hero deals damage: " + hero.Damage());
            TxtEditor.Items.Add("Enemy Health: " + enemy.Health);
            TxtEditor.Items.Add("");
            hero.Health -= enemy.Damage();
            TxtEditor.Items.Add("Enemy deals damage: " + enemy.Damage());
            TxtEditor.Items.Add("Health Hero: " + hero.Health);
            TxtEditor.Items.Add("");
        }

        private void Buff_Click(object sender, RoutedEventArgs e)
        {
            TxtEditor.Items.Add("");
            hero.Health += temp2.Buff(hero.Ability.Find(s => s == buffSpell));
            TxtEditor.Items.Add("Hero restores health: " + buffSpell.BuffValue);
            TxtEditor.Items.Add("Health Hero: " + hero.Health);
            TxtEditor.Items.Add("Mana Hero: " + hero.Mana);
            TxtEditor.Items.Add("");
        }

        private void Debuff_Click(object sender, RoutedEventArgs e)
        {
            TxtEditor.Items.Add("");
            enemy.Health -= temp2.AttackAbility(hero.Ability.Find(s => s == attackSpell));
            TxtEditor.Items.Add("Hero deals Spell damage: " + attackSpell.Damage);
            TxtEditor.Items.Add("Enemy Health: " + enemy.Health);
            TxtEditor.Items.Add("Mana Hero: " + hero.Mana);
            TxtEditor.Items.Add("");
            hero.Health -= enemy.Damage();
            TxtEditor.Items.Add("Enemy deals damage: " + enemy.Damage());
            TxtEditor.Items.Add("Health Hero: " + hero.Health);
            TxtEditor.Items.Add("");

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
                TxtEditor.Items.Add("File downloaded");
                hero = Editor.HeroDeserialize(openFileDialog.FileName).Result;
                //= File.ReadAllText(openFileDialog.FileName);
            }
            else 
            {
                TxtEditor.Items.Add("File not uploaded");
            } 
                 
        }
    }
}