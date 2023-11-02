using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAppMAUI.Domain.Common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

    }
}
