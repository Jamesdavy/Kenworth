// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Data.Entity;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StructureMap;
using StructureMap.Pipeline;
using StructureMap.Web;
using StructureMap.Web.Pipeline;
using WebApplication.Infrastructure.Logging;
using WebApplication.Infrastructure.Nlog;
using WebApplication.Infrastructure.Services;
using WebApplication.Models;
using WebApplication.Models.DatabaseFirst;
using StructureMap.Diagnostics;

namespace WebApplication.DependencyResolution {
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
	
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    //scan.TheCallingAssembly();
                    //scan.WithDefaultConventions();
					scan.With(new ControllerConvention());
                });

            For<ApplicationEntities>().HttpContextScoped().Use(() => new ApplicationEntities());
            For<ContextFactory>().Use<ContextFactory>();
            For<IOrderReferenceNumberLookupService>().Use<OrderReferenceNumberLookupService>();
            //For(typeof(ApplicationEntities)).HttpContextScoped();
            //For<ApplicationDbContext>.HttpContextScoped();
            For<ILogger>().Use<NLogLogger>();

            

        }

        #endregion
    }
}