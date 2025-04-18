using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Repositories;
public class VisitRepository : IVisitRepository
{

    private readonly ApplicationDbContext _context;

    public VisitRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Visit> PostVisit(Visit visit)
    {
        _context.Visits.Add(visit);
        await _context.SaveChangesAsync();

        return visit;

    }


    public async Task<List<Visit>> PostListOfVisits(List<Visit> visit)
    {
        await _context.Visits.AddRangeAsync(visit);
        await _context.SaveChangesAsync();

        return visit;

    }

    public async Task<Visit?> GetVisit(Guid id)
    {
        return await _context.Visits.FindAsync(id);
    }

    public async Task<List<Visit>> GetAllVisits()
    {
        return await _context.Visits.ToListAsync();
    }

    public async Task<List<Visit>> GetAllVisitsByCustomerId(Guid id)
    {
        return await _context.Visits
        .Where(c => c.CustomerId == id)
        .ToListAsync();
    }

    public Task<List<Visit>> GetAllVisitsByStoreId(int id)
    {
        throw new NotImplementedException();
    }

        public async Task<bool> DeleteVisit(Guid id)
    {
        var visit = await GetVisit(id);
        if (visit != null)
        {
            _context.Visits.Remove(visit); 
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}