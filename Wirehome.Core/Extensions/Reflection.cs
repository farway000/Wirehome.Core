﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Wirehome.Core.Extensions
{
    public static class Reflection
    {
        public static List<Type> GetClassesImplementingInterface<TInterface>()
        {
            var interfaceType = typeof(TInterface);
            if (!interfaceType.IsInterface)
            {
                throw new InvalidOperationException();
            }

            var assembly = Assembly.GetExecutingAssembly();

            return assembly.GetTypes()
                .Where(t => t.IsClass && typeof(TInterface).IsAssignableFrom(t)).ToList();            
        }
    }
}
