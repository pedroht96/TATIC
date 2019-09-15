using System;
using System.IO;
using BaseDados.Objetos;
using FluentAssertions;
using Xunit;

namespace BaseDados.Testes
{
    public class TesteUnitario
    {
// Teste unitario para verificar que a classe armazenador nao aceita um argumento vazio;
//Ex: digitar apenas "armazenador", o correto é: armazenador C:/Users/Pedro/Desktop/tatic/sample.txt
        [Fact]
        public void TesteArgumentoVazio()
        {
            Action a = () => new Armazenador("");

            a.Should().NotThrow<NullReferenceException>();
        }
//Teste unitario para verificar se o arquivo de entrada existe no caminho especificado.
        [Fact]
        public void TesteArquivoInexistente()
        {
            Action a = () => new Armazenador("C:/Users/Pedro/Desktop/tatic/sampl.txt");

            a.Should().NotThrow<Exception>();
        }
//Testando com o caminho do arquivo correto
        [Fact]
        public void TesteArquivoOK()
        {   
            Action a = () => new Armazenador(Path.GetFullPath("C:/Users/Pedro/Desktop/tatic/sample.txt"));

            a.Should().NotThrow<NullReferenceException>();
            a.Should().NotThrow<Exception>();
        }
//Teste unitario do buscador, esta busca deveria retornar 3 resultados, este teste verifica isso.
        [Fact]
        public void TesteBusca()
        {
            Armazenador armazenador = new Armazenador("C:/Users/Pedro/Desktop/tatic/sample.txt");
            armazenador.LerArquivo();
            Buscador buscador = new Buscador(armazenador);
            buscador.ProcurarPor("20170206175709744", "20170219124557428", new string[]{"BFD99205", "B5079387"});

            Assert.Equal(buscador.NumeroResultados, 3);    
        }
    }
}
