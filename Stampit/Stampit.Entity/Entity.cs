using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Entity
{
    /// <summary>
    /// Base for all entities in the data schema of the stampit application
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// The unique identifier of the entity as string to be not guessable
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        /// <summary>
        /// DateTime when the entity has been persisted for the first time
        /// </summary>
        public virtual DateTime CreatedAt { get; set; }
        /// <summary>
        /// Nullable DateTime when the entity has been updated the last time, if it is null no updated occured and the createdAt date is valid for last update
        /// </summary>
        public virtual DateTime? UpdatedAt { get; set; }
    }
}
