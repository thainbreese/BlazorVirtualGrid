using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace BlazorVirtualGrid.App.Services
{
    public static class GridHandler
    {
        //public static GetHandler CreateGetHandler(Type type, PropertyInfo propertyInfo)
        //{
        //    MethodInfo _getMethodInfo = propertyInfo.GetGetMethod(true);
        //    DynamicMethod _dynamicGet = CreateGetDynamicMethod(type);
        //    ILGenerator getGenerator = dynamicGet.GetILGenerator();

        //    _getGenerator.Emit(OpCodes.Ldarg_0);
        //    getGenerator.Emit(OpCodes.Call, getMethodInfo);
        //    BoxIfNeeded(_getMethodInfo.ReturnType, _getGenerator);
        //    _getGenerator.Emit(OpCodes.Ret);

        //    return (GetHandler)_dynamicGet.CreateDelegate(typeof(GetHandler));
        //}

        //private static void BoxIfNeeded(Type type, ILGenerator generator)
        //{
        //    if (type.IsValueType)
        //    {
        //        generator.Emit(OpCodes.Box, type);
        //    }
        //}
    }
}
