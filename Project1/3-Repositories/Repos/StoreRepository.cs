using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project1.Models;
using Microsoft.EntityFrameworkCore;

namespace Project1.Repositories;

public class StoreRepository : IStoreRepository
{
    private readonly ApplicationDbContext _context;
    public StoreRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Store> PostStore(Store store)
    {
        _context.Stores.Add(store);
        await _context.SaveChangesAsync();

        return store;

    }


    public async Task<List<Store>> PostListOfStore(List<Store> stores)
    {
        await _context.Stores.AddRangeAsync(stores);
        await _context.SaveChangesAsync();

        return stores;

    }

    public async Task<Store?> GetStore(int id)
    {
        return await _context.Stores.FindAsync(id);
    }

    public async Task<List<Store>> GetAllStores()
    {
        return await _context.Stores.ToListAsync();
    }




}