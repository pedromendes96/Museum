﻿using System;
using System.Collections.Generic;

namespace Museum
{
    public class Exhibitor : Person
    {
        public static readonly string TypeProperty = "type";

        public Exhibitor()
        {
        }

        public Exhibitor(Dictionary<string, string> dictionary)
        {
            var dictionaryAdapter = new DictionaryAdapter(dictionary);
            Id = int.Parse(dictionaryAdapter.GetValue("persons_id"));
            Name = dictionaryAdapter.GetValue("name");
            Password = dictionaryAdapter.GetValue("password");
            Phone = int.Parse(dictionaryAdapter.GetValue("phone"));
            Mail = dictionaryAdapter.GetValue("mail");
            IdExhibitor = int.Parse(dictionaryAdapter.GetValue("exhibitors_id"));
            Type = dictionaryAdapter.GetValue("type");
        }

        private int idExhibitor { get; set; }

        public int IdExhibitor
        {
            get => idExhibitor;
            set => idExhibitor = value;
        }

        private string type { get; set; }

        public string Type
        {
            get => type;
            set => type = value;
        }

        private Process process { get; set; }

        public Process Process
        {
            get => process;
            set => process = value;
        }

        public List<int> IdItems { get; set; } = new List<int>();

//        public void AddItem(string type)
//        {
//            var artFactory = FactoryCreator.Instance.CreateFactory(FactoryCreator.ArtPieceFactory);
//            ArtPiece artPiece;
//            if (type == ArtpieceFactory.painting)
//            {
//                artPiece = (Painting) artFactory.Create(type);
//            }
//            else if (type == ArtpieceFactory.photography)
//            {
//                artPiece = (Photography) artFactory.Create(type);
//            }
//            else if (type == ArtpieceFactory.sculpture)
//            {
//                artPiece = (Sculpture) artFactory.Create(type);
//            }
//            else
//            {
//                Console.WriteLine("Some error occour");
//                return;
//            }
//            artPiece.Name = "Arte";
//            artPiece.Description = "Arte";
//            artPiece.Exhibitor = this;
//            artPiece.Size = 12.2;
//            artPiece.Save();
//        }

        public override int RoleId()
        {
            return IdExhibitor;
        }

        public override void GetData(Dictionary<string, string> values)
        {
            var dictionaryAdapter = new DictionaryAdapter(values);
            Name = dictionaryAdapter.GetValue(NameProperty);
            Password = dictionaryAdapter.GetValue(PasswordProperty);
            Phone = int.Parse(dictionaryAdapter.GetValue(PhoneProperty));
            Mail = dictionaryAdapter.GetValue(MailProperty);
            Type = dictionaryAdapter.GetValue(TypeProperty);
        }

        public override bool SubmitData()
        {
            var table = "persons";
            var keys = new[] {PasswordProperty, NameProperty, PhoneProperty, MailProperty};
            var values = new[] {Password, Name, Phone.ToString(), Mail};
            var insertPersons = SqlOperations.Instance.Insert(table, keys, values);
            Console.WriteLine(insertPersons);
            Id = DBConnection.Instance.Execute(insertPersons);

            table = "exhibitors";
            keys = new[] {TypeProperty, "persons_id"};
            values = new[] {Type, Id.ToString()};
            var insertExhibitors = SqlOperations.Instance.Insert(table, keys, values);
            Console.WriteLine(insertExhibitors);
            DBConnection.Instance.Execute(insertExhibitors);
            return true;
        }

        public override void Update(string changeProperties, string changeValues, string table)
        {
            var properties = changeProperties.Split('-');
            var values = changeValues.Split('-');
            var error = false;
            for (var i = 0; i < properties.Length; i++)
                if (table == Itself)
                {
                    if (properties[i] != PasswordProperty && properties[i] != NameProperty &&
                        properties[i] != PhoneProperty && properties[i] != MailProperty) error = true;
                }
                else if (table == Exhibitor)
                {
                    if (properties[i] != TypeProperty) error = true;
                }
                else
                {
                    error = true;
                }

            if (error)
                Console.WriteLine("Nao e possivel efetuar essa operacao!");
            else
                UpdateSequence(table, properties, values);
        }
    }
}