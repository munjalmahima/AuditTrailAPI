using AuditTrailShared.Models;

namespace AuditTrailRepository.Interface
{
    public interface IAuditRepository
    {
        void Save(AuditResponse audit);
        List<AuditResponse> GetAll();
        List<AuditResponse> GetByEntityName(string entityName);
        List<AuditResponse> GetByUserId(string userId);
        void Clear();
    }
}
