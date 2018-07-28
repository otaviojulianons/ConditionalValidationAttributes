using Api.Attributes;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class UserModel
    {
        
        [LambdaValidation("u => new List<char>(){ 'F','M' }.Contains(u.Gender)",ErrorMessage = "Invalid gender.")]
        public char Gender { get; set; }

        public string Email { get; set; }

        [RequiredIf(nameof(Email),ErrorMessage = "Enter email password.")]
        public string Password { get; set; }

        [LambdaValidation("u => u.Password == u.PasswordConfirm", ErrorMessage = "Different passwords.")]
        public string PasswordConfirm { get; set; }

        [RequiredIf(nameof(Email), ValueExpected = null, ErrorMessage = "Enter Username.")]
        public string Username { get; set; }




    }
}
