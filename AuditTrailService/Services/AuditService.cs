using AuditTrailRepository.Interface;
using AuditTrailService.Interface;
using AuditTrailShared.Enums;
using AuditTrailShared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AuditTrailService.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;

        public AuditService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        private static Dictionary<string, object> ToDictionary(object? obj)
        {
            return obj == null ? new() : JsonConvert.DeserializeObject<Dictionary<string, object>>(obj.ToString() ?? "{}") ?? new();
        }

        private static JToken ParseJsonOrRaw(string input)
        {
            try
            {
                return string.IsNullOrWhiteSpace(input)
                    ? JValue.CreateNull()
                    : JToken.Parse(input);
            }
            catch
            {
                return input == null ? JValue.CreateNull() : new JValue(input);
            }
        }

        public AuditResponse GenerateAuditTrail(AuditRequest request)
        {
            var beforeDict = ToDictionary(request.Before);
            var afterDict = ToDictionary(request.After);

            List<ChangeLog> changes = [];

            foreach (var key in request.Action switch
            {
                AuditAction.Created => afterDict.Keys,
                AuditAction.Deleted => beforeDict.Keys,
                AuditAction.Updated => afterDict.Keys.Union(beforeDict.Keys),
                _ => Enumerable.Empty<string>()
            })
            {
                beforeDict.TryGetValue(key, out var oldVal);
                afterDict.TryGetValue(key, out var newVal);

                switch (request.Action)
                {
                    case AuditAction.Created when newVal != null:
                        changes.Add(new ChangeLog { PropertyName = key, NewValue = newVal });
                        break;

                    case AuditAction.Deleted when oldVal != null:
                        changes.Add(new ChangeLog { PropertyName = key, OldValue = oldVal });
                        break;

                    case AuditAction.Updated:
                        var oldToken = ParseJsonOrRaw(oldVal?.ToString());
                        var newToken = ParseJsonOrRaw(newVal?.ToString());

                        if (!JToken.DeepEquals(oldToken, newToken))
                        {
                            changes.Add(new ChangeLog
                            {
                                PropertyName = key,
                                OldValue = oldVal,
                                NewValue = newVal
                            });
                        }
                        break;
                }
            }

            var audit = new AuditResponse
            {
                Action = request.Action,
                Changes = changes,
                Timestamp = DateTime.UtcNow,
                UserId = request.UserId,
                EntityName = request.EntityName
            };

            _auditRepository.Save(audit);
            return audit;
        }

        public List<AuditResponse> GetAll()
        {
            return _auditRepository.GetAll();
        }

        public List<AuditResponse> GetByEntityName(string entityName)
        {
            return _auditRepository.GetByEntityName(entityName);
        }

        public List<AuditResponse> GetByUserId(string userId)
        {
            return _auditRepository.GetByUserId(userId);
        }

        public void Clear()
        {
            _auditRepository.Clear();
        }
    }
}
