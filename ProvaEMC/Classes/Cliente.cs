using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaEMC.Classes
{
    public class Cliente : Pessoa
    {

        [Key]
        public int Id { get; set; }
        
        [Required]
        public int Idade { get; set; }

    }
}
