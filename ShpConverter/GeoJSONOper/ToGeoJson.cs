using System.Collections.Generic;
using DotSpatial.Data;
using DotSpatial.Topology;
using Jil;

namespace ShpConverter.GeoJSONOper
{
    /// <summary>
    /// 将shapefile转成GeoJSON的具体类
    /// </summary>
    internal class ToGeoJson
    {
        internal string GetGeoJson(IFeatureSet fs)
        {
            if (fs.Features.Count==0)
            {
                return GeoJSON.Empty;
            }
            //else
            
            var type = fs.Features[0].GetGeoJsonType();
            if (type == GeoJSONType.Unspecified)
            {
                return string.Empty;
            }
            //else

            GeoJSON geoJson = new GeoJSON
            {
                Bbox = fs.GetBbox()
            };
            var features = new List<FeatureGeoJson>();

            //创建该feature对应的GeometryGeoJson对象
            foreach (var feature in fs.Features)
            {
                if (feature != null)
                {
                    if (feature.BasicGeometry == null || feature.BasicGeometry.NumPoints == 0)
                    {
                        continue;
                    }
                    //else

                    
                    //获取该feature的坐标点
                    var coords = feature.GetCoordinates(type);
                    if (coords == null)
                    {
                        continue;
                    }
                    //else

                    //获取该feature的属性信息
                    var properties = feature.GetPropertieJson();
                    GeometryGeoJson geometryGeoJson = new GeometryGeoJson(type)
                    {
                        Coordinates = coords
                    };
                    FeatureGeoJson featureGeoJson = new FeatureGeoJson()
                    {
                        GeometryGeoJson = geometryGeoJson,
                        Properties = properties
                    };
                    features.Add(featureGeoJson);

                }
                //else continue
            }
            geoJson.Features = features;
            return JSON.SerializeDynamic(geoJson);
        }
    }
}
