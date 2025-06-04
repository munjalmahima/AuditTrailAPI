using AuditTrailShared.Enums;
using AuditTrailShared.Models;
using FluentValidation;
using Newtonsoft.Json.Linq;

namespace AuditTrailAPI.Validators
{
    public class AuditRequestValidator : AbstractValidator<AuditRequest>
    {
        public AuditRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Before)
            .Must(BeNullOrValidJObject)
            .WithMessage("'Before' must be either null or a valid JSON object.");

            RuleFor(x => x.After)
           .Must(BeNullOrValidJObject)
           .WithMessage("'After' must be either null or a valid JSON object.");

            RuleFor(x => x.EntityName)
                .NotEmpty().WithMessage("EntityName is required.");

            RuleFor(x => x.Action)
                .NotNull().WithMessage("Action is required.")
                .Must(action => Enum.IsDefined(typeof(AuditAction), action))
                .WithMessage("Invalid audit action. Allowed values are: 0 (Created), 1 (Updated), 2 (Deleted).");

            RuleFor(x => x.Before)
                .NotNull().When(x => x.Action == AuditAction.Deleted || x.Action == AuditAction.Updated)
                .WithMessage("'Before' data must be provided for Deleted or Updated actions.");

            RuleFor(x => x.After)
                .NotNull().When(x => x.Action == AuditAction.Created || x.Action == AuditAction.Updated)
                .WithMessage("'After' data must be provided for Created or Updated actions.");
        }

        private bool BeNullOrValidJObject(JObject obj)
        {
            return obj == null || obj.Type == JTokenType.Object;
        }
    }
}
