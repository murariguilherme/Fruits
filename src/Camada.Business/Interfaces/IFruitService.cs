using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fruits.Business.Models;

namespace Fruits.Business.Interfaces
{
    public interface IFruitService: IDisposable
    {
        Task Add(Fruit fruit);
        Task Remove(Guid id);
        Task Update(Fruit fruit);
        Task<List<Fruit>> GetAll();
        Task<IEnumerable<Fruit>> Search(Expression<Func<Fruit, bool>> predicate);
        Task<Fruit> GetById(Guid id);
    }
}