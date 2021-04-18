using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EnumModelBinder.Web.Model
{
    public static class EnumExts
    {
        public static string GetDescription(this Enum genericEnum)
        {
            Type genericEnumType = genericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(genericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((attribs != null && attribs.Count() > 0))
                {
                    return ((DescriptionAttribute)attribs.ElementAt(0)).Description;
                }
            }
            return genericEnum.ToString();
        }

        public static IReadOnlyCollection<EnumItem> GetItems(Type enumType)
        {
            var values = Enum.GetValues(enumType);
            var items = new List<EnumItem>();
            foreach (var item in values)
            {
                //var enumItem = item;
                items.Add(new EnumItem
                {
                    Id = (int)item,
                    EnumName = item.ToString(),
                    Description = ((Enum)item).GetDescription()
                });

            }


            return new ReadOnlyCollection<EnumItem>(items);
        }
    }
}
