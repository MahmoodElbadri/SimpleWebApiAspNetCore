using System.ComponentModel;
using System.Dynamic;

namespace SimpleWebApiAspNetCore.Helpers;

public static class DynamicExtensions
{
    public static dynamic ToDynamic(this object value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Input object cannot be null.");
        }

        IDictionary<string, object?> expando = new ExpandoObject();

        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
        {
            var propertyValue = property.GetValue(value);
            expando.Add(property.Name, propertyValue);
        }

        return expando;
    }
}
