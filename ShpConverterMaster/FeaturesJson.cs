using System.Collections.Generic;
using System.Linq;
using DotSpatial.Data;

namespace ShpConverter
{
    public static class FeaturesJson
    {
        /// <summary>
        /// 获取features对象的结果
        /// </summary>
        /// <param name="featureSet"></param>
        /// <returns></returns>
        public static string GetFeaturesJson(this IFeatureSet featureSet)
        {
            List<string> featuresJsonList= (from Feature feature in featureSet.Features select feature.GetOneFeatureJson()).ToList();
            return "[" + string.Join(",", featuresJsonList) + "]";
        }
        /// <summary>
        /// 获取一个feature的json,示例:
        /// { "type": "Feature", "properties": { "Id": 0 }, "geometry": { "type": "Point", "coordinates": [ -23799049.398816429, 26181539.292323731 ] } }
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        private static string GetOneFeatureJson(this IFeature feature)
        {
            string featuresJson = "{ \"type\": \"Feature\", \"properties\": {{0}},\"geometry\": {1}}";
            string properties = feature.GetPropertieJson();
            string geometry = feature.GetGeometryJson();
            return featuresJson.Replace("{0}", properties).Replace("{1}", geometry);
        }
    }
}
