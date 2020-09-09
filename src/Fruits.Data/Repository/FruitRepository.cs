using Fruits.Business.Interfaces;
using Fruits.Business.Models;
using Fruits.Data.Context;

namespace Fruits.Data.Repository
{
    public class FruitRepository: Repository<Fruit>, IFruitRepository
    {
        public FruitRepository(FruitsDBContext fruitsDBContext) : base(fruitsDBContext)
        {

        }
    }
}