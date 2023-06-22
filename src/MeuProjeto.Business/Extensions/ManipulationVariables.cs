using Newtonsoft.Json;

namespace MeuProjeto.Business.Extensions
{
    public static class ManipulationVariables
    {
        public static Type VariableClone<Type>(Type objectToCopy)
        {
            if (objectToCopy == null) return objectToCopy;

            return JsonConvert.DeserializeObject<Type>(JsonConvert.SerializeObject(objectToCopy, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }));
        }
    }
}
