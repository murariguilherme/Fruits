using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fruits.Business.Interfaces;
using Fruits.Business.Models;
using Fruits.Business.Validations;

namespace Fruits.Business.Services
{
    public class FruitService: BaseService, IFruitService
    {
        private readonly IFruitRepository _fruitRepository;

        public FruitService(IFruitRepository fruitRepository,
            INotificator notificator): base(notificator)
        {
            _fruitRepository = fruitRepository;
        }

        public async Task Add(Fruit fruit)
        {
            if (!ExecuteValidation(new FruitValidation(), fruit)) return;
            
            /*if (_fruitRepository.Search(f => f.Name == fruit.Name).Result.Any())
            {
                Notificate("Existe uma fruit com o nome informado na base de dados.");
                return;
            }*/

            await _fruitRepository.Add(fruit);
        }

        public async Task Remove(Guid id)
        {
            await _fruitRepository.Remove(id);
        }

        public async Task Update(Fruit fruit)
        {
            await _fruitRepository.Update(fruit);
        }

        public async Task<List<Fruit>> GetAll()
        {
            return await _fruitRepository.GetAll();
        }

        public async Task<IEnumerable<Fruit>> Search(Expression<Func<Fruit, bool>> predicate)
        {
            return await _fruitRepository.Search(predicate);
        }

        public async Task<Fruit> GetById(Guid id)
        {
            return await _fruitRepository.GetById(id);
        }

        public void Dispose()
        {
            _fruitRepository?.Dispose();
        }
    }
}