using System.Reflection;
using DotSpatial.Data;
using DotSpatial.Topology;

namespace ShpConverter
{
    public static class FeatureGeometry
    {
        public static string GetGeometryJson(this IFeature feature)
        {
            string typeStr = feature.FeatureType.ToString();
            if (typeStr==DotSpatial.Topology.FeatureType.Line.ToString())
            {
                var lineString = feature.BasicGeometry as ILineString;
                if (lineString!=null)
                {
                    typeStr = FeatureType.LineString.ToString();
                }
                else
                {
                    typeStr = FeatureType.MultiLineString.ToString();
                }
            }
            string geometryJson = "{ \"type\": \""+ typeStr+"\", \"coordinates\": {0} }";
            
            string assemblyName = typeof (FeatureGeometry).Assembly.GetName().Name;
            string className = "ShpConverter." + typeStr+"Coordinates";
            ICoordinates coordinates = (ICoordinates)Assembly.Load(assemblyName).CreateInstance(className);

            string coordinatesTemp = coordinates.GetFeatureCoordinates(feature);
            geometryJson = geometryJson.Replace("{0}", coordinatesTemp);
            return geometryJson;
        }
    }
}
