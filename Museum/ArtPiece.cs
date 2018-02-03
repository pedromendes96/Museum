﻿using System;
using System.Collections.Generic;

namespace Museum
{
    public abstract class ArtPiece : IDecorator
    {
        public static readonly string NameProperty = "name";
        public static readonly string DescriptionProperty = "description";
        public static readonly string TitleProperty = "title";
        public static readonly string RoomProperty = "room";

        public static readonly string Items = "items";
        public static readonly string Paitings = "paitings";
        public static readonly string Photographies = "photographies";
        public static readonly string Sculptures = "sculptures";

        private int id { get; set; }

        public int Id
        {
            get => id;
            set => id = value;
        }

        private string name { get; set; }

        public string Name
        {
            get => name;
            set => name = value;
        }

        private string description { get; set; }

        public string Description
        {
            get => description;
            set => description = value;
        }

        private Exhibitor exhibitor { get; set; }

        public Exhibitor Exhibitor
        {
            get => exhibitor;
            set => exhibitor = value;
        }

        private IDecorator element { get; set; }

        public IDecorator Element
        {
            get => element;
            set => element = value;
        }

        public abstract string GetInformation();

        public void SetElement(IDecorator newElement)
        {
            Element = newElement;
        }

        public IDecorator GetElement()
        {
            return Element;
        }

        public void SetParameters(string name, string description, string size, Exhibitor exhibitor)
        {
            Name = name;
            Description = description;
            SetDimension(size);
            Exhibitor = exhibitor;
        }

        public static IList<Dictionary<string,string>> GetAllItemsByProcess(string idProcess)
        {
            var query = "SELECT * FROM items_has_processes WHERE processes_id=" + idProcess;
            return DbConnection.Instance.Query(query);
        }

        public void AssociateWithProcess(int processId)
        {
            var artPieceProcess = "INSERT INTO items_has_processes (items_id,processes_id) VALUES (" + Id + "," +
                                  processId + ")";
            DbConnection.Instance.Execute(artPieceProcess);
        }

        public abstract void SetDimension(string size);

        public abstract void Save();

        public abstract void Update(string properties, string values, string table);

        public void UpdateSequence(string table, string[] properties, string[] values)
        {
            var update = SqlOperations.Instance.Update(Id, table, properties, values);
            DbConnection.Instance.Execute(update);
        }
    }
}