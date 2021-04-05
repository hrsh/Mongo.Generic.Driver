using System;

namespace Mongo.Generic.Driver.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DocumentNameAttribute : Attribute
    {
        public DocumentNameAttribute(string documentName)
        {
        }
    }
}
