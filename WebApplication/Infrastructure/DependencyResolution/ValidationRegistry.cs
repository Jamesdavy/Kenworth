using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using StructureMap.Configuration.DSL;
using WebApplication.DependencyResolution;

namespace WebApplication.Infrastructure.DependencyResolution
{
    public class ValidationRegistry : Registry
    {
        public ValidationRegistry()
        {
            AssemblyScanner.FindValidatorsInAssemblyContaining<DefaultRegistry>()
                .ForEach(result =>
                {
                    For(result.InterfaceType).Add(result.ValidatorType).Singleton();
                });
        }
    }
}