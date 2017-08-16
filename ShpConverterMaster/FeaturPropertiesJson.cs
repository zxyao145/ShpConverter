using System.Collections.Generic;
using System.Data;
using DotSpatial.Data;

namespace ShpConverter
{
    public static class FeaturPropertiesJson
    {
        /// <summary>
        /// 获取当前这一个feature的propertie对象
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public static string GetPropertieJson(this IFeature feature)
        {
            DataRow dr = feature.DataRow;
            var cols = dr.Table.Columns;
            var dataArr = dr.ItemArray;
            List<string> propertieList = new List<string>();
            for (int i = 0; i < cols.Count; i++)
            {
                string propertie = "\"" + cols[i].ColumnName + "\":\"" + dataArr[i].ToString() + "\"";
                propertieList.Add(propertie);
            }
            return string.Join(",", propertieList);
        }
    }
}
