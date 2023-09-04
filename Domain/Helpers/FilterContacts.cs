using Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Helpers
{
    public static class FilterContacts
    {
        public static List<ContactEntity> GetContacts(List<ContactEntity> contacts, List<Param> paramaters)
        {
            var now = DateTime.Now;
            var listName = GetParam(paramaters, "listName");
            var typeList = GetParam(paramaters, "typeList");

            var inputFilterDays = GetParam(paramaters, "inputFilterDays");


            var unity = GetParam(paramaters, "unity");
            var inputMinCountOrders = GetParam(paramaters, "inputMinCountOrders");
            var inputMaxCountOrders = GetParam(paramaters, "inputMaxCountOrders");
            var inputMinDay = GetParam(paramaters, "inputMinDays");
            var inputMaxDay = GetParam(paramaters, "inputMaxDays");

            var countMessages = GetParam(paramaters, "countMessages");
            var inputParamCupon = GetParam(paramaters, "inputParamCupon");
            var inputNameProduct = GetParam(paramaters, "inputNameProduct");
            var inputData = GetParam(paramaters, "inputData");

            return contacts;
        }

        public static string GetParam(List<Param> paramaters, string nameParam)
        {
            var search = paramaters.Where(p => p.Name == nameParam).Select(pm => pm.Value).ToList();

            if (search.Count() > 0)
                return search[0].ToString();
            return null;
        }
    }
}
