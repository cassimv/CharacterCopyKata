using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCopyKarta.Contracts
{
    public interface IDestination
    {
        public void WriteChar(char c);
        public void WriteChars(char[] values);
    }
}
