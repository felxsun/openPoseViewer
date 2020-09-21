using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace openposeImageKey
{
    public class openposeImageFrame
    {
        public enum keyPointModelType
        {
            body_25,
            coco
        }

        public string fileName = null;
        public List<openposeKeyPoint[]> bodys = null;
        public keyPointModelType keyType;

        public openposeImageFrame(string keyPointType="body_25")
        {
            bodys = new List<openposeKeyPoint[]>();
            if(keyPointType.ToUpper()=="COCO")
            {
                this.keyType = keyPointModelType.coco;
            }
        }

        static public openposeImageFrame[] loadFromTextFile(string file)
        {
            List<openposeImageFrame> rtn = new List<openposeImageFrame>();
            string line = "";
            string recordStr="";
            bool inSection = false;


            System.IO.StreamReader fr = new System.IO.StreamReader(@file);
            try
            {
                while ((line = fr.ReadLine()) != null)
                {
                    if (line.Trim().Length < 1)
                        continue;

                    if (inSection)
                    {
                        if (line[0] == '>')
                        {
                            openposeImageFrame oif = openposeImageFrame.fromTextLine(recordStr);
                            if(oif!=null)
                                rtn.Add(oif);

                            recordStr = line.Substring(1);
                        }
                        else
                            recordStr += line;
                    }
                    else if (line[0] == '>')
                    {
                        inSection = true;
                        recordStr = line.Substring(1);
                    }
                }

            }
            catch (Exception ex)
            {
                fr.Close();
                throw ex;
            }

            return rtn.ToArray();
        }

        static public openposeImageFrame[] loadFromTextFile_coco(string file)
        {
            List<openposeImageFrame> rtn = new List<openposeImageFrame>();
            string line = "";
            string recordStr = "";
            bool inSection = false;


            System.IO.StreamReader fr = new System.IO.StreamReader(@file);
            try
            {
                while ((line = fr.ReadLine()) != null)
                {
                    if (line.Trim().Length < 1)
                        continue;

                    if (inSection)
                    {
                        if (line[0] == '>')
                        {
                            openposeImageFrame oif = openposeImageFrame.fromTextLine_coco(recordStr);
                            if (oif != null)
                                rtn.Add(oif);

                            recordStr = line.Substring(1);
                        }
                        else
                            recordStr += Environment.NewLine+line;
                    }
                    else if (line[0] == '>')
                    {
                        inSection = true;
                        recordStr = line.Substring(1);
                    }
                }

            }
            catch (Exception ex)
            {
                fr.Close();
                throw ex;
            }

            return rtn.ToArray();
        }


        static public openposeImageFrame fromTextLine(string line)
        {
            openposeImageFrame rtn= new openposeImageFrame();
            line = line.Replace('[', '<').Replace(']', '>');
            line = Regex.Replace(line, "<<<", "|<<");
            line = Regex.Replace(line, ">>>", ">>|");

            string[] parts = line.Split('|');
            if (parts.Length < 3)
                return null;
            rtn.fileName = parts[0].Trim();



            string bodysStr = Regex.Replace(Regex.Replace(Regex.Replace(parts[1], "<<", "_<"),">>",">_"),"_\\s+_","_");
            if (!Regex.IsMatch(bodysStr, "^_") || !Regex.IsMatch(bodysStr, "_$"))
                throw new Exception("Invalid bodys keypoints format");
            bodysStr = Regex.Replace(bodysStr, "^_|_$","");
            string[] bodys = bodysStr.Split('_');

            foreach (string body in bodys)
                if(!Regex.IsMatch(body, "\\.\\.\\."))
                    rtn.bodys.Add(bodyFromLine(body));

            return rtn;
        }

        static public openposeImageFrame fromTextLine_coco(string line)
        {
            openposeImageFrame rtn = new openposeImageFrame();
            rtn.keyType = keyPointModelType.coco;

            string[] lines = line.Split('\n');
            rtn.fileName = lines[0].Trim();

            for(int i=1; i<lines.Length; ++i)
            {
                string body = lines[i].Trim();
                if (body.Length > 0)
                    rtn.bodys.Add(bodyFromLine_coco(body));
            }

            return rtn;
        }
        static public openposeKeyPoint[] bodyFromLine(string line)
        {
            List<openposeKeyPoint> points = new List<openposeKeyPoint>();

            line = Regex.Replace(line.Trim(), ">\\s+<", "_");
            if (!Regex.IsMatch(line, "^<") || !Regex.IsMatch(line, ">$"))
                throw new Exception("Invalid keypoints format");

            line = Regex.Replace(line, "^<|>$", "");
            string[] parts = line.Split('_');

            foreach (string ps in parts)
                points.Add(openposeKeyPoint.fromString2D(ps));

            return points.ToArray();
        }

        static public openposeKeyPoint[] bodyFromLine_coco(string line)
        {
            List<openposeKeyPoint> points = new List<openposeKeyPoint>();
            string[] lines = line.Split('|');
            

            foreach (string ps in lines)
                points.Add(openposeKeyPoint.fromString2D_coco(ps.Trim()));

            return points.ToArray();
        }
    }
}
