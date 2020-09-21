using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace openposeImageKey
{
    public class openposeKeyPoint
    {
        public float x = float.NaN;
        public float y = float.NaN;
        public float z = float.NaN;
        public float pValue = float.NaN;

        public static openposeKeyPoint fromString2D(string line)
        {
            openposeKeyPoint rtn = new openposeKeyPoint();
            string[] lines=Regex.Replace(line.Trim(),"\\s+"," ").Split(' ');
            float temp;

            try
            {
                if(!float.TryParse(lines[0],out temp))
                    throw new Exception("Invalid point x value '" + line + "'");
                rtn.x = temp;

                if (!float.TryParse(lines[1], out temp))
                    throw new Exception("Invalid point y value '" + line + "'");
                rtn.y = temp;

                if (!float.TryParse(lines[2], out temp))
                    throw new Exception("Invalid point p-value '" + line + "'");
                rtn.pValue = temp;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to parse point record '" + line + "' : " + ex.Message);
            }

            return rtn;
        }

        public static openposeKeyPoint fromString2D_coco(string line)
        {
            openposeKeyPoint rtn = new openposeKeyPoint();
            string[] lines = line.Trim().Split(',');
            float temp;

            try
            {
                if (!float.TryParse(lines[0], out temp))
                    throw new Exception("Invalid point x value '" + line + "'");
                rtn.x = temp;

                if (!float.TryParse(lines[1], out temp))
                    throw new Exception("Invalid point y value '" + line + "'");
                rtn.y = temp;

                if (!float.TryParse(lines[2], out temp))
                    throw new Exception("Invalid point p-value '" + line + "'");
                rtn.pValue = temp;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to parse point record '" + line + "' : " + ex.Message);
            }

            return rtn;
        }


    }
}
