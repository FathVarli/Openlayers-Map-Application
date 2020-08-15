using System;
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
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("point_no")]
        public string PointNo { get; set; }
        [Column("x_coordinate")]
        public float XCoordinate { get; set; }
        [Column("y_coordinate")]
        public float YCoordinate { get; set; }
    }
}
