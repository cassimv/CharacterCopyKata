using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCopyKarta.Contracts
{
    public interface ISource
    {
        public char ReadChar();
        public char[] ReadChars(int count);
    }
}
