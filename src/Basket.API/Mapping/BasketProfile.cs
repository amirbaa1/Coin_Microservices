using AutoMapper;
using Basket.API.Model;
using EventBus.Messages.Events;

namespace Basket.API.Mapping
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CheckOut, BasketCheckOutEvent>().ReverseMap();
        }
    }
}