using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaEMC.Classes
{
    public class Endereco
    {
        [Required] 
        [MaxLength(80)]
        public string Rua { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        [MaxLength(50)]
        public string Cep { get; set; }

        [MaxLength(85)]
        public string Complemento { get; set; }

        [Required]
        [MaxLength(50)]
        public string Bairro { get; set; }

        [Required]
        [MaxLength(60)]
        public string Cidade { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; }
    }
}
