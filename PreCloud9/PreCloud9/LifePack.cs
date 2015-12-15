using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStructure
{
    class LifePack
    {
        private int xcod;
        private int ycod;
        private int val;

        public LifePack()
        {
            this.xcod = 0;
            this.ycod = 0;
            this.val = 0;
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
        
        public int Val
        {
            get { return val; }
            set { val = value; }
        }

    }
}
