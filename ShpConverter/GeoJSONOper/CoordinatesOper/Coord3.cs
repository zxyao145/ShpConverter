using System;
using System.Collections.Generic;
using DotSpatial.Data;
using DotSpatial.Topology;
using Jil;

namespace ShpConverter.GeoJSONOper.CoordinatesOper
{
    /// <summary>
    /// 三维的数组，针对多线和面
    /// </summary>
    internal class Coord3 : Coord2
    {
        /// <summary>
        /// 获取多线的坐标点
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        internal new List<List<List<double>>> GetCoords(IFeature feature)
        {
            List<List<List<double>>> coordinates = new List<List<List<double>>>();

            if (feature.BasicGeometry is IMultiLineString muiltLineString)
            {
                for (int i = 0; i < muiltLineString.Count; i++)
                {
                    var oneline = muiltLineString[i];
                    ILineString lineString = (ILineString) oneline;
                    if (lineString != null)
                    {
                        IFeature ife = new Feature((IBasicGeometry) lineString);
                        coordinates.Add(base.GetCoords(ife));
                    }
                }
            }
            
            return coordinates;
        }

        /// <summary>
        /// 获取面的坐标点
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        internal new List<List<List<double>>> GetClosedCoords(IFeature feature)
        {
            var polygon = feature.BasicGeometry as IPolygon;
            if (polygon == null)
            {
                return null;
            }

            List<List<List<double>>> coordinates;
            if (polygon.Boundary.GeometryType != "MultiLineString")
            {
                coordinates = new List<List<List<double>>>()
                {
                    base.GetClosedCoords(feature)
                };
            }
            else
            {
                //带有洞的复杂面
                var geometryStr = feature.BasicGeometry.ToString();
                geometryStr = geometryStr.Replace("POLYGON ", "").TrimStart(new char[] { '(' }).TrimEnd(new char[] { ')' });
                var linesCoords = GetLinesCoords(geometryStr);

                coordinates = new List<List<List<double>>>();
                foreach (var line in linesCoords)
                {
                    List<List<double>> lineList = new List<List<double>>();
                    foreach (var pointCoord in line)
                    {
                        lineList.Add(base.GetFeatureCoordinates(pointCoord));
                    }
                    coordinates.Add(lineList);
                }
            }
           
            return coordinates;
        }
        /// <summary>
        /// 获取带有洞的复杂面的线点
        /// </summary>
        /// <param name="geometryStr"></param>
        /// <returns></returns>
        private List<List<Coordinate>> GetLinesCoords(string geometryStr)
        {
            var lineList = new List<List<Coordinate>>();
            string[] lineItem = geometryStr.Split(new string[] { "), (" }, StringSplitOptions.RemoveEmptyEntries);
            if (lineItem.Length>0)
            {
                foreach (var line in lineItem)
                {
                    List<Coordinate> oneLine = new List<Coordinate>();
                    var pointStr = line.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var point in pointStr)
                    {
                        string[] xy = point.Split(new char[] { ' ' });
                        if (xy.Length == 2)
                        {
                            Coordinate coord = new Coordinate()
                            {
                                X = Convert.ToDouble(xy[0]),
                                Y = Convert.ToDouble(xy[1]),
                            };
                            oneLine.Add(coord);
                        }

                    }
                    if (oneLine.Count > 0)
                    {
                        lineList.Add(oneLine);
                    }
                }
            }
            return lineList;
        }
    }
}
