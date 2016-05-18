using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        [Newtonsoft.Json.JsonConverter(typeof(IsoDateTimeWithoutPlusConverter))]
        public virtual DateTime CreatedAt { get; set; }
        /// <summary>
        /// Nullable DateTime when the entity has been updated the last time, if it is null no updated occured and the createdAt date is valid for last update
        /// </summary>
        public virtual DateTime? UpdatedAt { get; set; }

        public override bool Equals(object obj)
        {
            Entity other = obj as Entity;
            if (obj == null || other == null) return false;

            if (string.IsNullOrEmpty(other?.Id) && string.IsNullOrEmpty(this.Id))
                return base.Equals(obj);
            return other?.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {Id}";
        }

        public static bool operator == (Entity entity, Entity other)
        {
            return entity?.Equals(other) ?? false;
        }

        public static bool operator !=(Entity entity, Entity other)
        {
            return !(entity == other);
        }
    }

    public class IsoDateTimeWithoutPlusConverter : IsoDateTimeConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFF";
            base.DateTimeFormat = serializer.DateFormatString;
            base.WriteJson(writer, value, serializer);
        }
    }
}
