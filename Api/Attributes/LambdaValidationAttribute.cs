using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Api.Attributes
{
    public class LambdaValidationAttribute : ValidationAttribute
    {
        public string Lambda { get; set; }

        public LambdaValidationAttribute(string lambda)
        {
            Lambda = lambda;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ScriptOptions options = ScriptOptions.Default;
            options = options.AddReferences(validationContext.ObjectType.Assembly);

            options = options.AddImports(
                "System.Collections.Generic",
                "System",
                validationContext.ObjectType.Namespace
                );

            var param = Lambda.Split("=>").FirstOrDefault();
            if (!param.Trim().Contains(" "))
                Lambda = Lambda.Replace(param, $"( dynamic {param} )");

            var function = CSharpScript.EvaluateAsync<Func<dynamic, bool>>(Lambda, options ).Result;

            return function(validationContext.ObjectInstance)
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage);
        }
    }
}
