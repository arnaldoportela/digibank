using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.NuGet.Domain.Interfaces;

public interface IEntity : IValueObject
{
    bool IsTransient { get; }

    DateTime DataPersistenciaRegistro { get; set; }
}

public interface IEntity<TIdentifier> : IEntity, IEquatable<IEntity<TIdentifier>>
{
    TIdentifier Id { get; set; }
}