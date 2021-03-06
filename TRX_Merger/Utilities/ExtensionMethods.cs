﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TRX_Merger.Utilities
{
    public static class ExtensionMethods
    {
        public static void SetDefaultXmlNamespace(this XElement xelem, XNamespace xmlns)
        {
            if (xelem.Name.NamespaceName == string.Empty)
                xelem.Name = xmlns + xelem.Name.LocalName;
            foreach (var e in xelem.Elements())
                e.SetDefaultXmlNamespace(xmlns);
        }

        public static string GetAttributeValue(this XElement elem, string attribute)
        {
            if (elem.Attribute(attribute) != null)
                return elem.Attribute(attribute).Value;
            return null;
        }

        public static T ToObject<T>(this IDictionary<string, int> source)
        where T : class, new()
        {
            T someObject = new T();
            Type someObjectType = someObject.GetType();

            foreach (KeyValuePair<string, int> item in source)
            {
                var property = item.Key.First().ToString().ToUpper() + item.Key.Substring(1);
                someObjectType.GetProperty(property).SetValue(someObject, item.Value, null);
            }

            return someObject;
        }
    }
}
