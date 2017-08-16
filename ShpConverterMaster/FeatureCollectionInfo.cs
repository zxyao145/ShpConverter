using System;
using System.Collections.Generic;
using DotSpatial.Data;

namespace ShpConverter
{
    public static class FeatureCollectionInfo
    {
        public static string Bbox(this IFeatureSet fs)
        {
            if (fs==null)
            {
                throw new Exception("IFeatureSet对象为空!");
            }
            Extent extent = fs.Extent;
            List<string> bboxList = new List<string>
            {
                extent.MinX.ToString(),
                extent.MinY.ToString(),
                extent.MaxX.ToString(),
                extent.MaxY.ToString()
            };
            return string.Join(",", bboxList);
        }
        public static string Crs(this IFeatureSet fs)
        {
            return "";
        }
    }
}
