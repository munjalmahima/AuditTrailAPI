using AuditTrailRepository.Interface;
using AuditTrailShared.Models;

namespace AuditTrailRepository.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly List<AuditResponse> _auditResponseList = [];

        public void Save(AuditResponse audit) => _auditResponseList.Add(audit);

        public List<AuditResponse> GetAll() => _auditResponseList;

        public List<AuditResponse> GetByEntityName(string entityName) =>
            _auditResponseList.Where(a => a.EntityName.Equals(entityName, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<AuditResponse> GetByUserId(string userId) =>
            _auditResponseList.Where(a => a.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase)).ToList();

        public void Clear() => _auditResponseList.Clear();
    }
}
