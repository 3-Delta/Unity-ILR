using System;

public static class ManualAdapterRegister
{
    public static void Register(ILRuntime.Runtime.Enviorment.AppDomain domain)
    {
        domain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
        domain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        domain.RegisterCrossBindingAdaptor(new ScriptableObjectAdapter());
        domain.RegisterCrossBindingAdaptor(new ExceptionAdapter());
        domain.RegisterCrossBindingAdaptor(new IEnumerableAdapter());
        domain.RegisterCrossBindingAdaptor(new ILRuntimeDemo.TestClassBaseAdapter());
    }
}
