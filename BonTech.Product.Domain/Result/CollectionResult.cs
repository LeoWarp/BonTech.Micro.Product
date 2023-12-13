namespace BonTech.Product.Domain.Result;

public class CollectionResult<T> : Result<IEnumerable<T>>
{
    public int Count { get; set; }
}