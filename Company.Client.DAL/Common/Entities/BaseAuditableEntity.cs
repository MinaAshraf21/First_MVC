

namespace Company.Client.DAL.Common.Entities
{
    public class BaseAuditableEntity<TKey> : BaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public /*required*/ string CreatedBy { get; set; } // FK refers to the user GUID
        public DateTime CreatedOn { get; set; }
        public /*required*/ string LastModifiedBy { get; set; } // FK refers to the user GUID
        public DateTime LastModifiedOn { get; set; }

    }
}
