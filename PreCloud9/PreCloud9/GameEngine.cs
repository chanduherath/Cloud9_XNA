﻿using System;
using System.Collections;
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

        public  Queue<LifePack> lifePackQueue;
        public  Queue<Coin> coinQueue;
        public List<Coin> coinList;
        public List<LifePack> lifePackList;

        public GameEngine()
        {
            this.p = new Parser();
            this.con = new Connection();
            this.gridSize = 10;
            initializeMap();
            Thread listenThread = new Thread(listentoServer);
            listenThread.Start();
            lifePackQueue = new Queue<LifePack>();
            coinQueue = new Queue<Coin>();
            coinList = new List<Coin>();
            lifePackList = new List<LifePack>();
            coinObserver();
            //startTimer(5000);
        }

        private void initializeMap()
        {
            map = new String[gridSize, gridSize];
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
                markLifePackOnMap(lf, map);
                lifePackQueue.Enqueue(lf);
                
            } if (str.StartsWith("C"))
            {
                Coin coin = p.createCoin(str);
                markCoinOnMap(coin, map);
                //coinQueue.Enqueue(coin);
                coinList.Add(coin);
                coin.startTimer(coin.Lifetime);
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

        public void markLifePackOnMap(LifePack lf,String[,] tempmap)
        {
            tempmap[lf.Ycod, lf.Xcod] = "L";
            Console.WriteLine("After creating a lifepack.. ");
            //drawMap();
            this.map = tempmap;
        }

        public void markCoinOnMap(Coin coin, String[,] tempmap)
        {
            tempmap[coin.Ycod, coin.Xcod] = "C";
            Console.WriteLine("After creating a Coin.. ");
            //drawMap();
            this.map = tempmap;
            
        }

        public void coinObserver()
        {
            Thread thread = new Thread(new ThreadStart(workThreadMethod));
            thread.Start();
        }

        public void workThreadMethod()
        {
            while (true)
            {
                if (coinList.Count > 0)
                {
                    for (int i = 0; i < coinList.Count; i++)
                    {
                        if (coinList[i].State == false)
                        {
                            removeCoinFromMap(coinList[i], this.map);
                            coinList.Remove(coinList[i]);
                        }
                    }
                }
            }
        }

        public void removeCoinFromMap(Coin coin, string[,] tempmap)
        {
            tempmap[coin.Ycod, coin.Xcod] = "N";
            Console.WriteLine(coin.Ycod + " " + coin.Xcod + " " + "removed form string map");
            this.map = tempmap;
        }
    }
}
