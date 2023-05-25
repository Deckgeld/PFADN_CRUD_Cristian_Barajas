using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passengers.Dto
{
    public class PassengerDto
    {
        public int Id { get; set; }
        [StringLength(32)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(32)]
        [Required]
        public string LastName { get; set; }
        
        public int Age { get; set; }
    }
}
