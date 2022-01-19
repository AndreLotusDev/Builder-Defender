using System;
using System.ComponentModel;
using System.Reflection;

public static class EnumExtension {

    public static string DescriptionOfEnum<T>(this T source) where T : IConvertible
    {
        FieldInfo fi = source.GetType().GetField(source.ToString());

        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0) return attributes[0].Description;
        else return source.ToString();
    }

}
