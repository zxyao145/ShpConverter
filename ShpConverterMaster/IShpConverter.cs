using DotSpatial.Data;

namespace ShpConverter
{
    public interface IShpConverter
    {
        string GetGeoJson(IFeatureSet fs);
    }
}
