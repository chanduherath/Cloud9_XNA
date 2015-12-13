using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace GameStructure
{
    class GameEngine
    {
        public static TcpListener listener;
        public static NetworkStream incommingStream;
        private Parser p;
        private Connection con;

        List<String[]> mapList = new List<String[]>();
        public String[,] map;
        public int gridSize;

        System.Timers.Timer tm;

        public GameEngine()
        {
            this.p = new Parser();
            this.con = new Connection();
            this.gridSize = 10;
            initializeMap();
            Thread listenThread = new Thread(listentoServer);
            listenThread.Start();
            startTimer(5000);
        }

        private void initializeMap()
        {
            this.map = new String[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    map[i, j] = "N";
                }
            }
        }

        public void callParserMethods(String str)
        {
            if (str.StartsWith("I"))
            {
                this.mapList = p.createMapList(str);
                markOnMap(mapList);
                drawMap();
            } if (str.StartsWith("L"))
            {
                LifePack lf = p.createLifePack(str);
                this.map = markLifePackOnMap(lf, map);
            } if (str.StartsWith("C"))
            {
                Coin coin = p.createCoin(str);
                this.map = markCoinOnMap(coin, map);
            }
        }

        public void markOnMap(List<String[]> maplist)
        {
            for (int i = 0; i < maplist.Count; i++)
            {
                String[] tempString = maplist[i];
                if (i == 0)//Bricks
                {
                    for (int j = 0; j < tempString.Length; j += 2)
                    {
                        map[Convert.ToInt32(tempString[j + 1]), Convert.ToInt32(tempString[j])] = "B";
                    }
                }
                if (i == 1)//Stones
                {
                    for (int j = 0; j < tempString.Length; j += 2)
                    {
                        map[Convert.ToInt32(tempString[j + 1]), Convert.ToInt32(tempString[j])] = "S";
                    }
                }
                if (i == 2)//Water
                {
                    for (int j = 0; j < tempString.Length; j += 2)
                    {
                        map[Convert.ToInt32(tempString[j + 1]), Convert.ToInt32(tempString[j])] = "W";
                    }
                }

            }
        }

        public void drawMap()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Console.Write(map[i, j] + " ");
                }
                Console.WriteLine();
            }

        }

        public void listentoServer()
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7000);
                listener.Start();

                while (true)
                {
                    TcpClient cln = listener.AcceptTcpClient();
                    incommingStream = cln.GetStream();
                    byte[] bytesToRead = new byte[cln.ReceiveBufferSize];
                    int bytesRead = incommingStream.Read(bytesToRead, 0, cln.ReceiveBufferSize);
                    Console.WriteLine(Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
                    this.callParserMethods(Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void startTimer(int miseconds)
        {
            tm = new System.Timers.Timer();
            tm.Interval = miseconds;
            tm.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            tm.Enabled = true;
            tm.Start();

        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            
        }

        public String [,] markLifePackOnMap(LifePack lf,String[,] map)
        {
            map[lf.Ycod, lf.Xcod] = "L";
            Console.WriteLine("After creating a lifepack.. ");
            //drawMap();
            return map;
        }

        public String[,] markCoinOnMap(Coin coin, String[,] map)
        {
            map[coin.Ycod, coin.Xcod] = "C";
            Console.WriteLine("After creating a Coin.. ");
            //drawMap();
            return map;
        }
    }
}
