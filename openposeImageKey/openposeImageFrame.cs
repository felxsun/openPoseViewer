using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openposeImageKey
{
    public class openposeImageFrame
    {
        public string fileName = null;
        public List<float[,]> bodys = null;

        public openposeImageFrame()
        {
            bodys = new List<float[,]>();
        }

        static public openposeImageFrame[] loadFromTextFile(string file)
        {
            List<openposeImageFrame> rtn = new List<openposeImageFrame>();
            string line;

            System.IO.StreamReader fr = new System.IO.StreamReader(@file);
            try
            {
                while ((line = fr.ReadLine()) != null)
                {
                    line.Trim();
                    if (line[0] == '>')
                    {
                        rtn.Add(parseImageSection(line.Substring(1), fr);
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

        static public openposeImageFrame parseImageSection(string fileName, System.IO.StreamReader sr)
        {
            int chr;
            openposeImageFrame rtn = new openposeImageFrame();
            rtn.fileName = fileName.Trim();

            while ((chr = sr.Read()) != -1)
            {
                if (chr == (int)'[')
                    getFrameBodys(rtn, sr);
            }
            return rtn;
        }

        static public void getFrameBodys(openposeImageFrame oif, System.IO.StreamReader sr)
        {
            int chr;
            List<string> keyPointLines = new List<string>();
            while ((chr = sr.Read()) != -1)
            {
                if ('[' == (char)chr)
                    getBodyPoints(keyPointLines, sr);
            }
        }

        static public void getBodyPoints(List<string> lines, System.IO.StreamReader sr)
        {
            int chr;
            bool inPointLine = false;
            string line

            while ((chr = sr.Read()) != -1)
            {
                if (inPointLine)
                {

                }
                else
                    inPointLine = '[' == (char) chr;
            }
        }
    }
}
