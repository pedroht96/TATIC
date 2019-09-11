using System;
using System.Collections.Generic;

namespace BaseDados.Objetos
{
    public class Registros
    {
        /*  Um dicionaro de dicionario onde o mais externo contém um numero que representa o indice que será escolhido em codigo
            E um mais interno que éuma representação do evento, um evento está associado a mais de um usuario, portanto será um
            indice secundario
        */
        private Dictionary<long, Dictionary<long, List<Registro>>> Objetos{ get; set; }


        public Dictionary<long, Dictionary<long, List<Registro>>> EventoRegistros 
        {
            get
            {
                return Objetos;
            }
        }

        public Registros()
        {
            Objetos = new Dictionary<long, Dictionary<long, List<Registro>>>();
        }

        public void AdicionarRegistro(string DataEvento, string CodigoEvento, string CodigoUsuario)
        {   
            UInt16 ano = UInt16.Parse(DataEvento.Substring(0, 4)); /* Para representar o ano utilizamos 16 bits 0 - 65535 */
            byte mes = byte.Parse(DataEvento.Substring(4, 2)); /* Para o mês utilizamos 8bits 0 - 255 */
            byte dia = byte.Parse(DataEvento.Substring(6, 2)); /* O mesmo para o dia */
            byte hora = byte.Parse(DataEvento.Substring(8, 2)); /*O mesmo para hora */
            byte minuto = byte.Parse(DataEvento.Substring(10, 2)); /* O mesmo para minuto */
            byte segundos = byte.Parse(DataEvento.Substring(12, 2)); /* O mesmo para segundo */
            UInt16 milissegundos = UInt16.Parse(DataEvento.Substring(14, 3)); /* Milissegundos vai de 0 - 999, portanto 16bits */
            long codEvento = Int64.Parse(CodigoEvento, System.Globalization.NumberStyles.HexNumber); /* Codigo do evento 
            representando em valor numero, utilizando apenas 32 bits, com string seria necessario 64bits */
        
            DateTime data = new DateTime(ano, mes, dia, hora, minuto, segundos, milissegundos);

            Registro registro = new Registro(data.Ticks, ulong.Parse(CodigoUsuario)); // Cria um registro
            
            // Indexação
            DateTime DateDictionary = new DateTime(ano, mes, dia, hora, 0, 0);

            Dictionary<long, List<Registro>> DicRegistros;
            /* Datetime.Ticks = representação numerica da data
             */
            if(Objetos.TryGetValue(DateDictionary.Ticks, out DicRegistros)) // Verifica se aquela data ja foi indexada
            {                                                               // Caso afirmativo referencia ela com DicRegistros
                List<Registro> ListRegistros; 
                
                if(DicRegistros.TryGetValue(codEvento, out ListRegistros)) // Verifica se um evento ja foi indexado
                {                                                          // Caso afirmativa referencia com ListRegistro
                    ListRegistros.Add(registro); // Adiciona o registro
                }
                else
                {
                    ListRegistros = new List<Registro>();
                    ListRegistros.Add(registro); 
                    DicRegistros.TryAdd(codEvento, ListRegistros);
                    // Caso o evento não tenha sido indexado, ele é indexado e adicionado o registro
                }
            }
            else // Se a data não foi indexada, ela é feita, bem como o evento a ser lido e posteriormente adicionado o registro
            {
                var ListRegistros = new List<Registro>();
                ListRegistros.Add(registro);
                var DicEventos = new Dictionary<long, List<Registro>>();
                DicEventos.Add(codEvento, ListRegistros);
                Objetos.Add(DateDictionary.Ticks, DicEventos);
            }

        }
        /* Função que conta quantidade de registros */
        public int GetNumeroRegistros()
        {
            int i = 0;
            
            foreach(var data in Objetos)
            {
                foreach (var registros in data.Value)
                {
                    i += registros.Value.Count;
                }
            }
            
            return i;
        }
        /* Função que retorna quantidade de indeces criado */
        public int GetNumeroRegistrosIndexados()
        {
            return Objetos.Count;
        }

        // Utilizado para retornar o registro e não permitir que ele seja modificado de fora
        public Dictionary<long, Dictionary<long, List<Registro>>> GetRegistros()
        {
            return EventoRegistros;
        }

    }
}