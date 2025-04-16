using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Repositories;
public class ReceiptRepository : IReceiptRepository
{

    private readonly ApplicationDbContext _context;

    public ReceiptRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Receipt> PostReceipt(Receipt receipt)
    {
        _context.Receipts.Add(receipt);
        await _context.SaveChangesAsync();

        return receipt;
    }


    public async Task<List<Receipt>> PostListOfReceipt(List<Receipt> receipts)
    {

        await _context.Receipts.AddRangeAsync(receipts);
        await _context.SaveChangesAsync();

        return receipts;

    }



    public async Task<Receipt?> GetReceipt(Guid id)
    {
        return await _context.Receipts
        .Include(r => r.ReceiptItem)
        .FirstOrDefaultAsync(r => r.Id == id);
    }


    public async Task<List<Receipt>> GetAllReceipts()
    {
        return await _context.Receipts.Include(r => r.ReceiptItem).ToListAsync();
    }

    public async Task<List<Receipt>> GetAllReceiptsByStore(int storeId)
    {
        return await _context.Receipts
        .Where(v => v.Visit.StoreId == storeId)
        .Include(r => r.ReceiptItem)
        .ToListAsync();

    }

    public async Task<List<Receipt>> GetAllReceiptsByCustomer(Guid customerId)
    {
        return await _context.Receipts
        .Where(v => v.Visit.CustomerId == customerId)
        .Include(r => r.ReceiptItem)
        .ToListAsync();
    }

    public async Task<List<Receipt>> GetAllReceiptsByStoreAndCustomer(int storeId, Guid customerId)
    {
        return await _context.Receipts
        .Where(v => v.Visit.CustomerId == customerId && v.Visit.StoreId == storeId)
        .Include(r => r.ReceiptItem)
        .ToListAsync();
    }

    public async Task<List<Receipt>> GetAllReceiptsByVisit(Guid id)
    {
        return await _context.Receipts
        .Where(r => r.VisitId == id)
        .ToListAsync();

    }



}