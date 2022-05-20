using ReflectionConsole.ReflectionLogic;
using System;
using System.Reflection;

namespace ReflectionConsole
{
    internal class Program
    {
        static void Main(string[] args)
{

            MyReflection reflection = new MyReflection();

            reflection.SearchForEverything();
            //string objFullName = "";
            //string objName = "";

            ////Get all types in namespace
            //var types = Assembly.GetExecutingAssembly().GetTypes();
            //Console.WriteLine("Types in the executing assembly");
            //foreach (var item in types)
            //{
            //    Console.WriteLine(item.FullName);

            //    if (item.Name == "Person")
            //    {
            //        objFullName = item.FullName;
            //        objName = item.Name;
            //    }
            //}

            ////If namespace dont return the wanted class name
            //if (string.IsNullOrEmpty(objFullName))
            //{
            //    return;
            //}

            ////Get type 
            //Type personType = Type.GetType(objFullName);


            ////Get memberinfos
            //MemberInfo[] memberInfo = personType.GetMembers(BindingFlags.Instance | BindingFlags.NonPublic |
            //BindingFlags.Public);
            //Console.WriteLine("MemberInfos");

            //foreach (var item in memberInfo)
            //{
            //    Console.WriteLine($"Name: {item.Name}  MemberType: {item.MemberType}  ToString(): {item.ToString()}");


            //}


            ////Get methodsinfo 
            //MethodInfo[] methodInfos = personType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            //Console.WriteLine("MethodInfos");

            //foreach (var method in methodInfos)
            //{
            //    var _params = method.GetParameters();
            //    Console.Write(method.Name + " Parameters");
            //    foreach (var param in _params)
            //    {
            //        Console.Write($"    {param.Name} type: {param.ParameterType}");
            //    }
            //    Console.WriteLine();
            //}

            ////Get constructor info 
            //ConstructorInfo[] constructorInfos = personType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            //Console.WriteLine("ConstructorInfos");

            //foreach (var constructor in constructorInfos)
            //{
            //    var _params = constructor.GetParameters();
            //    Console.Write(constructor.Name + " Parameters");
            //    foreach (var param in _params)
            //    {
            //        Console.Write($"    {param.Name} type: {param.ParameterType}");
            //    }
            //    Console.WriteLine();
            //}


            ////Creating an instance of object
            //object myObj = personType.InvokeMember("Person", BindingFlags.Public |
            //    BindingFlags.Instance | BindingFlags.CreateInstance, null, null, new object[] { 20, "Men" });



        }
    }
}
