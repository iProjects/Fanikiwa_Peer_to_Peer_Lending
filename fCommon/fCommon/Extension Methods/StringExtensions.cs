using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Web.UI;

namespace fCommon.Extension_Methods
{
    public static class StringExtensions
    {

        #region Extension Methods
        public static string Between(this string src, string findfrom,
                             params string[] findto)
        {
            int start = src.IndexOf(findfrom);
            if (start < 0) return "";
            foreach (string sto in findto)
            {
                int to = src.IndexOf(sto, start + findfrom.Length);
                if (to >= 0) return
                    src.Substring(
                               start + findfrom.Length,
                               to - start - findfrom.Length);
            }
            return "";
        }

        public static string FormatWith(this string format, object source)
        {
            return FormatWith(format, null, source);
        }

        public static string FormatWith(this string format, IFormatProvider provider, object source)
        {
            if (format == null)
                throw new ArgumentNullException("format");
            Regex r = new Regex(@"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+",
              RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            List<object> values = new List<object>();
            string rewrittenFormat = r.Replace(format, delegate(Match m)
            {
                Group startGroup = m.Groups["start"];
                Group propertyGroup = m.Groups["property"];
                Group formatGroup = m.Groups["format"];
                Group endGroup = m.Groups["end"];
                values.Add((propertyGroup.Value == "0")
                  ? source
                  : DataBinder.Eval(source, propertyGroup.Value));
                return new string('{', startGroup.Captures.Count) + (values.Count - 1) + formatGroup.Value
                 + new string('}', endGroup.Captures.Count);
            });

            return string.Format(provider, rewrittenFormat, values.ToArray());

        }

        public static bool IsNumeric(this string s)
        {
            return Regex.IsMatch(s, @"^\d+$");
        }

        public static bool IsEmail(this string s)
        {
            return Regex.IsMatch(s, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            //Utility.RegexUtilities regex = new Utility.RegexUtilities();
            //return regex.IsValidEmail(s);
        }
    }

    public static class ObjectExtensions
    {
        public static IDictionary<string, object> ToDictionary(this object o)
        {
            if (o == null) return new Dictionary<string, object>();

            return TypeDescriptor
                .GetProperties(o).Cast<PropertyDescriptor>()
                .ToDictionary(x => x.Name, x => x.GetValue(o));
        }
    }
        #endregion

}

