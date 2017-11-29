using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Selvagens
{
    class Pote
    {
        private int tamanho;
        List<int> fila = new List<int>();
        private int contador = 0;

        private SemaphoreSlim mutex = new SemaphoreSlim(1, 1);
        private Object cozinheiro = new Object();
        private Object selvagem = new Object();

        public Pote() { }

        public Pote(int tamanho)
        {
            this.tamanho = tamanho;
            this.contador = tamanho;
        }

        public void depositar()
        {

            lock (this.cozinheiro)
            {
                Monitor.Wait(this.cozinheiro);

                this.contador = this.tamanho;
                Console.WriteLine("\nCozinheiro encheu o pote!!!\n");

                lock (this.selvagem)
                {
                    Monitor.Pulse(this.selvagem);
                }
            }
        }

        public void retirar(int numeroSelvagem)
        {
            this.mutex.Wait();
            this.fila.Add(numeroSelvagem);
            Console.WriteLine("Selvagem " + numeroSelvagem.ToString() + " está no pote");
            this.mutex.Release();

            while (this.fila[this.fila.Count - 1] != numeroSelvagem)
            {
                lock (this.selvagem)
                {
                    Monitor.Wait(this.selvagem);
                    Monitor.Pulse(this.selvagem);
                }
            }

            while (this.contador == 0)
            {
                Console.WriteLine("POTE VAZIO!!");
                lock (this.cozinheiro)
                {
                    Monitor.Pulse(this.cozinheiro);
                }

                lock (this.selvagem)
                {
                    Monitor.Wait(this.selvagem);
                }
            }

            this.contador -= 1;
            Console.WriteLine("Consumido: [" + this.contador.ToString() + "], por " + " Selvagem " + numeroSelvagem.ToString());
            this.mutex.Wait();
            this.fila.Remove(this.fila.Count - 1);
            this.mutex.Release();

            lock (this.selvagem)
            {
                Monitor.Pulse(this.selvagem);
            }

        }
    }
}
