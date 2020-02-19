using CQRS.Extensions;
using CQRS.Extensions.Interfaces;
using CQRS.Extensions.Pipelines;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PackUtils;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            PropertyName.Resolver = (propertyName) =>
            {
                var parts = propertyName.Split('.');
                return string.Join(".", parts.Select(r => r.ToSnakeCase()));
            };

            var serviceProvider = SetupServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var command1 = new CreateAccountCommand { Name = "success", CustomNumber = 2 };
            var result_success = mediator.Send(command1).GetAwaiter().GetResult();

            var command2 = new CreateAccountCommand { Name = "Barradas", CustomNumber = 2 };
            var result_internal_fail = mediator.Send(command2).GetAwaiter().GetResult();

            var command3 = new CreateAccountCommand { Name = "fail", CustomNumber = 0 };
            var result_validation_fail = mediator.Send(command3).GetAwaiter().GetResult();
        }

        private static ServiceProvider SetupServiceProvider()
        {
            var services = new ServiceCollection();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic);

            AssemblyScanner
                .FindValidatorsInAssemblies(assemblies)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies(), null);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorPipelineBehavior<,>));

            return services.BuildServiceProvider();
        }
    }

    public class CreateAccountCommand : ICommand<Account>
    {
        public string Name { get; set; }

        public int CustomNumber { get; set; }

        public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, Account>
        {
            public CreateAccountCommandHandler()
            {
            }

            public async Task<Result<Account>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                var account = new Account
                {
                    Name = request.Name,
                    Number = request.CustomNumber
                };

                if (account.Name == "Barradas")
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
        public string Test => "Just a little test";

        public string Name { get; set; }

        public int Number { get; set; }
    }
}