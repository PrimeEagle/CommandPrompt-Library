using System.Collections.Generic;
using System.IO;
using VNet.CommandLine;
using VNet.CommandLine.Validation;
using VNet.CommandLine.Validation.FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace VNet.CommandLineTest
{
    public static class ConfigTestServices
    {
        public static TestServices ConfigureServices()
        {
            var alternateValidatorServices = new ServiceCollection();
            alternateValidatorServices.AddScoped<IAdditionalValidators>(p => 
                ActivatorUtilities.CreateInstance<AdditionalValidators>(p, 
                    new List<ICommandValidator>
                    {
                        new ArgumentArrayValueValidator(),
                        new ArgumentValuePairValidator(),
                        new UnknownOptionsValidator(),
                        new AssemblyValidator(),
                        new CommandVerbAttributeValidator(),
                        new CommandOptionAttributeValidator(),
                        new CommandVerbAttributeCollectionValidator(),
                        new CommandOptionAttributeCollectionValidator()
                    }));

            var alternateValidatorServiceProvider = alternateValidatorServices.BuildServiceProvider();



            var validatorServices = new ServiceCollection();
            validatorServices.AddScoped<ICommandValidator<IVerb>, VerbValidator>()
                .AddScoped<ICommandValidator<IOption>, OptionValidator>()
                .AddScoped<ICommandValidator<IVerbCollection>, VerbCollectionValidator>()
                .AddScoped<ICommandValidator<IOptionCollection>, OptionCollectionValidator>()
                .AddScoped(p => alternateValidatorServiceProvider.GetService<IAdditionalValidators>());
            
            var validatorServiceProvider = validatorServices.BuildServiceProvider();



            var validationParametersServices = new ServiceCollection();
            validationParametersServices.AddScoped<IValidatorParameters>(p =>
                ActivatorUtilities.CreateInstance<DefaultValidatorParameters>(p, 
                    new object[]
                    {
                        validatorServiceProvider.GetService<ICommandValidator<IVerb>>(),
                        validatorServiceProvider.GetService<ICommandValidator<IOption>>(),
                        validatorServiceProvider.GetService<ICommandValidator<IVerbCollection>>(),
                        validatorServiceProvider.GetService<ICommandValidator<IOptionCollection>>(),
                        validatorServiceProvider.GetService<IAdditionalValidators>()
                    })); 

            var validationParametersServiceProvider = validationParametersServices.BuildServiceProvider();



            var assemblyServices = new ServiceCollection();
            assemblyServices.AddTransient<IAssembly, FakeAssemblyWrapper>();

            var assemblyServiceProvider = assemblyServices.BuildServiceProvider();



            var services = new ServiceCollection();
            services.AddSingleton<TextWriter>(t =>
                    ActivatorUtilities.CreateInstance<StreamWriter>(t, new MemoryStream()))
                .AddScoped<IBuilder, DefaultBuilder>()
                .AddScoped<IValidatorManager>(m =>
                    ActivatorUtilities.CreateInstance<DefaultValidator>(m, 
                        validationParametersServiceProvider.GetService<IValidatorParameters>(),
                        new object[]
                        {
                            assemblyServiceProvider.GetService<IAssembly>()
                        }
                        ))
                .AddScoped<IParser, DefaultParser>()
                .AddScoped<IDisplayer, DefaultDisplayer>();

            var serviceProvider = services.AddLogging(b => b.AddProvider(NullLoggerProvider.Instance))
                                          .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Information)
                                          .BuildServiceProvider();

            var ts = new TestServices(validatorServiceProvider, validationParametersServiceProvider,
                assemblyServiceProvider, serviceProvider);
            
            return ts;
        }
    }
}