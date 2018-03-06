using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Data;

namespace ShpConverter
{
    /// <summary>
    /// shapefile文件转换器的借口
    /// </summary>
    public interface IShpConvert
    {
        /// <summary>
        /// 将shapefile文件转为GeoJSON，目前仅支持WGS84坐标系
        /// </summary>
        /// <returns>GeoJSON字符串</returns>
        string ToGeoJSON();
    }
}
