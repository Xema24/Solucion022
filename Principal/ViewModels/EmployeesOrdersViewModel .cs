using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.Datos;
namespace Principal.ViewModels
{
    public class EmployeesOrdersViewModel
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Nullable<System.DateTime> birthDate { get; set; }
        public string Photo { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}