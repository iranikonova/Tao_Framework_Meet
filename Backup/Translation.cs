using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao_Framework_Meet
{
    public class Translation
    {
        public double DeltaTranslation = 0;
        public double x = 0, y = 0, z = 0;

        public Translation(Translation copy)
        {
            this.DeltaTranslation = copy.DeltaTranslation;
            this.x = copy.x;
            this.y = copy.y;
            this.z = copy.z;
        }

        public Translation()
        {
            /*DeltaTranslation = 0;
            x = 0; y = 0; z = 0;*/
        }
    }
}
