//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphControlLib
{
    public class SelectionEventArgs :EventArgs
    {
        public double MinX, MaxX, MinY, MaxY;
    }
}
