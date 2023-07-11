using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace fCommon.Utility
{
    public static class Config
    {
        private static string GetItem(string Key, string def)
        {
            string item = ConfigurationManager.AppSettings[Key]; //returns null if the key does not exist
            if (string.IsNullOrEmpty(item))
                item = def;
            return item;
        }
        private static string GetItem(string Key)
        {
            string item = ConfigurationManager.AppSettings[Key];
            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("Key", string.Format("The item [{0}] does not exist in the configuration file", Key));
            return item;
        }

        public static string GetString(string Key) { return GetItem(Key); }
        public static string GetString(string Key, string def) { return GetItem(Key,def); }
        public static int GetInt(string Key, int def)
        {
            if (ConfigurationManager.AppSettings[Key] == null) return def;
            return int.Parse(ConfigurationManager.AppSettings[Key]);
        }
        public static int GetInt(string Key)
        {
            int result;
            string resStr = GetItem(Key);
            if (!int.TryParse(resStr, out result))
                throw new InvalidCastException(string.Format("The item [{0}][{1}] is invalid, integer expected",  Key, resStr));

               return result;
        }

        public static short GetShort(string Key, short def)
        {
            if (ConfigurationManager.AppSettings[Key] == null) return def;
            return short.Parse(ConfigurationManager.AppSettings[Key]);
        }
        public static short GetShort(string Key)
        {
            short result;
            string resStr = GetItem(Key);
            if (!short.TryParse(resStr, out result))
                throw new InvalidCastException(string.Format("The item [{0}][{1}] is invalid, integer expected", Key, resStr));

            return result;
        }

        public static Decimal GetDecimal(string Key, decimal def)
        {
            if (ConfigurationManager.AppSettings[Key] == null) return def;
            return decimal.Parse(ConfigurationManager.AppSettings[Key]);
        }
        public static Decimal GetDecimal(string Key)
        {
            Decimal result;
            string resStr = GetItem(Key);
            if (!Decimal.TryParse(resStr, out result))
                throw new InvalidCastException(string.Format("The item [{0}][{1}] is invalid, Decimal expected", Key, resStr));

            return result;
        }
        public static bool GetBool(string Key)
        {
            bool result;
            string resStr = GetItem(Key);
            if (!bool.TryParse(resStr, out result))
                throw new InvalidCastException(string.Format("The item [{0}][{1}] is invalid, boolean expected", Key, resStr));

            return result;
        }
        public static bool GetBool(string Key, bool def)
        {
            if (ConfigurationManager.AppSettings[Key] == null) return def;
            return bool.Parse(ConfigurationManager.AppSettings[Key]);
        }
        public static DateTime GetDateTime(string Key)
        {
            DateTime result;
            string resStr = GetItem(Key);
            if (!DateTime.TryParse(resStr, out result))
                throw new InvalidCastException(string.Format("The item [{0}][{1}] is invalid, DateTime expected", Key, resStr));

            return result;
        }
        public static DateTime GetDateTime(string Key, DateTime def)
        {
            if (ConfigurationManager.AppSettings[Key] == null) return def;
            return DateTime.Parse(ConfigurationManager.AppSettings[Key]);
        }

        public static TEnum GetEnumValue<TEnum>(string Key)
    where TEnum : struct
        {
            string userInputString = GetItem(Key);
            TEnum value = (TEnum)Enum.Parse(typeof(TEnum), userInputString);
             if (!Enum.IsDefined(typeof(TEnum), value) )
             {
                 string msg = string.Format("{0} is not an underlying value of the STOCommissionFeesPaidFlag enumeration.",userInputString);
                 throw new ArgumentException(msg);
             }
            return  value ;
        }
    }
}
