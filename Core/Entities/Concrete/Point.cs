﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    [Table("dbo.Point")]
    public class Point:IEntity
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("KapiNo")]
        public string KapiNo { get; set; }
        [Column("x")]
        public float x { get; set; }
        [Column("y")]
        public float y { get; set; }
    }
}
