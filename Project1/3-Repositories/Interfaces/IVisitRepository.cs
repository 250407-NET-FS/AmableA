using Project1.Models;
public interface IVisitRepository{

    Task<Visit> PostVisit(Visit visit);

    Task<List<Visit>> PostListOfVisits(List<Visit> visit);

    Task<Visit?> GetVisit(Guid id);

    Task<List<Visit>> GetAllVisits();

    Task<List<Visit>> GetAllVisitsByCustomerId(Guid id);

    Task<List<Visit>> GetAllVisitsByStoreId(int id);

    Task<bool> DeleteVisit(Guid id);
}