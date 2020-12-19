using System.Collections;
using System.Collections.Generic;

namespace customModelValidation.ViewModels
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<string> FaultyParameters { get; set; }
    }
}