namespace BonTech.Product.Domain.Enum;

public enum ErrorCodes
{
    /// <summary>
    /// Продукт не найден для получения из БД
    /// </summary>
    ProductNotFound1 = 1,
    /// <summary>
    /// Продукт не найден для обновления
    /// </summary>
    ProductNotFound2 = 2,
    
    /// <summary>
    /// Продукты не найдены
    /// </summary>
    ProductsNotFound = 3,
    
    /// <summary>
    /// Продукт с таким названием уже есть в системе
    /// </summary>
    ProductAlreadyExists = 4,
    
    /// <summary>
    /// Продукт не был создаг
    /// </summary>
    ProductNotCreated = 5,
    
    /// <summary>
    /// Продукт не был удален
    /// </summary>
    ProductNotDeleted = 6,
    
    /// <summary>
    /// Категория не найдена для создания
    /// </summary>
    CategoryNotFound7 = 7,
    
    /// <summary>
    /// Категория не найдена для обновления 
    /// </summary>
    CategoryNotFound8 = 8,
    
    /// <summary>
    /// Категория не найдена для удаления 
    /// </summary>
    CategoryNotFound9 = 9,
    
    InternalServerError = 10
}