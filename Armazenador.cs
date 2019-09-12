using System;
using System.IO;
using System.Threading.Tasks;

namespace BaseDados.Objetos
{
    public class Armazenador
    {
        private string nomeArquivo { get; set; }

        private Registros registros { get; set; }

        private Registros PriRegistros
        {
            get
            {
                return registros;
            }
        }

        public Armazenador(string nomeArquivo)
        {
            try
            {
                /* Verifica se a propriedade contém apenas espaços ou está vazia */
                if(string.IsNullOrEmpty(nomeArquivo) || string.IsNullOrWhiteSpace(nomeArquivo))
                    throw new NullReferenceException(nomeArquivo.GetType().FullName + " Não pode ser NULL ou Vazio");
                /* Verifica se o arquivo existe */
                if(!System.IO.File.Exists(nomeArquivo))
                    throw new Exception(nomeArquivo + " Não Encontrado");
            }
            catch (NullReferenceException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            this.nomeArquivo = nomeArquivo;
        }

        public void LerArquivo()
        {
            /* Carrega o Arquivo */
            using(var reader = new StreamReader(nomeArquivo))
            {
                registros = new Registros();
                /* While que verificar se está no afim do arquivo*/
                while(!reader.EndOfStream)
                {
                    /* Ler do arquivo a cada quebra de linha */
                    var line = reader.ReadLine();

                    /* Separa cada conteudo da string a cada ; retornada */
                    var data = line.Split(";");
                    
                    /* Funcao responsável por adicionar o registro na memoria */
                    registros.AdicionarRegistro(data[0], data[1], data[2]);
                }
            }
        }

        public Registros GetData()
        {
            return PriRegistros;
        }
    }
}