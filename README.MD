# ConditionalValidationAttributes

Este repositório mostra alguns exemplos de validações condicionais em *Model*.

## LambdaValidation
Executa uma expressão lambda no formato de *string* para validação.
```csharp
    public class UserModel
    {
        .  .  .
        [Required(ErrorMessage = "Enter email password.")]
        public string Password { get; set; }

        [LambdaValidation("u => u.Password == u.PasswordConfirm", ErrorMessage = "Different passwords.")]
        public string PasswordConfirm { get; set; }

        .  .  .

    }
```

## RequiredIf
Executa a validação no campo informado verificando valor esperado.
```csharp
    public class UserModel
    {
        .  .  .

        public string Email { get; set; }

        [RequiredIf(nameof(Email), ValueExpected = null, ErrorMessage = "Enter Username.")]
        public string Username { get; set; }

        .  .  .        
    }
```