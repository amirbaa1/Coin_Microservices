using Microsoft.AspNetCore.Mvc;
using WebApp.Model.OrderModel;

namespace WebApp.Services;

public interface IOrderService
{
    Task<List<OrderModel>> GetOrder(string username);
}