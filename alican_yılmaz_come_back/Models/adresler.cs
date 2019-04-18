using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace alican_yılmaz_come_back.Models
{
    [Table("adresler")]
    public class adresler
    {   
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(300)]
        public string adres_tanim { get; set; }

        public virtual kisiler kisi  { get; set; } 
    }
} 