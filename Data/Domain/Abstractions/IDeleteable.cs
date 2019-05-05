using System.Data.SqlTypes;
using System;
namespace Data.Domain.Abstractions
{
    public interface IDeleteable<TDeleter, TDeletedKey> where TDeletedKey : struct
    {
        DateTime? DeleteTime { get; set; }
        TDeletedKey? DeleterId { get; set; }
        TDeleter Deleter { get; set; }
    }
}