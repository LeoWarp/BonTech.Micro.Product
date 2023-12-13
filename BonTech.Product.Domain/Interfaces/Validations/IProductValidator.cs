using BonTech.Product.Domain.Entity;

namespace BonTech.Product.Domain.Interfaces.Validations;

/// <summary>
/// Класс валидации отвечающий за продукт
/// </summary>
public interface IProductValidator
{
    /// <summary>
    /// Проверяется наличие продукта, если продукт с переданным названием есть в БД, то создать точно такой же нельзя
    /// Затем проверяется переданная категория, если её нет в БД, то продукт не будет создан
    /// </summary>
    /// <param name="product"></param>
    /// <param name="categories"></param>
    /// <returns></returns>
    Result.Result ValidateCreate(Entity.Product product, IEnumerable<Category> categories);

    /// <summary>
    /// Проверяется наличие продукта и категории по названию, если объекты есть в системе, то продукт будет обновлен
    /// </summary>
    /// <param name="product"></param>
    /// <param name="category"></param>
    Result.Result ValidateUpdate(Entity.Product product, IEnumerable<Category> categories);
    
    /// <summary>
    /// Проверяется продукт на налиичие, если продукт найден по Id, то он будет удален
    /// </summary>
    /// <param name="product"></param>
    Result.Result ValidateDelete(Entity.Product product);
}