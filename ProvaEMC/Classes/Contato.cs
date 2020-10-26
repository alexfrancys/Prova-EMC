using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaEMC.Classes
{
    public class Contato
    {
        [Key]
        public int ContatoId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public string Tipo { get; set; }
                
        public virtual Cliente Cliente { get; set; }       
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
