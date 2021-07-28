using System;
using System.Reflection;
using UnityEngine;

public class ForeachClass
{
    /// <summary>
    /// C#反射遍历对象属性
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="model">对象</param>
    public static void ForeachClassProperties<T>(T model)
    {
        Debug.Log("FFFFF~~~");
        Type t = model.GetType();
        PropertyInfo[] PropertyList = t.GetProperties();

        
        foreach (PropertyInfo item in PropertyList)
        {
            string name = item.Name;
            object value = item.GetValue(model, null);

            Debug.Log("FFFFF:" + name + "," + value);
        }
    }
}