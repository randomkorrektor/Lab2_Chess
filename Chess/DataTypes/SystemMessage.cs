using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public class SystemMessage
    {
        public string Message;
        public int NumberOfPlayer;

        public SystemMessage(string message, int numberOfPPlayer)
        {
            Message = message;
            NumberOfPlayer = numberOfPPlayer;
        }
    }
}
