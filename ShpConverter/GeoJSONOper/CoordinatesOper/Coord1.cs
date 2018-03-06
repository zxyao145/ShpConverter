using System;
using System.Collections.Generic;
using DotSpatial.Data;
using DotSpatial.Topology;
using Jil;

namespace ShpConverter.GeoJSONOper.CoordinatesOper
{

    /// <summary>
    /// 一维坐标集合，针对单个点
    /// </summary>
    internal class Coord1
    {
        protected List<double> GetFeatureCoordinates(Coordinate coor)
        {
            return new List<double>() { coor.X, coor.Y };
        }
        public List<double> GetCoords(IFeature feature)
        {
            var coor = feature.Coordinates[0];
            return new List<double>() { coor.X, coor.Y };
        }
    }
}
