using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using customModelValidation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace customModelValidation.ViewModels
{
    public abstract class AbstractValidatableObject : IValidatableObject
    {
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CancellationTokenSource source = new CancellationTokenSource();

            var task = ValidateAsync(validationContext, source.Token);

            Task.WaitAll(task);

            return task.Result;
        }

        public virtual Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext, CancellationToken cancellation)
        {
            return Task.FromResult((IEnumerable<ValidationResult>)new List<ValidationResult>());
        }
    }

    public class NewOrder : AbstractValidatableObject
    {
        public int StoreId { get; set; }

        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext, CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();
            var services = validationContext.GetService<IDbServices>();
            if (!await services.IsStoreExists(this.StoreId))
                errors.Add(new ValidationResult("Store exist", new[] { nameof(StoreId) }));

            return errors;
        }
    }

    public class OrderModel : IValidatableObject
    {
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} field must be between {1}-{2}")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} field is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Max")]
        public int Max { get; set; }

        [Display(Name = "Min")]
        public int Min { get; set; }

        [Required]
        [Display(Name = "Information")]
        public string Info { get; set; }

        [Display(Name = "Age Of User")]
        public int Age { get; set; }

        [Display(Name = "Store")]
        public int StoreId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Min > 0 && this.Min >= this.Max)
            {
                yield return new ValidationResult("Min field must be less then Max field");
            }
            if (this.Info == "Test")
            {
                yield return new ValidationResult($"Info: {Info} field cannot be Test.", new[] { nameof(Info) });
            }

            var db = validationContext.GetService<IDbServices>();
            if (db.IsStoreExists(this.StoreId).Result == false)
                yield return new ValidationResult($"Store: {StoreId} not found", new[] { nameof(StoreId) });

            // var errors = new List<ValidationResult>();
            // if (Age < 18)
            //     errors.Add(new ValidationResult($"Age: {Age} not allowed to work. Minimum age is 18 or more", new[] { nameof(Age) }));

            // return errors;
        }
    }
}