using System.Reflection;

namespace Net7.WebApi.Template.Enums
{
    public class EnumerationBase : IComparable
    {
        protected EnumerationBase(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; private set; }
        public int Id { get; private set; }

        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(EnumerationBase enumeration1, EnumerationBase enumeration2)
        {
            return enumeration1.Id == enumeration2.Id;
        }

        public static bool operator !=(EnumerationBase enumeration1, EnumerationBase enumeration2)
        {
            return enumeration1.Id != enumeration2.Id;
        }

        public static T GetById<T>(int Id) where T : EnumerationBase
        {
            return typeof(T).GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly)
                .Select(m => m.GetValue(null))
                .Cast<T>()
                .Where(m => m.Id == Id)
                .SingleOrDefault();
        }

        public static T GetByName<T>(string name) where T : EnumerationBase
        {
            return typeof(T).GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly)
                .Select(m => m.GetValue(null))
                .Cast<T>()
                .Where(m => m.Name == name)
                .SingleOrDefault();
        }

        public static IEnumerable<T> GetAll<T>() where T : EnumerationBase
        {
            return typeof(T).GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly)
                .Select(m => m.GetValue(null))
                .Cast<T>();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(EnumerationBase))
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(((EnumerationBase)obj).Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}