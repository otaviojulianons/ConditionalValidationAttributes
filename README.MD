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
Obriga o preechimento da propriedade se a condição em questão for verdadeira.
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