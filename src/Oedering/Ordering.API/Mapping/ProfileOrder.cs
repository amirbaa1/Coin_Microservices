using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.Mapping
{
    public class ProfileOrder : Profile
    {
        public ProfileOrder()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckOutEvent>().ReverseMap();
        }
    }
}
