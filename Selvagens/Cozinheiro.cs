using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Selvagens
{
    class Cozinheiro
    {
        private Pote pote;
        private Random rdm;

        public Cozinheiro() { }
        public Cozinheiro(Pote pote)
        {
            this.pote = pote;
        }

        public void execute()
        {
            while(true)
            {
                this.pote.depositar();
                this.rdm = new Random();
                int tempo = this.rdm.Next(500);
                Thread.Sleep(tempo);
            }
        }
    }
}
