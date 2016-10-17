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

        //适用于腾讯、TMS
        public Point LatLngToTileXYTecent(double latitude, double longitude, int zoomlevel)
        {
            Point result = new Point();
            result = LatLngToTileXYGeneric(latitude, longitude, zoomlevel);
            //TMS标准与Google切片一样，原点放在了左下角
            result.Y = (int)Math.Pow(2, zoomlevel) - 1 - result.Y;
            return result;
        }

        //适用于Baidu （有问题）
        public Point LatLngToTileXYBaidu(double latitude, double longitude, int zoomlevel)
        {
            Point result = new Point();

            result.X = (int)(Math.Pow(2, zoomlevel - 26) * (Math.PI * longitude * EarthRadius / 180));
            result.Y = (int)(Math.Pow(2, zoomlevel - 26) * EarthRadius * Math.Log(Math.Tan(Math.PI * latitude / 180.0) + 1.0 / Math.Cos(Math.PI * latitude / 180.0)));

            return result;
        }      

        //根据经纬度转Mercator（原点在经纬度（0，0）位置）（有问题）
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

            double x = k * (longitude * Math.PI / 180.0 - L0);
            double y = k * Math.Log(Math.Tan(Math.PI / 4.0 + latitude * Math.PI / 360.0) * Math.Pow((1.0 - e1 * Math.Sin(latitude)) / (1.0 + e1 * Math.Sin(latitude)), e1 / 2.0));

            double resolution = Math.Pow(2, 18 - zoomlevel);
            x = x / (resolution * 256.0);
            y = y / (resolution * 256.0);

            result.X = (int)x;
            result.Y = (int)y;

            return result;
        }

        //根据经纬度转WebMercator（椭球简化）（有问题）
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

        #region 相关私有变量与方法
        private double[] MCBAND = { 12890594.86, 8362377.87, 5591021, 3481989.83, 1678043.12, 0 };
        private double[] LLBAND = { 75, 60, 45, 30, 15, 0 };
        private double[,] MC2LL = { { 1.410526172116255e-8, 0.00000898305509648872, -1.9939833816331,
                  200.9824383106796, -187.2403703815547, 91.6087516669843,
                  -23.38765649603339, 2.57121317296198, -0.03801003308653, 17337981.2 },
         { -7.435856389565537e-9, 0.000008983055097726239, -0.78625201886289,
          96.32687599759846, -1.85204757529826, -59.36935905485877,
          47.40033549296737, -16.50741931063887, 2.28786674699375, 10260144.86 },
         { -3.030883460898826e-8, 0.00000898305509983578, 0.30071316287616,
          59.74293618442277, 7.357984074871, -25.38371002664745,
          13.45380521110908, -3.29883767235584, 0.32710905363475, 6856817.37 },
         { -1.981981304930552e-8, 0.000008983055099779535, 0.03278182852591,
          40.31678527705744, 0.65659298677277, -4.44255534477492,
          0.85341911805263, 0.12923347998204, -0.04625736007561, 4482777.06 },
         { 3.09191371068437e-9, 0.000008983055096812155, 0.00006995724062,
          23.10934304144901, -0.00023663490511, -0.6321817810242,
          -0.00663494467273, 0.03430082397953, -0.00466043876332, 2555164.4 },
         { 2.890871144776878e-9, 0.000008983055095805407, -3.068298e-8,
          7.47137025468032, -0.00000353937994, -0.02145144861037,
          -0.00001234426596, 0.00010322952773, -0.00000323890364, 826088.5 } };

        double[,] LL2MC = { { -0.0015702102444, 111320.7020616939, 1704480524535203,
          -10338987376042340, 26112667856603880, -35149669176653700,
          26595700718403920, -10725012454188240, 1800819912950474, 82.5 },
         { 0.0008277824516172526, 111320.7020463578, 647795574.6671607,
          -4082003173.641316, 10774905663.51142, -15171875531.51559,
          12053065338.62167, -5124939663.577472, 913311935.9512032, 67.5},
         { 0.00337398766765, 111320.7020202162, 4481351.045890365,
          -23393751.19931662, 79682215.47186455, -115964993.2797253,
          97236711.15602145, -43661946.33752821, 8477230.501135234, 52.5},
         { 0.00220636496208, 111320.7020209128, 51751.86112841131,
          3796837.749470245, 992013.7397791013, -1221952.21711287,
          1340652.697009075, -620943.6990984312, 144416.9293806241, 37.5},
         { -0.0003441963504368392, 111320.7020576856, 278.2353980772752,
          2485758.690035394, 6070.750963243378, 54821.18345352118,
          9540.606633304236, -2710.55326746645, 1405.483844121726, 22.5},
         { -0.0003218135878613132, 111320.7020701615, 0.00369383431289,
          823725.6402795718, 0.46104986909093, 2351.343141331292,
          1.58060784298199, 8.77738589078284, 0.37238884252424, 7.45}};


        private double[] convert_coord_to_mercator(double latitude, double longitude)
        {
            double[] ll2mc = new double[10];

            int llband_len = LLBAND.Length;

            for (int i = 0; i < llband_len; i++)
            {
                if (latitude > LLBAND[i])
                {
                    for(int j=0;j<10;j++)
                    {
                        ll2mc[j] = LL2MC[i, j];
                    }
                    break;
                }
            }
            if (ll2mc == null || ll2mc.Length == 0)
            {
                for (int i = llband_len - 1; i >= 0; i--)
                {
                    if (latitude <= -LLBAND[i])
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            ll2mc[j] = LL2MC[i, j];
                        }
                        break;
                    }
                }
            }
            double x = ll2mc[0] + ll2mc[1] * Math.Abs(longitude);
            double a = Math.Abs(latitude) / ll2mc[9];
            double y = ll2mc[2] + ll2mc[3] * a + ll2mc[4] * Math.Pow(a, 2) + ll2mc[5] * Math.Pow(a, 3);
            y += ll2mc[6] * Math.Pow(a, 4) + ll2mc[7] * Math.Pow(a, 5) + ll2mc[8] * Math.Pow(a, 6);
            x *= (longitude > 0 ? 1 : -1);
            y *= (latitude > 0 ? 1 : -1);

            return new double[] { x, y };
        }

        private double[] convert_mercator_to_px(double x, double y, int z)
        {
            double x_px = Math.Floor(x * Math.Pow(2, z - 18));
            double y_px = Math.Floor(y * Math.Pow(2, z - 18));
            return new double[] { x_px, y_px };
        }

        private int[] convert_px_to_tile(double x, double y)
        {
            int t_x = (int)(Math.Floor(x / 256.0));
            int t_y = (int)(Math.Floor(y / 256.0));
            return new int[] { t_x, t_y };
        }
        #endregion

        //通过相关参数解析瓦片坐标
        public Point LatLngToTileXYBaiduParameter(double latitude, double longitude, int zoomlevel)
        {
            Point result = new Point();

            double[] point = convert_coord_to_mercator(latitude, longitude);
            double[] px = convert_mercator_to_px(point[0], point[1], zoomlevel);
            int[] tilexy = convert_px_to_tile(px[0], px[1]);
            result.X = tilexy[0];
            result.Y = tilexy[1];
            return result;
        }
    }
}
