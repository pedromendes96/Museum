﻿using System.Management.Instrumentation;

namespace Museum
{
    class SqlOperations
    {
        public static SqlOperations instance = new SqlOperations();
        
        private SqlOperations()
        {
            
        }

        public static SqlOperations Instance => instance;
        
        public string Update(int? id, string table, string[] properties, string[] values)
        {
            if (properties.Length == values.Length)
            {
                var update = "UPDATE " + table + " SET ";
                for (var i = 0; i < properties.Length; i++)
                {
                    if (i == properties.Length - 1)
                    {
                        update += properties[i] + " = '" + values[i] + "'";
                    }
                    else
                    {
                        update += properties[i] + "= '" + values[i] + "', ";
                    }
                }
                update += " WHERE id=" + id;
                return update;
            }
            else
            {
                return null;
            }
        }

        public string Insert(string table, string[] keys, string[] values)
        {
            if (keys.Length == values.Length)
            {
                var insert = "INSERT INTO " + table + " (";
                for (var i = 0; i < keys.Length; i++)
                {
                    if (i == keys.Length - 1)
                    {
                        insert += keys[i] + ") VALUES (";
                    }
                    else
                    {
                        insert += keys[i] + ",";
                    }

                }
                for (var j = 0; j < values.Length; j++)
                {
                    if (j == values.Length - 1)
                    {
                        insert += "'" + values[j] + "')";
                    }
                    else
                    {
                        insert += "'" + values[j] + "',";
                    }
                }
                return insert;
            }
            else
            {
                return null;
            }
        }

        public string Delete(string table, string[] keys, string[] values)
        {
            if (keys.Length == values.Length)
            {
                string delete = "DELETE FROM " + table + " WHERE ";
                for (var i = 0; i < keys.Length; i++)
                {
                    if (i == values.Length - 1)
                    {
                        delete += keys[i] + " = '" + values[i] + "'";
                    }

                    else
                    {
                        delete += keys[i] + " = '" + values[i] + "' AND ";
                    }
                }
                return delete;
            }
            else
            {
                return null;
            }
        }

        public string Select(string[] selvals, string[] tables)
        {
            string select = "SELECT ";
            for (var i = 0; i < selvals.Length; i++)
            {
                if (i == selvals.Length - 1)
                {
                    select += selvals[i] + " from ";
                }
                else
                {
                    select += selvals[i] + ",";
                }
            }
            for (var i = 0; i < tables.Length; i++)
            {
                if (i == tables.Length - 1)
                {
                    select += tables[i];
                }
                else
                {
                    select += tables[i] + ",";
                }
            }
            return select;
        }

        public string Select(string[] selvals, string[] tables, string[] keys, string[] values)
        {
            if (keys.Length == values.Length)
            {
                string select = "SELECT ";
                for (var i = 0; i < selvals.Length; i++)
                {
                    if (i == selvals.Length - 1)
                    {
                        select += selvals[i] + " from ";
                    }
                    else
                    {
                        select += selvals[i] + ",";
                    }
                }
                for (var i = 0; i < tables.Length; i++)
                {
                    if (i == tables.Length - 1)
                    {
                        select += tables[i] + " WHERE ";
                    }
                    else
                    {
                        select += tables[i] + ",";
                    }
                }

                for (var i = 0; i < keys.Length; i++)
                {
                    if (i == keys.Length - 1)
                    {
                        select += keys[i] + " = '" + values[i] + "'";
                    }
                    else
                    {
                        select += keys[i] + " = '" + values[i] + "' AND ";
                    }

                }
                return select;
            }
            return null;

        }
    }
}
