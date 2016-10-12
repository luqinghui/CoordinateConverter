using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter
{
    public class LatLngToTileXY
    {
        #region 代码参考Bing Tile System
        private const double EarthRadius = 6378137;
        private const double MinLatitude = -85.05112878;
        private const double MaxLatitude = 85.05112878;
        private const double MinLongitude = -180;
        private const double MaxLongitude = 180;

        /// <summary>
        /// Clips a number to the specified minimum and maximum values.
        /// </summary>
        /// <param name="n">The number to clip.</param>
        /// <param name="minValue">Minimum allowable value.</param>
        /// <param name="maxValue">Maximum allowable value.</param>
        /// <returns>The clipped value.</returns>
        private double Clip(double n, double minValue, double maxValue)
        {
            return Math.Min(Math.Max(n, minValue), maxValue);
        }

        /// <summary>
        /// Determines the map width and height (in pixels) at a specified level
        /// of detail.
        /// </summary>
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
        /// to 23 (highest detail).</param>
        /// <returns>The map width and height in pixels.</returns>
        private uint MapSize(int levelOfDetail)
        {
            return (uint)256 << levelOfDetail;
        }

        /// <summary>
        /// Converts a point from latitude/longitude WGS-84 coordinates (in degrees)
        /// into pixel XY coordinates at a specified level of detail.
        /// </summary>
        /// <param name="latitude">Latitude of the point, in degrees.</param>
        /// <param name="longitude">Longitude of the point, in degrees.</param>
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
        /// to 23 (highest detail).</param>
        /// <param name="pixelX">Output parameter receiving the X coordinate in pixels.</param>
        /// <param name="pixelY">Output parameter receiving the Y coordinate in pixels.</param>
        private void LatLongToPixelXY(double latitude, double longitude, int levelOfDetail, out int pixelX, out int pixelY)
        {
            latitude = Clip(latitude, MinLatitude, MaxLatitude);
            longitude = Clip(longitude, MinLongitude, MaxLongitude);

            double x = (longitude + 180) / 360;
            double sinLatitude = Math.Sin(latitude * Math.PI / 180);
            double y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

            uint mapSize = MapSize(levelOfDetail);
            pixelX = (int)Clip(x * mapSize + 0.5, 0, mapSize - 1);
            pixelY = (int)Clip(y * mapSize + 0.5, 0, mapSize - 1);
        }
        /// <summary>
        /// Converts pixel XY coordinates into tile XY coordinates of the tile containing
        /// the specified pixel.
        /// </summary>
        /// <param name="pixelX">Pixel X coordinate.</param>
        /// <param name="pixelY">Pixel Y coordinate.</param>
        /// <param name="tileX">Output parameter receiving the tile X coordinate.</param>
        /// <param name="tileY">Output parameter receiving the tile Y coordinate.</param>
        private void PixelXYToTileXY(int pixelX, int pixelY, out int tileX, out int tileY)
        {
            tileX = pixelX / 256;
            tileY = pixelY / 256;
        }
        #endregion

        //适用于Google、高德、OSM
        public Point LatLngToTileXYGeneric(double latitude, double longitude, int zoomlevel)
        {
            Point result = new Point();
            int pixel_x, pixel_y;
            LatLongToPixelXY(latitude, longitude, zoomlevel, out pixel_x, out pixel_y);
            int tile_x, tile_y;
            PixelXYToTileXY(pixel_x, pixel_y, out tile_x, out tile_y);
            result.X = tile_x;
            result.Y = tile_y;
            return result;
        }

        //适用于Baidu
        public Point LatLngToTileXYBaidu(double latitude, double longitude, int zoomlevel)
        {
            Point result = new Point();

            result.X = (int)(Math.Pow(2, zoomlevel - 26) * (Math.PI * longitude * EarthRadius / 180));
            result.Y = (int)(Math.Pow(2, zoomlevel - 26) * EarthRadius * Math.Log(Math.Tan(Math.PI * latitude / 180.0) + 1.0 / Math.Cos(Math.PI * latitude / 180.0)));

            return result;
        }
        public Point LatLngToTileXYMercator(double latitude, double longitude, int zoomlevel)
        {
            Point result = new Point();

            double a = 6378137;
            double b = 6356752.3142;
            double L0 = 0;
            double B0 = 0;

            double e1 = Math.Sqrt(1.0 - Math.Pow((b / a), 2));
            double e2 = Math.Sqrt(Math.Pow((a / b), 2) - 1.0);

            double k = (a * a * Math.Cos(B0) / b) / Math.Sqrt(1 + e2 * e2 * Math.Cos(B0));

            double x = k * (longitude*Math.PI/180.0 - L0);
            double y = k * Math.Log(Math.Tan(Math.PI / 4.0 + latitude * Math.PI / 360.0) * Math.Pow((1.0 - e1 * Math.Sin(latitude)) / (1.0 + e1 * Math.Sin(latitude)), e1 / 2.0));

            double resolution = Math.Pow(2, 18 - zoomlevel);
            x = x / (resolution * 256.0);
            y = y / (resolution * 256.0);

            result.X = (int)x;
            result.Y = (int)y;

            return result;
        }

        public Point LatLngToTileXYWebMercator(double latitude, double longitude, int zoomlevel)
        {
            Point result = new Point();

            double r = 6378137.0;
            double x = r * (longitude * Math.PI / 180.0);
            double y = r * Math.Log(Math.Tan(Math.PI / 4.0 + latitude * Math.PI / 360.0));

            double resolution = Math.Pow(2, 18 - zoomlevel);
            x = x / (resolution * 256.0);
            y = y / (resolution * 256.0);

            result.X = (int)x;
            result.Y = (int)y;

            return result;
        }

        //适用于腾讯、TMS
        public Point LatLngToTileXYTecent(double latitude, double longitude, int zoomlevel)
        {
            Point result = new Point();
            result = LatLngToTileXYGeneric(latitude, longitude, zoomlevel);
            //TMS标准与Google切片一样，原点放在了左下角
            result.Y = (int)Math.Pow(2, zoomlevel) - 1 - result.Y;
            return result;
        }
    }
}
