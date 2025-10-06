using System;
using System.Linq;
using System.Reflection;
using BepInEx.Configuration;

namespace Space_Duct_Tape
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurablePatchAttribute : Attribute
    {
        public static void Configure(ConfigFile configFile)
        {
            var typ = typeof(ConfigurablePatchAttribute);
            var configureMethods = Assembly.GetCallingAssembly()
                .GetTypes()
                .Where(x => x.IsDefined(typ))
                .Select(x => x.GetMethod(nameof(Configure), BindingFlags.Public | BindingFlags.Static));
            var args = new object[] { configFile };
            foreach (var method in configureMethods)
            {
                method?.Invoke(null, args);
            }
        }
    }
}