using PreCloud9;
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
            lf.LifeTime = Convert.ToInt32(arr[3]);

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

        public String getMyPlayerName(String str)
        {
            return str.Substring(2, 2);
        }

        public Tank getMydetails(String str,String name)
        {
            Tank myTank = new Tank();
            char[] predelimiters = new char[] { ':', '#' };
            char[] postdelimiters = new char[] { ',', ';' };
            string[] arr = str.Split(predelimiters);
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].StartsWith(name))
                {
                    string[] arr1 = arr[i].Split(postdelimiters);
                    myTank.Xcod = Int32.Parse(arr1[1]);
                    myTank.Ycod = Int32.Parse(arr1[2]);
                    myTank.Direction = Int32.Parse(arr1[3]);
                }
            }
            myTank.PlayerName = name;
            return myTank;
        }


    }
}
