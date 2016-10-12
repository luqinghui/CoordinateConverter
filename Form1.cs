using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoordinateConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(WGSRadio.Checked)
            {
                Converter convert = new Converter();
                double lat = Convert.ToDouble(this.LatTextBox.Text);
                double lng = Convert.ToDouble(this.LngTextBox.Text);
                LatLng p = new LatLng { Latitude = lat, Longitude = lng };
                LatLng gcj = convert.WGS84ToGCJ02(p);
                LatLng bd = convert.WGS84ToBD09(p);
                ALabel.Text = "gcj:";
                BLabel.Text = "bd:";
                AResultTextBox.Text = gcj.Longitude + "," + gcj.Latitude;
                BResultTextBox.Text = bd.Longitude + "," + bd.Latitude;
            }
            else if(GCJRadio.Checked)
            {
                Converter convert = new Converter();
                double lat = Convert.ToDouble(this.LatTextBox.Text);
                double lng = Convert.ToDouble(this.LngTextBox.Text);
                LatLng p = new LatLng { Latitude = lat, Longitude = lng };
                LatLng wgs = convert.GCJ02ToWGS84(p);
                LatLng bd = convert.GCJ02ToBD09(p);
                ALabel.Text = "wgs:";
                BLabel.Text = "bd:";
                AResultTextBox.Text = wgs.Longitude + "," + wgs.Latitude;
                BResultTextBox.Text = bd.Longitude + "," + bd.Latitude;
            }
            else if(BDRadio.Checked)
            {
                Converter convert = new Converter();
                double lat = Convert.ToDouble(this.LatTextBox.Text);
                double lng = Convert.ToDouble(this.LngTextBox.Text);
                LatLng p = new LatLng { Latitude = lat, Longitude = lng };
                LatLng gcj = convert.BD09ToGCJ02(p);
                LatLng wgs = convert.BD09ToWGS84(p);
                ALabel.Text = "gcj:";
                BLabel.Text = "wgs:";
                AResultTextBox.Text = gcj.Longitude + "," + gcj.Latitude;
                BResultTextBox.Text = wgs.Longitude + "," + wgs.Latitude;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double lat = Convert.ToDouble(this.LatTextBox.Text);
            double lng = Convert.ToDouble(this.LngTextBox.Text);
            LatLngToTileXY convert = new LatLngToTileXY();
            int zoom = int.Parse(ZoomLevelTextBox.Text);
            Point p = convert.LatLngToTileXYGeneric(lat, lng, zoom);
            Point pt = convert.LatLngToTileXYTecent(lat, lng, zoom);
            Point pb = convert.LatLngToTileXYBaiduParameter(lat, lng, zoom);

            string googleuri = "http://mt2.google.cn/vt/lyrs=m@258000000&hl=zh-CN&gl=CN&src=app&x={0}&y={1}&z={2}&s=Ga";
            string tecenturi = "http://p3.map.gtimg.com/maptilesv2/{0}/{1}/{2}/{3}_{4}.png?version=20130701";
            string baiduuri = "http://online1.map.bdimg.com/tile/?qt=tile&styles=pl&x={0}&y={1}&z={2}";

            GoolgeTileResultTextBox.Text = string.Format(googleuri,p.X,p.Y,zoom);
            TecentTileResultTextBox.Text = string.Format(tecenturi, zoom, Math.Floor(pt.X / 16.0), Math.Floor(pt.Y / 16.0), pt.X, pt.Y);
            BaiduResultTextBox.Text = string.Format(baiduuri,pb.X,pb.Y,zoom);
        }
    }
}
