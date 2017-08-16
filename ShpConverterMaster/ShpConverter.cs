using System;
using DotSpatial.Data;

namespace ShpConverter
{
    public class ConverterToJson : IShpConverter
    {
        public string GetGeoJson(IFeatureSet fs)
        {
            if (fs==null)
            {
                throw new Exception("shapefile文件为空！");
            }
            if (fs.DataTable==null)
            {
                throw new Exception("shapefile文件缺少属性表！");
            }
            string geoJson= "{\"type\": \"FeatureCollection\",\"bbox\":[{0}],\"features\": {1}}";
            string bbox = fs.Bbox();
            geoJson= geoJson.Replace("{0}", bbox);
            string featuresJSon = fs.GetFeaturesJson();
            geoJson= geoJson.Replace("{1}", featuresJSon);
            return geoJson;
        }
    }
}
