﻿using System;
using System.Collections.Generic;

namespace Museum
{
    public class Photography : ArtPiece
    {
        public static readonly string SizeProperty = "size";

        public Photography()
        {
        }

        public Photography(Dictionary<string, string> dictionary)
        {
        }

        private int id { get; set; }

        public int PhotographyId
        {
            get => id;
            set => id = value;
        }

        private double size { get; set; }

        public double Size
        {
            get => size;
            set => size = value;
        }

        public override void SetDimension(string size)
        {
            Size = double.Parse(size);
        }

        public override string GetInformation()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            var table = "items";
            var keys = new[] {NameProperty, DescriptionProperty,"exhibitors_id"};
            var values = new[] {Name, Description, Exhibitor.IdExhibitor.ToString()};
            var insertItems = SqlOperations.Instance.Insert(table, keys, values);
            Id = DBConnection.Instance.Execute(insertItems);

            table = "photographies";
            keys = new[] {SizeProperty, "items_id"};
            values = new[] {Size.ToString(), Id.ToString()};
            var insertPhotographies = SqlOperations.Instance.Insert(table, keys, values);
            DBConnection.Instance.Execute(insertPhotographies);
        }

        public override void Update(string changeProperties, string changeValues, string table)
        {
            var properties = changeProperties.Split('-');
            var values = changeValues.Split('-');
            var error = false;
            for (var i = 0; i < properties.Length; i++)
                if (table == Items)
                {
                    if (properties[i] != NameProperty && properties[i] != DescriptionProperty &&
                        properties[i] != RoomProperty) error = true;
                }
                else if (table == Photographies)
                {
                    if (properties[i] != SizeProperty) error = true;
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