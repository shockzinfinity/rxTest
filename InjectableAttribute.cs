namespace rxTest4
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class InjectableAttribute : Attribute
  {
    private Type _interfaceType { get; set; }
    private string _serviceType { get; set; }

    public InjectableAttribute(Type interfaceType, string serviceType = "singleton")
    {
      _interfaceType = interfaceType;
      _serviceType = serviceType;
    }

    public static void RegisterInjectables(IServiceCollection services)
    {
      var injectableTypes =
        from a in AppDomain.CurrentDomain.GetAssemblies()
        from t in a.GetTypes()
        let attribtes = t.GetCustomAttributes(typeof(InjectableAttribute), true)
        where attribtes != null && attribtes.Length > 0
        select new { Type = t, Attribute = attribtes.Cast<InjectableAttribute>() };

      foreach (var item in injectableTypes) {
        var t = item.Attribute.ElementAt(0);
        switch (t._serviceType) {
          case "transient":
            services.AddTransient(t._interfaceType, item.Type);
            break;

          case "scoped":
            services.AddScoped(t._interfaceType, item.Type);
            break;

          default:
            services.AddSingleton(t._interfaceType, item.Type);
            break;
        }
      }
    }
  }
}