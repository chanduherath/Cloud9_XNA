using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameStructure
{
    class Parser
    {
        

        public LifePack createLifePack(String str)
        {
            LifePack lf = new LifePack();
            char[] delimiters = new char[] { ':', '#', ',' };
            string[] arr = str.Split(delimiters);

            lf.Xcod = Convert.ToInt32(arr[1]);
            lf.Ycod = Convert.ToInt32(arr[2]);
            lf.Val = Convert.ToInt32(arr[3]);

            return lf;
        }

        public Coin createCoin(String str)
        {
            Coin coin = new Coin();
            char[] delimiters = new char[] { ':', '#', ',' };
            string[] arr = str.Split(delimiters);

            coin.Xcod = Convert.ToInt32(arr[1]);
            coin.Ycod = Convert.ToInt32(arr[2]);
            coin.Lifetime = Convert.ToInt32(arr[3]);
            coin.Val = Convert.ToInt32(arr[4]);

            return coin;
        }

        public List<String []> createMapList(String str)
        {
            List<String []> mapList = new List<String []>();
            char[] predelimiters = new char[] { ':', '#' };
            char[] postdelimiters = new char[] { ',', ';' };
            string[] arr = str.Split(predelimiters);

            for (int i = 2; i < arr.Length; i++)
            {
                String[] subArray = arr[i].Split(postdelimiters);
                mapList.Add(subArray);
            }
            return mapList;
        }


    }
}
