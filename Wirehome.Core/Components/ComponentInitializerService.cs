﻿using Microsoft.Extensions.Logging;
using System;
using Wirehome.Core.Components.Adapters;
using Wirehome.Core.Components.Logic;
using Wirehome.Core.Contracts;
using Wirehome.Core.Packages;
using Wirehome.Core.Python;

namespace Wirehome.Core.Components
{
    public class ComponentInitializerService : IService
    {
        private readonly PythonScriptHostFactoryService _pythonScriptHostFactoryService;
        private readonly PackageManagerService _packageManagerService;
        private readonly ILogger<ScriptComponentLogic> _scriptComponentLogicLogger;
        private readonly ILogger<ScriptComponentAdapter> _scriptComponentAdapterLogger;
        
        public ComponentInitializerService(
            PythonScriptHostFactoryService pythonScriptHostFactoryService,
            PackageManagerService packageManagerService,
            ILogger<ScriptComponentLogic> scriptComponentLogicLogger,
            ILogger<ScriptComponentAdapter> scriptComponentAdapterLogger)
        {
            _pythonScriptHostFactoryService = pythonScriptHostFactoryService ?? throw new ArgumentNullException(nameof(pythonScriptHostFactoryService));
            _packageManagerService = packageManagerService ?? throw new ArgumentNullException(nameof(packageManagerService));
            _scriptComponentLogicLogger = scriptComponentLogicLogger ?? throw new ArgumentNullException(nameof(scriptComponentLogicLogger));
            _scriptComponentAdapterLogger = scriptComponentAdapterLogger ?? throw new ArgumentNullException(nameof(scriptComponentAdapterLogger));
        }

        public void Start()
        {
        }

        public ComponentInitializer Create(ComponentRegistryService componentRegistryService)
        {
            if (componentRegistryService == null) throw new ArgumentNullException(nameof(componentRegistryService));

            return new ComponentInitializer(
                componentRegistryService, 
                _pythonScriptHostFactoryService, 
                _packageManagerService,
                _scriptComponentLogicLogger,
                _scriptComponentAdapterLogger);
        }
    }
}
