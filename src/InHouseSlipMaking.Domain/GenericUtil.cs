using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Data.Linq.Mapping;

namespace InHouseSlipMaking.Domain
{
    public static class GenericUtil
    {
        public static void DeepCopy<T>(this T source, out T dest)
        {
            dest = default(T);

            StringWriter sw = new StringWriter();
            XmlSerializer ser = new XmlSerializer(typeof(T));

            ser.Serialize(sw, source);

            StringReader sr = new StringReader(sw.ToString());

            ser = new XmlSerializer(typeof(T));
            dest = (T)ser.Deserialize(sr);
        }

        public static string Serialize<T>(this T t, string strPrefix, string strNamespace)
        {
            string strReturn = "";
            try
            {
                StringWriter sw = new StringWriter();
                XmlSerializerNamespaces namespaceManager = new XmlSerializerNamespaces();
                XmlSerializer ser;

                if (!string.IsNullOrEmpty(strNamespace))
                {
                    namespaceManager.Add(strPrefix, strNamespace);
                    ser = new XmlSerializer(typeof(T), strNamespace);
                    ser.Serialize(sw, t, namespaceManager);
                }
                else
                {
                    ser = new XmlSerializer(typeof(T));
                    ser.Serialize(sw, t);
                }

                strReturn = sw.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strReturn;
        }

        public static T DeSerialize<T>(this string s, string strNamespace)
        {
            T t = default(T);

            try
            {
                StringReader sr = new StringReader(s);
                XmlSerializer ser;
                if (!string.IsNullOrEmpty(strNamespace))
                {
                    ser = new XmlSerializer(typeof(T), strNamespace);
                }
                else
                {
                    ser = new XmlSerializer(typeof(T));
                }

                t = (T)ser.Deserialize(sr);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return t;

        }

        public static string ToString<T>(this T t)
        {
            StringBuilder sb = new StringBuilder();

            PropertyInfo[] propInfos = typeof(T).GetProperties();

            foreach (PropertyInfo pi in propInfos)
            {
                sb.AppendFormat("{0} : {1}\n", pi.Name, pi.GetValue(t, null) == null ? "None" : pi.GetValue(t, null).ToString());
            }

            return sb.ToString();
        }

        public static int GetHashCode<T>(this T t)
        {
            if (t == null)
                return -1;

            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propInfos = typeof(T).GetProperties();

            foreach (PropertyInfo pi in propInfos)
            {
                sb.AppendFormat("{0}{1}", pi.Name, pi.GetValue(t, null) == null ? string.Empty : pi.GetValue(t, null).ToString());
            }

            return sb.ToString().GetHashCode();
        }

        public static void Convert<T1, T2>(this T1 source, out T2 dest)
        {
            string strSource = Serialize<T1>(source, string.Empty, string.Empty);

            string ns = GetNameSpaceOfObject<T1>(source);

            dest = DeSerialize<T2>(strSource, ns);
        }

        private static string GetNameSpaceOfObject<T>(this T t)
        {
            string strNamespace = string.Empty;

            object[] objList = t.GetType().GetCustomAttributes(false);

            foreach (object obj in objList)
            {
                if (obj is System.Xml.Serialization.XmlRootAttribute)
                {
                    strNamespace = ((System.Xml.Serialization.XmlRootAttribute)obj).Namespace;

                    break;
                }
            }

            return strNamespace;
        }

        public static void Detach<TEntity>(this TEntity entity)
        {
            foreach (FieldInfo fi in entity.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {

                if (fi.FieldType.ToString().Contains("EntityRef"))
                {

                    var value = fi.GetValue(entity);

                    if (value != null)
                    {
                        fi.SetValue(entity, null);
                    }
                }

                if (fi.FieldType.ToString().Contains("EntitySet"))
                {
                    var value = fi.GetValue(entity);

                    if (value != null)
                    {
                        MethodInfo mi = value.GetType().GetMethod("Clear");

                        if (mi != null)
                        {
                            mi.Invoke(value, null);
                        }

                        fi.SetValue(entity, value);
                    }
                }

            }

        }

        public static void ShallowCopy<T>(this T source, out T dest)
        {
            PropertyInfo[] sourcePropInfos = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] destinationPropInfos = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // create an object to copy values into
            Type entityType = source.GetType();
            dest = (T)(Activator.CreateInstance(entityType));

            foreach (PropertyInfo sourcePropInfo in sourcePropInfos)
            {
                if (Attribute.GetCustomAttribute(sourcePropInfo, typeof(ColumnAttribute), false) != null)
                {
                    PropertyInfo destPropInfo = destinationPropInfos.Where(pi => pi.Name == sourcePropInfo.Name).First();
                    destPropInfo.SetValue(dest, sourcePropInfo.GetValue(source, null), null);
                }
            }

        }

        public static void ShallowCopy<T>(this T source, T dest)
        {
            PropertyInfo[] sourcePropInfos = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] destinationPropInfos = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // create an object to copy values into
            //Type entityType = source.GetType();
            //dest = (T)(Activator.CreateInstance(entityType));

            foreach (PropertyInfo sourcePropInfo in sourcePropInfos)
            {
                if (Attribute.GetCustomAttribute(sourcePropInfo, typeof(ColumnAttribute), false) != null)
                {
                    PropertyInfo destPropInfo = destinationPropInfos.Where(pi => pi.Name == sourcePropInfo.Name).First();
                    if (!destPropInfo.PropertyType.FullName.Equals("System.Data.Linq.Binary"))
                    {
                        destPropInfo.SetValue(dest, sourcePropInfo.GetValue(source, null), null);
                    }
                }
            }

        }

        public static string Dump(this IList list)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in list)
            {
                sb.AppendLine();

                sb.AppendLine(item.Dump());

                sb.AppendLine("---------------------------------------------------");

                sb.AppendLine();

            }

            return sb.ToString();
        }

        public static string Dump(this object obj)
        {
            StringBuilder sb = new StringBuilder();

            PropertyInfo[] properties = obj.GetType().GetProperties();

            foreach (PropertyInfo p in properties)
            {
                try
                {
                    sb.AppendLine();
                    sb.AppendFormat(string.Format("{0} : {1}", p.Name, p.GetValue(obj, null)));
                    sb.AppendLine();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return sb.ToString();
        }
    }
}
