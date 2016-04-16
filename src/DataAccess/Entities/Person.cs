using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("People")]
    public partial class Person : AuditEntityBase, IEntity
    {
        [Key]
        public int Id { get; set; }

        #region Properties
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        #endregion
    }
}
