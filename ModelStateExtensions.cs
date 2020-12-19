using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace customModelValidation
{
    public static class ModelStateExtensions
    {
        public static List<string> GetFaultyParametres(this ModelStateDictionary model)
        {
            var errors = model.Values.SelectMany(v => v.Errors);
            var errorList = errors.GroupBy(x => x.Exception == null ? x.ErrorMessage : x.Exception.Message)
                .Select(x => x.Key).ToList();

            return errorList;
        }

    }
}