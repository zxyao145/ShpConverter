using System;
using DotSpatial.Data;
using ShpConverter;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            string shpPath = @"../../Data/Point.shp";
            IFeatureSet fs = FeatureSet.Open(shpPath);

            IShpConverter shpConverter = new ConverterToJson();
            string geoJson = shpConverter.GetGeoJson(fs);

            Console.WriteLine(geoJson);
            Console.ReadLine();
        }

    }
}
