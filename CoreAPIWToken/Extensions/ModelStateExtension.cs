using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary modelState) => modelState.SelectMany(e => e.Value.Errors).Select(m => m.ErrorMessage).ToList();
    }
}
