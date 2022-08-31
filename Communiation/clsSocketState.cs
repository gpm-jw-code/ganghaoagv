using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GangHaoAGV.Communiation
{
    public class clsSocketState
    {
        public byte[] buffer = new byte[1024];
        public int bufferSize = 1024;
        public Socket socket;

        public clsSocketState(Socket socketint, int bufferSize = 1024)
        {
            this.socket = socketint;
            this.bufferSize = bufferSize;
            buffer = new byte[bufferSize];
        }
    }
}
