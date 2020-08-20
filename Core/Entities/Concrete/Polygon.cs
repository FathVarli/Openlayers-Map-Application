using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;

namespace Core.Entities.Concrete
{
    [Table("dbo.Polygon")]
    public class Polygon:IEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("polygon_name")]
        public string PolygonName { get; set; }
        [Column("coordinates")]
        public DbGeometry Coordinates { get; set; }
     
    }
}
