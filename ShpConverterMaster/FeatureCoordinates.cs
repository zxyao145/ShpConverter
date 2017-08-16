using System.Collections.Generic;
using System.Linq;
using DotSpatial.Data;
using DotSpatial.Topology;

namespace ShpConverter
{
    public interface ICoordinates
    {
        string GetFeatureCoordinates(IFeature feature);
    }
    class PointCoordinates: ICoordinates
    {
        protected string GetFeatureCoordinates(Coordinate coor)
        {
            return "["+coor.X + "," + coor.Y+"]" ;
        }
        public virtual string GetFeatureCoordinates(IFeature feature)
        {
            List<string> coorString=feature.Coordinates.Select(GetFeatureCoordinates).ToList();
            return string.Join(",", coorString) ;
        }
    }

    class MultiPointCoordinates : PointCoordinates
    {
        public override string GetFeatureCoordinates(IFeature multiPointFeature)
        {
            return "[" + base.GetFeatureCoordinates(multiPointFeature) + "]";
        }
    }

    class LineStringCoordinates : PointCoordinates
    {
        public override string GetFeatureCoordinates(IFeature lineStringFeature)
        {
            //List<string> lineStringCoorList= lineStringFeature.Coordinates.Select(coor => base.GetFeatureCoordinates(coor)).ToList();
            //return "[" + string.Join(",", lineStringCoorList) + "]";
            return "[" + base.GetFeatureCoordinates(lineStringFeature) + "]";
        }
    }

    class MultiLineStringCoordinates : LineStringCoordinates
    {
        public override string GetFeatureCoordinates(IFeature multiLineStringFeature)
        {
            var muiltLineString = multiLineStringFeature.BasicGeometry as IMultiLineString;
            List<string> multiLineCoorList=new List<string>();
            for (int i = 0; i < muiltLineString.Count; i++)
            {
                var oneline = muiltLineString[i];
                ILineString lineString = oneline as ILineString;
                if (lineString != null)
                {
                    IFeature ife = new Feature(lineString as IBasicGeometry);
                    multiLineCoorList.Add(base.GetFeatureCoordinates(ife));
                }
            }
            return "[" + string.Join(",", multiLineCoorList) + "]";
        }
    }

    class LinearRingCoordinates : PointCoordinates
    {
        public override string GetFeatureCoordinates(IFeature linearRingFeature)
        {
            List<string> coorStringList = linearRingFeature.Coordinates.Select(base.GetFeatureCoordinates).ToList();
            coorStringList.Add(coorStringList[0]);
            return "[" + string.Join(",", coorStringList) + "]";
        }
    }

    class PolygonCoordinates : LinearRingCoordinates
    {
        public override string GetFeatureCoordinates(IFeature polygonFeature)
        {
            var polygon = polygonFeature.BasicGeometry as IPolygon;
            if (polygon.Boundary.GeometryType != "MultiLineString")
            {
                return "[" + base.GetFeatureCoordinates(polygonFeature) + "]";
            }
            //else
            var geometryStr = polygonFeature.BasicGeometry.ToString();
            geometryStr = geometryStr.Replace(" ", "").Replace("),(", ")|(").Replace("POLYGON", "");
            List<string> linearString = new List<string>();
            linearString.AddRange(geometryStr.Split('|'));
            for (int i = 0; i < linearString.Count; i++)
            {
                var item = linearString[i];
                item = item.Replace("(", "").Replace(")", "").Replace(",", "],[").Replace("-", ",");
                item = "[[" + item + "]]";
                linearString[i] = item;
            }
            string s = "[" + string.Join(",", linearString) + "]";
            return s;
        }
    }
}
