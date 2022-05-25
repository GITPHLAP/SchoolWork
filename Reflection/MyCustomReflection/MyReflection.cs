using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomReflection
{
    public class MyReflection
    {
        public Type GetType(string className, string _namespace, string assemblyName)
        {
            //Get all types in namespace
            var types = Assembly.LoadFrom(assemblyName).GetTypes();
            Console.WriteLine("Types in the executing assembly");
            foreach (var item in types)
            {
                Console.WriteLine(item.FullName);

                if (item.FullName == $"{_namespace}.{className}")
                {
                    return item;
                }


            }
            return null;
            
        }

        public void SearchForEverything(string assemblyName)
        {
            //Get all types in namespace
            var types = Assembly.LoadFrom(assemblyName).GetTypes();
            Console.WriteLine($"Types in the {assemblyName} assembly");
            Console.WriteLine();

            foreach (var item in types)
            {
                Console.Write(item.FullName);

                #region What is the type if case
                if (item.IsPublic)
                {
                    Console.Write(" - Public");
                }
                if (item.IsNotPublic)
                {
                    Console.Write(" - NotPublic");
                }
                if (item.IsAbstract)
                {
                    Console.Write(" - Abstract");
                }
                if (item.IsClass)
                {
                    Console.Write(" - Class");
                }
                if (item.IsInterface)
                {
                    Console.Write(" - Interface");
                }
                if (item.IsEnum)
                {
                    Console.Write(" - Enum");
                }
                if (item.IsNested)
                {
                    Console.Write(" - Nested");
                }
                #endregion
                Console.WriteLine();
                //Get memberinfos
                MemberInfo[] memberInfo = item.GetMembers(BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public);

                Console.WriteLine("    MemberInfos");

                foreach (var member in memberInfo)
                {
                    Console.WriteLine($"        Name: {member.Name}  MemberType: {member.MemberType}  ToString(): {member.ToString()}");
                }


                //Get methodsinfo 
                MethodInfo[] methodInfos = item.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                Console.WriteLine("    MethodInfos");

                foreach (var method in methodInfos)
                {
                    var _params = method.GetParameters();

                    Console.Write("        " + method.Name);
                    #region What is the type if case
                    if (method.IsPublic)
                    {
                        Console.Write(" - Public");
                    }
                    if (method.IsPrivate)
                    {
                        Console.Write(" - NotPublic");
                    }
                    if (method.IsAbstract)
                    {
                        Console.Write(" - Abstract");
                    }
                    if (method.IsStatic)
                    {
                        Console.Write(" - Class");
                    }
                    if (method.IsVirtual)
                    {
                        Console.Write(" - Interface");
                    }
                    #endregion
                    if (_params.Length != 0)
                    {
                        Console.WriteLine(" Parameters");
                        foreach (var param in _params)
                        {
                            Console.WriteLine($"            {param.Name} type: {param.ParameterType}");
                        }
                    }
                    Console.WriteLine();

                }

                //Get constructor info 
                ConstructorInfo[] constructorInfos = item.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                Console.WriteLine("    ConstructorInfos");

                foreach (var constructor in constructorInfos)
                {
                    var _params = constructor.GetParameters();
                    Console.Write("        " + constructor.Name);
                    #region What is the type if case
                    if (constructor.IsPublic)
                    {
                        Console.Write(" - Public");
                    }
                    if (constructor.IsPrivate)
                    {
                        Console.Write(" - NotPublic");
                    }
                    if (constructor.IsAbstract)
                    {
                        Console.Write(" - Abstract");
                    }
                    if (constructor.IsStatic)
                    {
                        Console.Write(" - Class");
                    }
                    if (constructor.IsVirtual)
                    {
                        Console.Write(" - Interface");
                    }
                    #endregion
                    if (_params.Length != 0)
                    {
                        Console.WriteLine(" Parameters");
                        foreach (var param in _params)
                        {
                            Console.WriteLine($"            {param.Name} type: {param.ParameterType}");
                        }
                    }
                    Console.WriteLine();

                }

                //Get fields info 
                FieldInfo[] fieldsInfos = item.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                Console.WriteLine("    FieldInfos");

                foreach (var fieldInfo in fieldsInfos)
                {
                    Console.Write("        " + fieldInfo.Name);
                    #region What is the type if case
                    if (fieldInfo.IsPublic)
                    {
                        Console.Write(" - Public");
                    }
                    if (fieldInfo.IsPrivate)
                    {
                        Console.Write(" - NotPublic");
                    }
                    if (fieldInfo.IsStatic)
                    {
                        Console.Write(" - Class");
                    }
                    #endregion
                    Console.WriteLine();

                }

                //Get properties info 
                PropertyInfo[] propertyInfos = item.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                Console.WriteLine("    PropertyInfos");

                foreach (var propertyinfo in propertyInfos)
                {
                    Console.WriteLine("        " + propertyinfo.Name);
                }


                Console.WriteLine("----------------------------------------------------------");
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
            return type.InvokeMember(className, BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.CreateInstance, null, null, args);
        }

        public MethodInfo GetMethodInfo(Type type, string methodName)
        {
            return type.GetMethod(methodName, BindingFlags.Instance | 
                BindingFlags.NonPublic |
                BindingFlags.Public);
        }

        public T InvokeMethod<T>(Type type, string methodName, object[] argValues, object obj = null)
        {
            MethodInfo method = GetMethodInfo(type, methodName);
            var result = method.Invoke(obj, BindingFlags.InvokeMethod, null,
                argValues, null);


            return (T)Convert.ChangeType(result, typeof(T));
        }

        public void InvokeMethod(Type type, string methodName, object[] argValues, object obj = null)
        {

            MethodInfo method = GetMethodInfo(type, methodName);
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
