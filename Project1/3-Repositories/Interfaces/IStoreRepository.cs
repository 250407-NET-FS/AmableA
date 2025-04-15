using Project1.Models;

public interface IStoreRepository
{
    Task<Store> PostStore(Store store);
    Task<Store?> GetStore(int id);

    Task<List<Store>> GetAllStores();

    Task<List<Store>> PostListOfStore(List<Store> stores);
}