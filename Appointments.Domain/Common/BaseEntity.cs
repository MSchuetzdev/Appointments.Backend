using System.Diagnostics.CodeAnalysis;

namespace Appointments.Domain.Common;

public class BaseEntity<TEntityId> : IEquatable<BaseEntity<TEntityId>> where TEntityId : struct, IEquatable<TEntityId>
{
    
    public TEntityId Id { get; set; }
    
    
    public bool Equals(BaseEntity<TEntityId>? other)
    {
        if(ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true; 
        return obj.GetType() == GetType() && Equals((BaseEntity<TEntityId>) obj);
    }
    
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}