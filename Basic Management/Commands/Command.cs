using System;
using System.Linq;
using System.Reflection;
using Basic_Management.Utils;

namespace Basic_Management.Commands
{
    public abstract class Command
    {
        public string BaseCommand { get; }
        public AccessType AccessType { get; }
        public string Description { get; }
        public TypeCode[] Codes { get; }
        
        protected Command(string baseCommand, AccessType accessType, string description = "Im a command!", params TypeCode[] codes)
        {
            BaseCommand = baseCommand;
            AccessType = accessType;
            Description = description;
            Codes = codes;
        }

        public virtual FailType CanExecute(params string[] args)
        {
            if (UserManager.LoggedIn.CanAccess(AccessType) != true) return FailType.PERMISSION;
            if (Codes.Length == 0) return FailType.NONE;
            if (Codes.Length != args.Length) return FailType.ARGUMENT;

            var a = Codes.Where((t, i) => TestTypeCode(args[i], t)).Count();

            return a == Codes.Length ? FailType.NONE : FailType.ARGUMENT;
        }

        /// <summary>
        /// Tests and returns whether or not the string can be parsed as the typecode or not
        /// </summary>
        /// <param name="s">
        /// The string we want to test.
        /// </param>
        /// <param name="t">
        /// The TypeCode you want to find.
        /// </param>
        /// <returns>True if <paramref name="s"/> can be parsed as <paramref name="t"/> else False</returns>
        /// <exception cref="MissingMethodException">If the method isn't found for some reason</exception>
        private bool TestTypeCode(string s, TypeCode t)
        {
            if (t == TypeCode.String)
                return true;
            // Create the type that we will be trying to parse.
            Type type = Type.GetType("System." + t);
            
            // Find the first TryParse value
            MethodInfo methodInfo = type?.GetMethods().Where(g => g.Name == "TryParse").ToArray()[0];
            
            // Create an instance of the type found from the TypeCode for the returning type.
            object v = Activator.CreateInstance(type ?? throw new NullReferenceException());
            
            // If the method isn't found throw an exception!
            if (methodInfo == null) throw new MissingMethodException();
            
            // Preparing the parameters for the TryParse.
            object[] parametersArray = {s, v};
            
            // Invoke the method and return the value.
            return (bool) methodInfo.Invoke(null, parametersArray);
        }
        public abstract void RunCommand(string[] args);
    }
}