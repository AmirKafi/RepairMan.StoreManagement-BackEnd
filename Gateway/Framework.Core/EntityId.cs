using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class EntityId<T> : SoftDelete where T : struct
    {
        public T Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;


        //public int CreatedBy { get; set; }
        //public int? DeletedBy { get; set; }

        public override bool Equals(object obj)
        {
            if (this.GetType() != obj.GetType()) return false;
            var otherId = obj as EntityId<T>;
            return otherId != null && otherId.Id.Equals(this.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public virtual void Delete()
        {
            this.IsDeleted = true;
            this.DeletedOn = DateTime.Now;
            //DeletedBy = 
        }
    }

    public class SoftDelete
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
    }
}
