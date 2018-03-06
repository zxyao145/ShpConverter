using DotSpatial.Data;
using DotSpatial.Projections;
using ShpConverter.GeoJSONOper;

namespace ShpConverter
{
    /// <summary>
    /// shapefile文件转换器
    /// </summary>
    public class ShpConvert : IShpConvert
    {
        /// <summary>
        /// 通过shapefile对应的IFeatureSet对象，创建一个ShpConvert实例
        /// </summary>
        /// <param name="fs">shapefile对应的IFeatureSet对象</param>
        public ShpConvert(IFeatureSet fs)
        {
            this.Fs = fs;
        }

        /// <summary>
        /// 创建一个ShpConvert实例
        /// </summary>
        public ShpConvert()
        {

        }
        /// <summary>
        /// shapefile对应的IFeatureSet对象
        /// </summary>
        public IFeatureSet Fs { get; set; }

        /// <summary>
        /// 将shapefile文件转为GeoJSON，目前仅支持WGS84坐标系
        /// </summary>
        /// <returns>GeoJSON字符串</returns>
        public string ToGeoJSON()
        {
            CheckFs();
            ToGeoJson toGeoJson = new ToGeoJson();
            return toGeoJson.GetGeoJson(Fs);
        }

        private void CheckFs()
        {
            if (Fs == null)
            {
                throw new ShpConvertException("shapefile文件为空！");
            }
            if (Fs.DataTable == null)
            {
                throw new ShpConvertException("shapefile文件缺少属性表！");
            }

            if (Fs.Projection == KnownCoordinateSystems.Geographic.World.WGS1984)
            {
                throw new ShpConvertException("ShpConvert目前不支持WGS1984以外的坐标系！");
            }
        }
    }

    
}
