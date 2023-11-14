using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace VNet.CommandLineTest
{
    public class TestServices
    {
        public ServiceProvider AssemblyServiceProvider { get; }
        public ServiceProvider ValidatorServiceProvider { get; }
        public ServiceProvider ValidationParameterServiceProvider { get; }
        public ServiceProvider ApplicationServiceProvider { get; }

        public TestServices(ServiceProvider validator, ServiceProvider validationParameter,
            ServiceProvider assembly, ServiceProvider application)
        {
            this.AssemblyServiceProvider = assembly;
            this.ValidatorServiceProvider = validator;
            this.ValidationParameterServiceProvider = validationParameter;
            this.ApplicationServiceProvider = application;
        }
    }
}
