using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Register.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

      

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public string Last_Altered { get; set; }

        public User User { get; set; }

       
    }
}
