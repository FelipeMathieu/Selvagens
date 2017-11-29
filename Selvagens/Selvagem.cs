using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Selvagens
{
    class Selvagem
    {
        private Pote pote;
        private Random rdm;
        private int name;

        public Selvagem() { }

        public Selvagem(Pote pote, int name)
        {
            this.pote = pote;
            this.name = name;
        }

        public void execute()
        {
            while(true)
            {
                this.pote.retirar(this.name);
                this.rdm = new Random();
                int tempo = this.rdm.Next(2000);
                Thread.Sleep(tempo);
            }
        }
    }
}
