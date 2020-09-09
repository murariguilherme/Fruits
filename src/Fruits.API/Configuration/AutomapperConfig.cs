using AutoMapper;
using Fruits.App.ViewModels;
using Fruits.Business.Models;

namespace Fruits.API.Configuration
{
    public class AutomapperConfig: Profile
    {
        public AutomapperConfig()
        {
            CreateMap<FruitViewModel, Fruit>().ReverseMap();
        }
    }
}
