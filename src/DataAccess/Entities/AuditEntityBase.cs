using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public abstract class AuditEntityBase
    {
        /// <summary>
        /// Gets or sets the created date/time. 
        /// </summary>
        /// <value>
        /// The created date/time.
        /// </value>
        [Editable(false)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the create user.
        /// </summary>
        /// <value>
        /// The create user.
        /// </value>
        [StringLength(30)]
        [Editable(false)]
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the modified date/time.
        /// </summary>
        /// <value>
        /// The modified date/time.
        /// </value>
        [Editable(false)]
        public DateTime Modified { get; set; }

        /// <summary>
        /// Gets or sets the modify user.
        /// </summary>
        /// <value>
        /// The modify user.
        /// </value>
        [StringLength(30)]
        [Editable(false)]
        public string ModifyUser { get; set; }

        /// <summary>
        /// Timestamp property
        /// </summary>
        [Timestamp]
        public Byte[] Timestamp { get; set; } 
    }
}
