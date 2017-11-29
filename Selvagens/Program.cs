using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Selvagens
{
    class Program
    {
        static void Main(string[] args)
        {
            Pote pote = new Pote(5);
            Cozinheiro cozinheiro = new Cozinheiro(pote);

            List<Thread> s = new List<Thread>();

            Thread c = new Thread(cozinheiro.execute);
            c.Start();

            for(int i = 0; i < 15; i++)
            {
                s.Add(new Thread(new Selvagem(pote, i).execute));
                s[i].Start();
            }
        }
    }
}
