using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace alican_yılmaz_come_back.Models
{
    [Table("kisiler")]
    public class kisiler
    {   
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(20),Required]
        public string ad { get; set; }

        [StringLength(20),Required]
        public string soyad { get; set; }

        [Required]
        public int yas { get; set; }

        public virtual List<adresler> adreslers { get; set; }
    }
}