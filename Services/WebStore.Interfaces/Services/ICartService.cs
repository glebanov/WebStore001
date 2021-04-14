using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
{
    public interface ICartService
    {
        void Add(int id); //Добавить товар

        void Decrement(int id); // Уменьшить товар

        void Remove(int id); // Удалить товар

        void Clear(); // Очистить корзину

        CartViewModel GetViewModel(); // Преобразовать корзину в модель представления
    }
}