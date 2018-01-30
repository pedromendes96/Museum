﻿using System;
using System.Collections.Generic;

namespace Museum
{
    public class Permanent : Events
    {
        public Permanent()
        {
        }

        public Permanent(Dictionary<string, string> dictionary)
        {
        }

        private int id { get; set; }

        public int PermanentId
        {
            get => id;
            set => id = value;
        }

        public override string GetInformation()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            var table = "events";
            var keys = new[] {DescriptionProperty};
            var values = new[] {Process.Description};
            var insertEvent = SqlOperations.Instance.Insert(table, keys, values);
            DBConnection.Instance.Execute(insertEvent);

            table = "permanents";
            keys = new[] {"events_id"};
            values = new[] {Id.ToString()};
            var insertPermanent = SqlOperations.Instance.Insert(table, keys, values);
            DBConnection.Instance.Execute(insertPermanent);
        }

        public static IList<Dictionary<string, string>> GetPermanentsInEvents(string id)
        {
            var isPermanent = "SELECT title,name,description FROM permanents,events WHERE events.id=" +
                              id + " AND events.id=permanents.events_id";
            return DBConnection.Instance.Query(isPermanent);
        }

        public static IList<Dictionary<string, string>> GetAllPermanents()
        {
            var properties = new[] { "*" };
            var table = new[] { "permanents" };
            var permanentEvents = SqlOperations.Instance.Select(properties, table);
            return DBConnection.Instance.Query(permanentEvents);
        }

        public override void Update(string changeProperties, string changeValues, string table)
        {
            var properties = changeProperties.Split('-');
            var values = changeValues.Split('-');
            var error = false;
            for (var i = 0; i < properties.Length; i++)
                if (table == Event)
                {
                    if (properties[i] != DescriptionProperty) error = true;
                }
                else if (table == Permanent)
                {
//                    if ()
//                    {
                    error = true;
//                    }
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