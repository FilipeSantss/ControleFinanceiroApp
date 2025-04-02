using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Win32;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace ControleFinanceiro.Models {
    public class Entrada {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
    }



    public class Despesa {
        public int Id { get; set; } // Propriedade do ID

        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateTime Data { get; set; } // Propriedade da Data

        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; } // Propriedade do Valor

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public string Categoria { get; set; } // Propriedade da Categoria

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } // Propriedade da Descrição
    }


}