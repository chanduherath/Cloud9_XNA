using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PreCloud9
{
    class Tank
    {
        String playerName;
        int xcod;
        int ycod;
        int direction;

        
        public Tank()
        {
            this.playerName = "";
            this.Xcod = 0;
            this.Ycod = 0;
            this.Direction = 0;
        }


        public String PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }


        public int Xcod
        {
            get { return xcod; }
            set { xcod = value; }
        }

        public int Ycod
        {
            get { return ycod; }
            set { ycod = value; }
        }


        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        
    }
}
