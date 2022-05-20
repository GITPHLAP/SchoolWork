using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionConsole.ReflectionLogic
{
    public class MyReflection
    {

        public Type GetType(string className, string _namespace, string assemblyName)
        {
            string objFullName = "";
            //Get all types in namespace
            var types = Assembly.LoadFrom(assemblyName).GetTypes();
            Console.WriteLine("Types in the executing assembly");
            foreach (var item in types)
            {
                Console.WriteLine(item.FullName);

                if (item.FullName == $"{_namespace}.{className}")
                {
                    objFullName = item.FullName;
                }


            }

            //If namespace dont return the wanted class name
            if (string.IsNullOrEmpty(objFullName))
            {
                return null;
            }

            //Get type 
            return Type.GetType(objFullName);
        }

        public void SearchForEverything()
        {
            //Get type 
            Type personType = GetType("Person", "PrivateMethods", "ReflectionConsole.dll");


            //Get memberinfos
            MemberInfo[] memberInfo = personType.GetMembers(BindingFlags.Instance | BindingFlags.NonPublic |
            BindingFlags.Public);
            Console.WriteLine("MemberInfos");

            foreach (var item in memberInfo)
            {
                Console.WriteLine($"Name: {item.Name}  MemberType: {item.MemberType}  ToString(): {item.ToString()}");


            }


            //Get methodsinfo 
            MethodInfo[] methodInfos = personType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("MethodInfos");

            foreach (var method in methodInfos)
            {
                var _params = method.GetParameters();
                Console.Write(method.Name + " Parameters");
                foreach (var param in _params)
                {
                    Console.Write($"    {param.Name} type: {param.ParameterType}");
                }
                Console.WriteLine();
            }

            //Get constructor info 
            ConstructorInfo[] constructorInfos = personType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("ConstructorInfos");

            foreach (var constructor in constructorInfos)
            {
                var _params = constructor.GetParameters();
                Console.Write(constructor.Name + " Parameters");
                foreach (var param in _params)
                {
                    Console.Write($"    {param.Name} type: {param.ParameterType}");
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Create an instance of the type with the constructor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="className">Type name</param>
        /// <param name="args">constructor args</param>
        /// <returns>Instance of the type</returns>
        public object CreateInstanceOfType(Type type, string className, object[]? args)
        {
            if (args == null)
            {
                args = new object[] { };
            }
            //Creating an instance of object
            return type.InvokeMember(className, BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.CreateInstance, null, null, args);
        }

        public T InvokeMethod<T>(Type type, string methodName, object[] argValues, object obj = null)
        {
            MethodInfo method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic |
            BindingFlags.Public);
            var result = method.Invoke(obj, BindingFlags.InvokeMethod, null,
                argValues, null);


            return (T)Convert.ChangeType(result, typeof(T));
        }

        public void InvokeMethod(Type type, string methodName, object[] argValues, object obj = null)
        {

            MethodInfo method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic |
            BindingFlags.Public);
            method.Invoke(obj, BindingFlags.InvokeMethod, null,
                argValues, null);
        }

        public string[] GetMethodsParameterNames(Type type, string methodName)
        {
            //Get methodsinfo 
            MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (methodInfo == null)
            {
                return null;
            }
            List<string> parameterNames = new List<string>();
            var _params = methodInfo.GetParameters();
            foreach (var param in _params)
            {
                parameterNames.Add(param.Name);
            }
            return parameterNames.ToArray();
        }

        public T GetFieldValue<T>(Type type, string fieldName, object obj)
        {
            FieldInfo field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            return (T)Convert.ChangeType(field.GetValue(obj), field.FieldType);
        }

        public T GetPropertyValue<T>(Type type, string propName, object obj)
        {
            PropertyInfo prop = type.GetProperty(propName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            return (T)Convert.ChangeType(prop.GetValue(obj), prop.PropertyType);
        }
    }
}

