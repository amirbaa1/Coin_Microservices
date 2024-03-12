using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Model.AccountModel;
using WebApp.Model.Basket;
using WebApp.Services;

namespace WebApp.Pages.Order
{
    public class CheckOutModel : PageModel
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<CheckOutModel> _logger;
        private readonly UserManager<AppUser> _userManager;

        public CheckOutModel(IBasketService basketService, ILogger<CheckOutModel> logger, UserManager<AppUser> userManager)
        {
            _basketService = basketService;
            _logger = logger;
            _userManager = userManager;
        }
        [BindProperty]
        public CheckOut check { get; set; }
        [BindProperty]
        public CoinCart CoinCartItem { get; set; }
        public async Task OnGet()
        {
            var userGet = await _userManager.GetUserAsync(User);
            var getCoinUser = await _basketService.GetBasket(userGet.UserName);

            //_logger.LogInformation($"get Coin for page :{getCoinUser}");

            CoinCartItem = getCoinUser;


        }
        public async Task<IActionResult> OnPost()
        {
            var userGet = await _userManager.GetUserAsync(User);
            var coinUser = await _basketService.GetBasket(userGet.UserName);

            check.EmailAddress = _userManager.FindByNameAsync(User.Identity.Name).Result.Email;
            check.UserName = coinUser.UserName;

            check.FirstName = "Tester";
            check.LastName = "TesterB";


            check.CoinId = coinUser.CoinCarts.CoinId;
            check.CoinName = coinUser.CoinCarts.CoinName;
            check.PriceCoin = coinUser.CoinCarts.PriceCoin;
            check.Amount = coinUser.CoinCarts.Amount;
            check.TotalPrice = coinUser.TotalPrice;
            check.DateTime = DateTime.Now;


            _logger.LogInformation($"CheckOut send:{check}");

            _basketService.CheckOutBasket(check);
            TempData["sendToIdnex"] = $"buy coin {check.CoinName}";

            return RedirectToPage("Index");

        }
    }
}
