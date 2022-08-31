using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Models
{
    public class ModelBase
    {
        internal ushort NO;
        internal byte[] NOByte => BitConverter.GetBytes(NO);
        internal bool acturallyRecieved = true;

        public ModelBase()
        {
        }
        public ushort GetNO()
        {
            return NO;
        }
    }
}
