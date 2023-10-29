using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAppMAUI.Domain.Common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; private set; }
        //public DateTimeOffset DateCreated { get; private set; }
        //public DateTimeOffset? DateUpdated { get; private set; }
        // DateTimeOffset? DateDeleted { get; private set; }


        public BaseEntity()
        {
            //Id = Guid.NewGuid();
            //DateCreated = DateTimeOffset.UtcNow;
        }
    }
}
