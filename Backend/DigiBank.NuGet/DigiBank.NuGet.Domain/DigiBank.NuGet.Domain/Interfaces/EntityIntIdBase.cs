using System.ComponentModel.DataAnnotations;

namespace DigiBank.NuGet.Domain.Interfaces;

public abstract class EntityIntIdBase : IEntity<int>
{
    private int? _oldHashCode;

    [Key]
    public virtual int Id { get; set; }

    public virtual bool IsTransient
    {
        get
        {
            return (Id == 0);
        }
    }

    public DateTime DataPersistenciaRegistro { get; set; }

    public override int GetHashCode()
    {
        if (_oldHashCode.HasValue)
            return _oldHashCode.Value;

        if (IsTransient)
        {
            _oldHashCode = base.GetHashCode();
            return _oldHashCode.Value;
        }

        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as IEntity<int>);
    }

    public bool Equals(IEntity<int>? other)
    {
        if (other == null)
        {
            return false;
        }

        if (IsTransient)
        {
            return ReferenceEquals(this, other);
        }

        return other.Id == Id && other.GetType() == GetType();
    }
}