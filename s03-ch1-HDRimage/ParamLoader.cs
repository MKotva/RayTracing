using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
  static class ParamLoader
  {
    public static Dictionary<string, string> ParseInput(string line)
    {
      Dictionary<string, string> paramValuePairs = new Dictionary<string, string>();

      string[] operations = line.Split(';');
      foreach(string operation in operations) 
      {
        string[] pair = operation.Split('=');
        if(pair.Length == 2)
        {
          paramValuePairs.Add(pair[0], pair[1]);
        }
      }
      return paramValuePairs;
    }

    public static void Load<T>(T instance, Dictionary<string, string> paramValuePairs)
    {    
      var parameters = paramValuePairs.Keys.ToList();

      foreach(var param in parameters)
      {
        PropertyInfo pi = instance.GetType().GetProperty(param);
        if (pi != null)
        {
          SetPropertyValue<T>(instance, pi, paramValuePairs[param]);
        }
      }
    }


    private static void SetPropertyValue<T>(T instance, PropertyInfo propertyInfo, string value)
    {
      if (propertyInfo.PropertyType == typeof(string))
      {
        propertyInfo.SetValue(instance, value);
      }
      else if (propertyInfo.PropertyType == typeof(int))
      {
        int converted;
        if(int.TryParse(value, out converted))
          propertyInfo.SetValue(instance, converted);
      }
      else if (propertyInfo.PropertyType == typeof(double))
      {
        double converted;
        if (double.TryParse(value, out converted) ||
          double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out converted))
          propertyInfo.SetValue(instance, converted);
      }
      else if (propertyInfo.PropertyType == typeof(bool))
      {
        bool converted;
        if (bool.TryParse(value, out converted))
          propertyInfo.SetValue(instance, converted);
      }
    }
  }
}
