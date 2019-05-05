using System;
namespace Data.Domain.Abstractions
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
        DateTime CreationDateTime { get; set; }
    }
}