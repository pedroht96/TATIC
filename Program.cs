using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Habanero.Faces.Base;
using Microsoft.Win32;
 

namespace TATIC
{
    class Program
    {
        static void Main(string[] args)
        {
            menu();
        }

        public static void menu()
        {
            Console.WriteLine("Digite a opção desejada");
            Console.WriteLine("1 - Armazenador");
            Console.WriteLine("2- Buscador");
            Console.WriteLine("0- Para Sair");
            int op = int.Parse(Console.ReadLine());
            switch (op)
            {
                case 1:
                    Console.WriteLine("Digite o caminho que o arquivo para ser lido se encontra");
                    string caminho = Console.ReadLine();
                    lerArquivo(caminho);
                    Console.ReadKey();
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    Console.ReadKey();
                    break;
            }

        }

        public static void lerArquivo(string caminho)
        {           
            string text = System.IO.File.ReadAllText(caminho);
            string linha;
            while ((linha = text.ReadLine()) != null)
            {
                string[] dados = linha.Split(';');
                int codigo = int.Parse(dados[0]);
                string nome = dados[1];
                string endereco = dados[2];

            }
        }
    }
}
