__**Execuçao do codigo:**__

Para executar o codigo é nescessario a ferramenta SDK instalado na maquina, caso nao possua:
https://dotnet.microsoft.com/download/dotnet-core/2.2

Caso tenha o Visual Studio em sua maquina, basta abrir o arquivo BaseDados.csproj e executar o codigo. Caso contrario, é possivel executar pelo prompt de comando, com a seguinte comando: dotnet run -- project (caminho que se encontra o arquivo BaseDados.csproj)

Exemplo: dotnet run --projetct C:\Users\Pedro\Desktop\tatic\BaseDados.csproj

__**Funcionamento do programa:**__

1)Após a execução do programa o usuario deve digitar: armazenador (caminho que se encontra o arquivo .txt para ser lido) com um espaçamento;

2)Quando o programa terminar de ler o arquivo a linha sera quebrada, e assim podera prosseguir com a execução do programa;

3)Para acionar a função "buscador" digite buscador (primeira data) (segunda data) (identificador do usuario(opcional)) e as datas dentro do itervalo digitado serão exibidas no console.

__**Explicações técnicas do código:**__

1)Para a indexação foi indexado primeiro cada data contida no arquivo e convertido para o tipo de data DateTime.Ticks(representação numerica da data) - arquivo Registro.cs

2)Foi feito um dicionaro de dicionario onde o mais externo contém um numero que representa o indice que será escolhido em codigo e um mais interno que é uma representação do evento, um evento está associado a mais de um usuario, portanto será um indice secundario.No armazenamento dos dados no disco, as variavéis foram escolhidas de acordo com cada registro, para o ano e milissegundos foi usado o tipo "UInt16" e os demais foi representa por tipo "byte" - arquivo Registros.cs

3)Para o buscador o programa converte a data inicial e final para "Ticks", assim eu tenho dois numeros inteiros que consigo comparar e fazer uma busca no intervalo das datas - arquivo buscador.cs.


__**Testes automatizados:**__

A ferramenta escolhida para os testes foi a Xunit, propria do dotnet.
Para executar o codigo, é possivel executar todos os testes de uma vez como um por vez clicando no botao emcima de cada metodo "Run test"
A descrição de cada teste se encontra no codigo.
