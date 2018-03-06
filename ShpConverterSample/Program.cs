using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotSpatial.Data;
using ShpConverter;

namespace ShpConverterSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string shpPath = "../../Data";
            string[] shpFile = Directory.GetFiles(shpPath, "*.shp", SearchOption.TopDirectoryOnly);
           
            string savePath = "../../GeoJSON/ShpConverterResult";
            foreach (var file in shpFile)
            {
                IFeatureSet fs = FeatureSet.Open(file);
                
                IShpConvert shpConvert=new ShpConvert(fs);
                var geojson = shpConvert.ToGeoJSON();
                Console.WriteLine(geojson);

                //将GeoJSON保存到本地
                var fileName = Path.GetFileNameWithoutExtension(file) + ".json";
                var fileSave = Path.Combine(savePath, fileName ?? throw new InvalidOperationException());
                using (StreamWriter sw=new StreamWriter(fileSave,false,Encoding.UTF8))
                {
                    sw.Write(geojson);
                }
                Console.WriteLine();
                Console.WriteLine(new string('=',150));
                Console.WriteLine();
            }


            Console.WriteLine("All Finished");
            Console.ReadLine();
        }
    }
}
