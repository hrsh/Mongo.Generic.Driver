using System;
using System.Linq;
using System.Reflection;

namespace Mongo.Generic.Driver.Core
{
    public static class DocumentHelper
    {
        public static string NameAttr<T>(this Type source)
        {
            var typeInfo = source.GetTypeInfo();
            var docInfo = source
                .CustomAttributes
                .FirstOrDefault(a => a.AttributeType == typeof(T))?
                .ConstructorArguments?.
                FirstOrDefault();
            if (docInfo is null) throw new NullReferenceException();
            var document = docInfo?.Value.ToString();

            return document;
        }
    }
}
