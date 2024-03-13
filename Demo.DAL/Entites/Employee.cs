using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(10)]
        public string Name { get; set; }
        public int?Age { get; set; }
        public String Address {  get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set;}
        public string PhoneNumber { get; set;}
        public DateTime HireDate { get; set; }
        public DateTime DateOfCreation { get; set;} = DateTime.Now;
        public string ImageUrl { get; set;}
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }


    }
}
