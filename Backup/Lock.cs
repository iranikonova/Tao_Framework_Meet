using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao_Framework_Meet
{
    public class Lock_
    {
        public bool lockStatus;
        public bool enableChange;

        public Lock_()
        {
        }

        public Lock_(Lock_ copy)
        {
            this.lockStatus = copy.lockStatus;
            this.enableChange = copy.enableChange;
        }

        public Lock_(bool lockStatus_, bool enableChange_)
        {
            lockStatus = lockStatus_;
            enableChange = enableChange_;
        }
    }
}
