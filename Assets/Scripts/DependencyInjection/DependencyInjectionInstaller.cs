using ModestTree;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Universal.CustomAttributes;
using Zenject;

public class DependencyInjectionInstaller : MonoInstaller<DependencyInjectionInstaller>
{
    public override void InstallBindings()
    {
        string[] assembliesNames = new string[] { "UI", "Presentation", "Universal"};

        for (int i = 0; i < assembliesNames.Length; i++)
        {
            Assembly assembly = Assembly.Load(assembliesNames[i]);

            Type[] types = assembly.GetTypes();

            for (int j = 0; j < types.Length; j++)
            {
                Type type = types[j];

                if (type.IsDefined(typeof(CompilerGeneratedAttribute)))
                    return;

                if (type.HasAttribute(typeof(BindAsSingleAttribute)))
                {
                    Container.BindInterfacesAndSelfTo(type).AsSingle();
                }
            }
        }
    }
}
