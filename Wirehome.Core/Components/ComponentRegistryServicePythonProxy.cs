﻿#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

using IronPython.Runtime;
using System;
using System.Runtime.InteropServices;
using Wirehome.Core.Components.Exceptions;
using Wirehome.Core.Python;

namespace Wirehome.Core.Components
{
    public class ComponentRegistryServicePythonProxy : IInjectedPythonProxy
    {
        readonly ComponentRegistryService _componentRegistryService;

        public ComponentRegistryServicePythonProxy(ComponentRegistryService componentRegistryService)
        {
            _componentRegistryService = componentRegistryService ?? throw new ArgumentNullException(nameof(componentRegistryService));
        }

        public string ModuleName => "component_registry";

        public List get_uids()
        {
            var result = new List();
            foreach (var componentUid in _componentRegistryService.GetComponentUids())
            {
                result.Add(componentUid);
            }

            return result;
        }

        public bool set_tag(string component_uid, string tag)
        {
            return _componentRegistryService.SetComponentTag(component_uid, tag);
        }

        public bool add_tag(string component_uid, string tag)
        {
            return set_tag(component_uid, tag);
        }

        public bool remove_tag(string component_uid, string tag)
        {
            return _componentRegistryService.RemoveComponentTag(component_uid, tag);
        }

        public bool has_tag(string component_uid, string tag)
        {
            return _componentRegistryService.ComponentHasTag(component_uid, tag);
        }

        public bool has_status(string component_uid, string status_uid)
        {
            return _componentRegistryService.ComponentHasStatusValue(component_uid, status_uid);
        }

        public object get_status(string component_uid, string status_uid, [DefaultParameterValue(null)] object default_value)
        {
            return PythonConvert.ToPython(_componentRegistryService.GetComponentStatusValue(component_uid, status_uid, default_value));
        }

        public void set_status(string component_uid, string status_uid, object value)
        {
            _componentRegistryService.SetComponentStatusValue(component_uid, status_uid, PythonConvert.FromPython(value));
        }

        public bool has_setting(string component_uid, string setting_uid)
        {
            return _componentRegistryService.ComponentHasSetting(component_uid, setting_uid);
        }

        public object get_setting(string component_uid, string setting_uid, [DefaultParameterValue(null)] object default_value)
        {
            return PythonConvert.ToPython(_componentRegistryService.GetComponentSetting(component_uid, setting_uid, default_value));
        }

        public void register_setting(string component_uid, string setting_uid, object value)
        {
            _componentRegistryService.RegisterComponentSetting(component_uid, setting_uid, PythonConvert.FromPython(value));
        }

        public void set_setting(string component_uid, string setting_uid, object value)
        {
            _componentRegistryService.SetComponentSetting(component_uid, setting_uid, PythonConvert.FromPython(value));
        }

        [Obsolete]
        public PythonDictionary execute_command(string component_uid, PythonDictionary message)
        {
            return process_message(component_uid, message);
        }

        public PythonDictionary process_message(string component_uid, PythonDictionary message)
        {
            try
            {
                return PythonConvert.ToPythonDictionary(_componentRegistryService.ProcessComponentMessage(component_uid, message));
            }
            catch (ComponentNotFoundException)
            {
                return new PythonDictionary
                {
                    ["type"] = "exception.component_not_found",
                    ["component_uid"] = component_uid
                };
            }
        }
    }
}