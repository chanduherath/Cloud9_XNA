﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStructure
{
    class GameManager
    {

        public GameEngine gEngine;      
        public GameManager()
        {
            gEngine = new GameEngine();   
        }
    }
}
