using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STM.AIU.Domain.Common;
internal abstract class BaseAuditableEntity<PrimaryKeyType> : BaseEntity<PrimaryKeyType>
{
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
}
