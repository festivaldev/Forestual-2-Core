#pragma warning disable 1591

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2Core.Command
{
    public class Parameter<T> : IParameter where T : IConvertible
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public ParameterType Type { get; set; }

        public dynamic GetDefault() {
            return default(T);
        }

        public bool TryParse(string content, out T value) {
            if (typeof(T) == typeof(string)) {
                value = (T) Convert.ChangeType(content, typeof(T));
                return true;
            }
            if (typeof(T) == typeof(int)) {
                int Temp;
                var Result = int.TryParse(content, out Temp);
                value = Result ? (T) Convert.ChangeType(content, typeof(T)) : default(T);
                return Result;
            }
            if (typeof(T) == typeof(double)) {
                double Temp;
                var Result = double.TryParse(content, out Temp);
                value = Result ? (T) Convert.ChangeType(content, typeof(T)) : default(T);
                return Result;
            }
            if (typeof(T) == typeof(bool)) {
                bool Temp;
                var Result = bool.TryParse(content, out Temp);
                value = Result ? (T) Convert.ChangeType(content, typeof(T)) : default(T);
                return Result;
            }
            value = default(T);
            return false;
        }
    }
}
