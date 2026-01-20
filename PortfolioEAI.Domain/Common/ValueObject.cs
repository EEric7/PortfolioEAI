using System.Collections;

namespace PortfolioEAI.Domain.Common
{
    public abstract class ValueObject
    {
        /// <summary>
        /// When implemented in derived classes, this method should return the components
        /// that are used to determine equality for the value object.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object?> GetEqualityComponents();

        /// <summary>
        /// Determines whether the specified object is equal to the current value object.  
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return GetEqualityComponents()
                .SequenceEqual(((ValueObject)obj).GetEqualityComponents());
        }

        /// <summary>
        /// Computes the hash code for a given component of the value object.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        private static int ComputeHash(object? component)
        {
            if (component is null) return 0;

            if (component is IEnumerable enumerable && component is not string)
            {
                unchecked
                {
                    int hash = 19;
                    foreach (var item in enumerable)
                        hash = (hash * 31) + (item?.GetHashCode() ?? 0);
                    return hash;
                }
            }
            return component.GetHashCode();
        }

        /// <summary>
        /// Returns a hash code for the value object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                foreach (var component in GetEqualityComponents())
                    hash = (hash * 23) + ComputeHash(component);
                return hash;
            }
        }

        /// <summary>
        /// Determines whether two value objects are equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(ValueObject? a, ValueObject? b)
            => a is null ? b is null : a.Equals(b);

        /// <summary>
        /// Determines whether two value objects are not equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(ValueObject? a, ValueObject? b)
            => !(a == b);

        // TODO: Improve the ToString method to better represent the value object
        public override string ToString()
        {
            var components = GetEqualityComponents().Select(c => c?.ToString() ?? "null");
            return $"{GetType().Name} [{string.Join(", ", components)}]";
        }
    }
}