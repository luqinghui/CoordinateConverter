using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter
{
    public struct LatLng
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LatLng(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
  
    public class Converter
    {
        //中国范围经纬度
        private double RANGE_LON_MAX = 137.8347;
        private double RANGE_LON_MIN = 72.004;
        private double RANGE_LAT_MAX = 55.8271;
        private double RANGE_LAT_MIN = 0.8293;

        //椭球参数
        private double jzA = 6378245.0;
        private double jzEE = 0.00669342162296594323;

        private double transformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * Math.PI) + 20.0 * Math.Sin(2.0 * x * Math.PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * Math.PI) + 40.0 * Math.Sin(y / 3.0 * Math.PI)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * Math.PI) + 320 * Math.Sin(y * Math.PI / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        private double transformLon(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * Math.PI) + 20.0 * Math.Sin(2.0 * x * Math.PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * Math.PI) + 40.0 * Math.Sin(x / 3.0 * Math.PI)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * Math.PI) + 300.0 * Math.Sin(x / 30.0 * Math.PI)) * 2.0 / 3.0;
            return ret;
        }

        private bool outOfChina(double lat, double lon)
        {
            if (lon < RANGE_LON_MIN || lon > RANGE_LON_MAX)
                return true;
            if (lat < RANGE_LAT_MIN || lat > RANGE_LAT_MAX)
                return true;
            return false;
        }

        //wgs84转gcj02
        private LatLng gcj02Encrypt(double ggLat, double ggLon)
        {
            LatLng resPoint = new LatLng();
            double mgLat;
            double mgLon;
            if (outOfChina(ggLat, ggLon))
            {
                resPoint.Latitude = ggLat;
                resPoint.Longitude = ggLon;
                return resPoint;
            }
            double dLat = transformLat(ggLon - 105.0, ggLat - 35.0);
            double dLon = transformLon(ggLon - 105.0, ggLat - 35.0);
            double radLat = ggLat / 180.0 * Math.PI;
            double magic = Math.Sin(radLat);
            magic = 1 - jzEE * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((jzA * (1 - jzEE)) / (magic * sqrtMagic) * Math.PI);
            dLon = (dLon * 180.0) / (jzA / sqrtMagic * Math.Cos(radLat) * Math.PI);
            mgLat = ggLat + dLat;
            mgLon = ggLon + dLon;

            resPoint.Latitude = mgLat;
            resPoint.Longitude = mgLon;
            return resPoint;
        }

        //gcj02转wgs84
        private LatLng gcj02Decrypt(double gjLat, double gjLon)
        {
            LatLng gPt = gcj02Encrypt(gjLat, gjLon);
            double dLon = gPt.Longitude - gjLon;
            double dLat = gPt.Latitude - gjLat;
            LatLng pt = new LatLng();
            pt.Latitude = gjLat - dLat;
            pt.Longitude = gjLon - dLon;
            return pt;
        }

        //gcj02转bd09
        private LatLng bd09Encrypt(double ggLat, double ggLon)
        {
            LatLng bdPt = new LatLng();
            double x = ggLon, y = ggLat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * Math.PI*3000/180);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * Math.PI*3000/180);
            bdPt.Longitude = z * Math.Cos(theta) + 0.0065;
            bdPt.Latitude = z * Math.Sin(theta) + 0.006;
            return bdPt;
        }

        //bd09转gcj02
        private LatLng bd09Decrypt(double bdLat, double bdLon)
        {
            LatLng gcjPt = new LatLng();
            double x = bdLon - 0.0065, y = bdLat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * Math.PI*3000/180);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * Math.PI*3000/180);
            gcjPt.Longitude = z * Math.Cos(theta);
            gcjPt.Latitude = z * Math.Sin(theta);
            return gcjPt;
        }

        public LatLng WGS84ToGCJ02(LatLng location)
        {
            return gcj02Encrypt(location.Latitude, location.Longitude);
        }
        
        public LatLng GCJ02ToWGS84(LatLng location)
        {
            return gcj02Decrypt(location.Latitude, location.Longitude);
        }
      
        public LatLng WGS84ToBD09(LatLng location)
        {
            LatLng gcj02Pt = gcj02Encrypt(location.Latitude, location.Longitude);
            return bd09Encrypt(gcj02Pt.Latitude, gcj02Pt.Longitude);
        }

        public LatLng BD09ToWGS84(LatLng location)
        {
            LatLng gcj02 = BD09ToGCJ02(location);
            return gcj02Decrypt(gcj02.Latitude, gcj02.Longitude);
        }

        public LatLng GCJ02ToBD09(LatLng location)
        {
            return bd09Encrypt(location.Latitude, location.Longitude);
        }

        public LatLng BD09ToGCJ02(LatLng location)
        {
            return bd09Decrypt(location.Latitude, location.Longitude);
        }
    }
}
