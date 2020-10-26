using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaEMC.Classes
{
    public class Fornecedor : Pessoa
    {
        [Key]
        public int Id { get; set; }
        public int PrazoEntrega { get; set; }

    }
}
