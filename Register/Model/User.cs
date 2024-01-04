using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Register.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string UserLogin { get; set; }

        [Required]
        [StringLength(255)]
        public string UserPassword { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName{ get; set; }
        public ICollection<Employee> Employees { get; set; }


    }
}
