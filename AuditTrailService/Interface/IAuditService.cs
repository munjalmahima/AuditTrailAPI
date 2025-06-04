using AuditTrailShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditTrailService.Interface
{
    public interface IAuditService
    {
        AuditResponse GenerateAuditTrail(AuditRequest request);
        List<AuditResponse> GetAll();
        List<AuditResponse> GetByEntityName(string entityName);
        List<AuditResponse> GetByUserId(string userId);
        void Clear();
    }
}
