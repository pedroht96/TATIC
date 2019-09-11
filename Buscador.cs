using System;
using System.Linq;
using System.Text;

namespace BaseDados.Objetos
{
    public class Buscador
    {
        private Armazenador armazenador { get; set; }

        public int NumeroResultados { get; set; }

        public Buscador(Armazenador armazenador)
        {
            this.armazenador = armazenador;
        }

        public StringBuilder ProcurarPor(string dataInicial, string dataFinal, string[] Eventos)
        {
            NumeroResultados = 0;
            StringBuilder resultado =  new StringBuilder();
            var dados = armazenador.GetData().GetRegistros();

            var ticktsInicial = GetDateTimeFromString(dataInicial).Ticks;
            var ticktsFinal = GetDateTimeFromString(dataFinal).Ticks;

            var FullTicksInicial = GetFullDateTimeTicks(dataInicial).Ticks;
            var FullTicksFinal = GetFullDateTimeTicks(dataFinal).Ticks;

            foreach (var chavesIndexadora in dados.Keys)
            {
                // Verifica se a chave que atual está entre as datas, caso não esteja os for em cascata não são executados
                if(chavesIndexadora >= ticktsInicial && chavesIndexadora <= ticktsFinal)
                {
                    foreach (var Evento in dados[chavesIndexadora].Keys)
                    {
                        foreach (var registro in dados[chavesIndexadora][Evento])
                        {
                            // Obtem a representação da data do registro completa valor numerico
                            var ticktsRegistro = registro.GetDateTime().Ticks;
                            // Veririca se está entre a data solicitada
                            if(ticktsRegistro <= FullTicksFinal && ticktsRegistro >= FullTicksInicial)
                            {
                                // Verifica se deseja filtrar também por evento
                                if(Eventos.Length > 0)
                                {   // Verifica se os eventos a ser buscado é o do evento a ser lido atual
                                    if(!BuscarPorEvento(Eventos, Evento))
                                        break; // Caso não seja o loop vai para o próximo
                                    NumeroResultados++;
                                    resultado.Append(registro.GetDateTimeStringFormat()).Append(";").Append(Evento.ToString("X"))
                                                .Append(";").Append(registro.RUsuario.ToString()).Append("\n");
                                }
                                else // Caso a busca não esteja filtrando por linha apenas adiciona ao resultado
                                {
                                    NumeroResultados++;
                                    resultado.Append(registro.GetDateTimeStringFormat()).Append(";").Append(Evento.ToString("X"))
                                                .Append(";").Append(registro.RUsuario.ToString()).Append("\n");
                                }
                            }
                        }
                    }
                }
            }


            return resultado;
        }
        // Retorna um objeto dateTime apenas com os indices
        private DateTime GetDateTimeFromString(string data)
        {
            UInt16 ano = UInt16.Parse(data.Substring(0, 4));
            byte mes = byte.Parse(data.Substring(4, 2));
            byte dia = byte.Parse(data.Substring(6, 2));
            byte hora = byte.Parse(data.Substring(8, 2));
            byte minuto = byte.Parse(data.Substring(10, 2));
            byte segundos = byte.Parse(data.Substring(12, 2));
            UInt16 milissegundos = UInt16.Parse(data.Substring(14, 3));

            return new DateTime(ano, mes, dia, hora, 0, 0);
        }
        // Retorna um objeto dateTime com todos os valores da data
        private DateTime GetFullDateTimeTicks(string data)
        {
            UInt16 ano = UInt16.Parse(data.Substring(0, 4));
            byte mes = byte.Parse(data.Substring(4, 2));
            byte dia = byte.Parse(data.Substring(6, 2));
            byte hora = byte.Parse(data.Substring(8, 2));
            byte minuto = byte.Parse(data.Substring(10, 2));
            byte segundos = byte.Parse(data.Substring(12, 2));
            UInt16 milissegundos = UInt16.Parse(data.Substring(14, 3));

            return new DateTime(ano, mes, dia, hora, minuto, segundos, milissegundos);  
        }
        // Função que verifica se uma string está presente em um array de string
        bool BuscarPorEvento(string[] evento, long codEvento) => evento.Any(s => s.Contains(codEvento.ToString("X"))); 
    }
}