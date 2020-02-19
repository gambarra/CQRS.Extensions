# CQRS Extensions

CQRS interfaces and extensions to simpify MediatR;

## Install via NuGet

```
PM> Install-Package CQRS.Extensions
PM> Install-Package CQRS.Extensions.AspNetMVC
```

## How to use

```
var mediator = provider.GetService<IMediator>();

var command = new CreateAccountCommand { Name = "success", CustomNumber = 2 };
var result = mediator.Send(command).GetAwaiter().GetResult();
```

Command/Query, Models, Handle
```

public class CreateAccountCommand : ICommand<Account>
{
    public string Name { get; set; }

    public int CustomNumber { get; set; }

    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, Account>
    {
        public CreateAccountCommandHandler() {}

        public async Task<Result<Account>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                Name = request.Name,
                Number = request.CustomNumber
            };

            if (account.Name == "breaks")
            {
                return Result.Fail<Account>("error", 500);
            }

            return Result.Success(account);
        }
    }
}

public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty();

        RuleFor(p => p.CustomNumber)
            .NotEmpty();
    }
}

public class Account
{
    public string Name { get; set; }

    public int Number { get; set; }
}
```

## How can I contribute?
Please, refer to [CONTRIBUTING](.github/CONTRIBUTING.md)

## Found something strange or need a new feature?
Open a new Issue following our issue template [ISSUE TEMPLATE](.github/ISSUE_TEMPLATE.md)
