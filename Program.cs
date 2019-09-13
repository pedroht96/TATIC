using System;
using System.Linq;
using System.Threading.Tasks;
using BaseDados.Objetos;

namespace BaseDados
{
    class Program
    {
        static int Main(string[] args)
        {   
            bool ArmazenadorIsSet = false;

            Armazenador armazenador = null;
            Buscador buscador = null;
            while(true)
            {
                var teclado = Console.ReadLine();

                var funcao = teclado.Split(" ");
                switch(funcao[0])
                {
                    case "armazenador":
                        if(funcao.Length == 2 && funcao[1].EndsWith(".txt"))
                        {
                            armazenador = new Armazenador(funcao[1]);
                            armazenador.LerArquivo();    
                            ArmazenadorIsSet = true;
                            buscador = new Buscador(armazenador);
                            Console.WriteLine("\n");
                        }
                        else
                        {
                            Console.WriteLine("Comando Inválido");
                        }
                        break;
                    case "buscador":
                        if(funcao.Length >= 3 && ArmazenadorIsSet)
                        {
                            string []eventos = funcao.Skip(3).ToArray();
                            Console.WriteLine(buscador.ProcurarPor(funcao[1], funcao[2], eventos));
                        }
                        else
                        {
                            Console.WriteLine("Comando Invalido");
                        }
                        break;
                }

            }

        }
    }
}
