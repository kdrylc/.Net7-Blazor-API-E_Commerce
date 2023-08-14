using E_Commerce_UI.ViewModels;

namespace E_Commerce_UI.Service.IService
{
    public interface ICartService
    {
        public event Action OnChange;
        Task DecrementItem(ShoppingCart shoppingCart);
        Task IncrementItem(ShoppingCart shoppingCart);
    }
}
