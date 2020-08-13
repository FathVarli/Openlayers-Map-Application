using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BasarSoftStajMVC.DataBase
{
    [Table("dbo.User")]
    public class User 
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public string EMail { get; set; }

        public string Password { get; set; }
    }
}