﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Museum;

namespace MuseumForm
{
    public partial class EmployeesControl : UserControl
    {
        private IList<Person> employees = new List<Person>();

        public IList<Person> Employees
        {
            get => employees;
            set => employees = value;
        }

        public EmployeesControl()
        {
            InitializeComponent();
        }

        public void getEmployees()
        {
            var db = DBConnection.Instance;
            string select =
                "SELECT persons.name AS name,persons.password AS password,persons.mail AS mail,persons.phone AS phone, persons.id AS persons_id,employees.id AS employees_id,employees.salary,employees.lastUpdate,employees.id FROM employees,persons WHERE persons.id = employees.persons_id";
            Debug.WriteLine(select);
            var list = db.Query(select);
            Debug.WriteLine(list.Count);
            IFactory personFactory = FactoryCreator.Instance.CreateFactory("PersonFactory");

            foreach (var demployee in list)
            {
                var employee = personFactory.ImportData("Employee", demployee);
                Employees.Add((Employee)employee);
            }
        }


    }
}
