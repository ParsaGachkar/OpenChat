using System;
namespace Data.Domain.Abstractions
{
    public interface IDeleteable<TDeleter, TDeletedKey>
    {
        DateTime? DeleteTime { get; set; }
        TDeletedKey DeleterId { get; set; }
        TDeleter Deleter { get; set; }
    }
}