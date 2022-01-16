using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public abstract class Banco
    {
        public Banco()
        {
            this.NomedoBanco = "Dig Bank";
            this.CodigodoBanco = "1983";
        }
        public string NomedoBanco { get; private set; }
        public string CodigodoBanco { get; private set; }
    }
}
