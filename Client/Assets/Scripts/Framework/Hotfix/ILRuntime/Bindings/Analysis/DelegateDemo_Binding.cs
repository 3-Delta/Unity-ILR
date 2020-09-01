using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class DelegateDemo_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::DelegateDemo);
            args = new Type[]{typeof(System.Action<System.Int32>)};
            method = type.GetMethod("add_onLoaded", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, add_onLoaded_0);
            args = new Type[]{typeof(System.Action<System.Int32>)};
            method = type.GetMethod("RemoveLoadedListener", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, RemoveLoadedListener_1);
            args = new Type[]{typeof(System.Action<System.Int32>)};
            method = type.GetMethod("AddLoadedListener", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, AddLoadedListener_2);

            field = type.GetField("TestMethodDelegate", flag);
            app.RegisterCLRFieldGetter(field, get_TestMethodDelegate_0);
            app.RegisterCLRFieldSetter(field, set_TestMethodDelegate_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_TestMethodDelegate_0, AssignFromStack_TestMethodDelegate_0);
            field = type.GetField("TestFunctionDelegate", flag);
            app.RegisterCLRFieldGetter(field, get_TestFunctionDelegate_1);
            app.RegisterCLRFieldSetter(field, set_TestFunctionDelegate_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_TestFunctionDelegate_1, AssignFromStack_TestFunctionDelegate_1);
            field = type.GetField("TestActionDelegate", flag);
            app.RegisterCLRFieldGetter(field, get_TestActionDelegate_2);
            app.RegisterCLRFieldSetter(field, set_TestActionDelegate_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_TestActionDelegate_2, AssignFromStack_TestActionDelegate_2);


        }


        static StackObject* add_onLoaded_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.Int32> @value = (System.Action<System.Int32>)typeof(System.Action<System.Int32>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            global::DelegateDemo.onLoaded += value;

            return __ret;
        }

        static StackObject* RemoveLoadedListener_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.Int32> @action = (System.Action<System.Int32>)typeof(System.Action<System.Int32>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            global::DelegateDemo.RemoveLoadedListener(@action);

            return __ret;
        }

        static StackObject* AddLoadedListener_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.Int32> @action = (System.Action<System.Int32>)typeof(System.Action<System.Int32>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            global::DelegateDemo.AddLoadedListener(@action);

            return __ret;
        }


        static object get_TestMethodDelegate_0(ref object o)
        {
            return global::DelegateDemo.TestMethodDelegate;
        }

        static StackObject* CopyToStack_TestMethodDelegate_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::DelegateDemo.TestMethodDelegate;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_TestMethodDelegate_0(ref object o, object v)
        {
            global::DelegateDemo.TestMethodDelegate = (global::TestDelegateMethod)v;
        }

        static StackObject* AssignFromStack_TestMethodDelegate_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            global::TestDelegateMethod @TestMethodDelegate = (global::TestDelegateMethod)typeof(global::TestDelegateMethod).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            global::DelegateDemo.TestMethodDelegate = @TestMethodDelegate;
            return ptr_of_this_method;
        }

        static object get_TestFunctionDelegate_1(ref object o)
        {
            return global::DelegateDemo.TestFunctionDelegate;
        }

        static StackObject* CopyToStack_TestFunctionDelegate_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::DelegateDemo.TestFunctionDelegate;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_TestFunctionDelegate_1(ref object o, object v)
        {
            global::DelegateDemo.TestFunctionDelegate = (global::TestDelegateFunction)v;
        }

        static StackObject* AssignFromStack_TestFunctionDelegate_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            global::TestDelegateFunction @TestFunctionDelegate = (global::TestDelegateFunction)typeof(global::TestDelegateFunction).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            global::DelegateDemo.TestFunctionDelegate = @TestFunctionDelegate;
            return ptr_of_this_method;
        }

        static object get_TestActionDelegate_2(ref object o)
        {
            return global::DelegateDemo.TestActionDelegate;
        }

        static StackObject* CopyToStack_TestActionDelegate_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = global::DelegateDemo.TestActionDelegate;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_TestActionDelegate_2(ref object o, object v)
        {
            global::DelegateDemo.TestActionDelegate = (System.Action<System.String>)v;
        }

        static StackObject* AssignFromStack_TestActionDelegate_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Action<System.String> @TestActionDelegate = (System.Action<System.String>)typeof(System.Action<System.String>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            global::DelegateDemo.TestActionDelegate = @TestActionDelegate;
            return ptr_of_this_method;
        }



    }
}
