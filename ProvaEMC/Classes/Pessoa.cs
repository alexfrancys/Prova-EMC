using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvaEMC.Classes
{
    public class Pessoa
    {  
        [Required]
        [StringLength(60)]
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
        public virtual List<Contato> Contato { get; set; }
    }
}
