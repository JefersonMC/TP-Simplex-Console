using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace MetodoSimplex
{
    class Program
    {
        /// <summary>
        /// Procedimento para executar Menu;
        /// </summary>
        private static void Menu() 
        {
            bool Error = false;    //Testar variaveis informadas;
            do
            {
                Console.Clear();
                Console.WriteLine("***********************        MÉTODO SIMPLEX        ***********************");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Elaboração de um algoritmo para viabilizar a solução de muitos\nproblemas quanto á programação linear !!!");
                Console.WriteLine();
                Console.WriteLine("Programa consiste em receber um numero X de produtos e suas respectivas\nrestrições para determinar sua Função Linear.");
                Console.WriteLine();
                Console.Write("Responda de acordo com parenteses:\nVocê deseja MAXIMIZAR (max) ou MINIMIZAR (min) o problema?");
                string FO = Console.ReadLine();                     //Recebe variável respectiva a função (Não faz diferença nenhuma no algoritmo);
                Console.WriteLine();
                Console.Write("Informe o número de Produtos: ");
                int Produtos = int.Parse(Console.ReadLine());       //Recebe numero de produtos a serem analisados (x1, x2, etc);
                Console.WriteLine();
                Console.Write("Informe o número de Restrições: ");
                int Restricoes = int.Parse(Console.ReadLine());     //Recebe numero de restrições presentes no problema;
                Console.WriteLine();
                Console.WriteLine();

                //Testar variaveis informadas, caso ocorra erros repete procedimento Menu;
                #region Verificar
                if ((Produtos <= 0) || (Restricoes <= 0) || (FO != "max") && (FO != "min"))
                {
                    Console.WriteLine("********************         VALOR(ES) INCORRETO(S)         ********************");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write("Pressione uma tecla para voltar...");
                    Console.ReadKey();
                    Console.Clear();
                    Error = true;
                    goto erro;
                }
                #endregion

                int[] VetProdutos = new int[Produtos];                       //Receber valores do produto (30x1, 30x2, etc);
                string[] VetInequacao = new string[Restricoes];              //Receber valores das inequações (>= ou <=) para criar variáveis auxiliares;
                int[,] MatRestricoes = new int[Restricoes, (Produtos + 1)];  //Receber valores das inequações respectivas aos produtos (R1 = ..., R2 = ...,);
                Dados(VetProdutos, VetInequacao, MatRestricoes);             //Chama procedimento para preencher os valores acima;
            erro: Console.Clear();
            } while (Error == true);        //Tratamento de erros, repetirá o procedimento Menu se erro for verdade;
        }

        /// <summary>
        /// //Procedimento para receber dados de Produtos e Restrições;
        /// </summary>
        /// <param name="VetProdutos"> Vetor contendo o total de produtos (x1, x2, .., xn)</param>
        /// <param name="VetInequacao"> Vetor que recebe o tipo de inequação de cada restrição (R1: maior/igual, R2: menor/igual, etc)</param>
        /// <param name="MatRestricoes"> Matriz que recebe valores de suas restrições</param>
        private static void Dados(int[] VetProdutos, string[] VetInequacao, int[,] MatRestricoes) 
        {
            bool Error = false;    //Testar variaveis informadas;
            do
            {
                for (int cont = 0; cont < VetProdutos.Length; cont++)
                {
                    Console.Write("Informe valor respectivo ao " + (cont + 1) + "º produto: ");
                    VetProdutos[cont] = int.Parse(Console.ReadLine());    //Vetor recebe os valores dos produtos;
                    Console.WriteLine();

                    //Testar variaveis informadas, caso ocorra erros repete procedimento Dados;
                    #region Verificar
                    if (VetProdutos[cont] <= 0)
                    {
                        Console.Write("VALOR INCORRETO");
                        Console.ReadKey();
                        Console.Clear();
                        Error = true;
                        goto erro;
                    }
                    #endregion

                }
                Console.WriteLine();
                Console.Write("Agora informe as Restrições!");
                Console.WriteLine();
                for (int Linha = 0; Linha < MatRestricoes.GetLength(0); Linha++)    //FOR percorre Matriz que receberá as inequações => Linha1 = Restrição1: (Valorx1) | (Valorx2) | (<= ou >=) | (ValorResultante);
                {
                    Console.WriteLine();
                    Console.WriteLine((Linha + 1) + "º Restrição: ");
                    for (int cont = 0; cont < (MatRestricoes.GetLength(1) - 1); cont++)   //(MatRestricoes.GetLength(1) - 1) => total de colunas da tabela gerada do simplex - a coluna "Membro Livres" = Total de produtos informados;
                    {
                        Console.Write("Informe valor respectivo ao " + (cont + 1) + "º produto: ");
                        MatRestricoes[Linha, cont] = int.Parse(Console.ReadLine());       //Matriz recebe os valores das restrições;
                        Console.WriteLine();

                        //Testar variaveis informadas, caso ocorra erros repete procedimento Dados;
                        #region Verificar
                        if (MatRestricoes[Linha, cont] < 0)
                        {
                            Console.Write("VALOR INCORRETO");
                            Console.ReadKey();
                            Console.Clear();
                            Error = true;
                            goto erro;
                        }
                        #endregion
                    }
                    Console.Write("Informe a inequação ( '<=' ou '>=' ) dessa respectiva restrição: ");
                    VetInequacao[Linha] = Console.ReadLine();    //Vetor recebe o tipo de inequação (>= ou <=);
                    Console.WriteLine();

                    //Testar variaveis informadas, caso ocorra erros repete procedimento Dados;
                    #region Verificar
                    if ((VetInequacao[Linha] != "=>") && (VetInequacao[Linha] != ">=") && (VetInequacao[Linha] != "<=") && (VetInequacao[Linha] != "=<"))
                    {
                        Console.Write("VALOR INCORRETO");
                        Console.ReadKey();
                        Console.Clear();
                        Error = true;
                        goto erro;
                    }
                    #endregion

                    Console.Write("Informe valor resultante dessa respectiva inequação: ");
                    MatRestricoes[Linha, MatRestricoes.GetLength(1) - 1] = int.Parse(Console.ReadLine());    //Matriz recebe valor resultante da respectiva inequação;
                    Console.WriteLine();

                    //Testar variaveis informadas, caso ocorra erros repete procedimento Dados;
                    #region Verificar
                    if (MatRestricoes[Linha, MatRestricoes.GetLength(1) - 1] <= 0)
                    {
                        Console.Write("VALOR INCORRETO");
                        Console.ReadKey();
                        Console.Clear();
                        Error = true;
                        goto erro;
                    }
                    #endregion
                }
                double[,] Tabela = CriaTabela(VetProdutos, VetInequacao, MatRestricoes);  //Função que cria tabela simplex das respectivas variáveis informadas;
                
                //Ao executar Algoritmo de Troca as variáveis mudam suas posições na tabela, ficando impossível no final determinar qual valor é de qual variavel, então criei um método para gravar os valores das variaveis x1, x2, x3, etc;
                int aux = 1;    //Variavel auxiliar para verificar os valores das variaveis básicas na tabela simplex;
                string[,] ValorVariaveis = new string[2, (Tabela.GetLength(0) - 1) + (Tabela.GetLength(1) - 1)];   //Matriz que recebe as variaveis e seus respectivos valores, retirando da Tabela SImplex;
                for (int cont = 0; cont < ValorVariaveis.GetLength(1); cont++)
                {
                    ValorVariaveis[0, cont] = "X" + (cont + 1);                         //Adicionando as variaveis na matriz na linha1;
                    if (cont < (Tabela.GetLength(1) - 1))
                        ValorVariaveis[1, cont] = string.Concat(Tabela[0, cont + 1]);   //Adicionando na linha2 os valores respectivos as variaveis não-básicas;
                    else
                    {
                        ValorVariaveis[1, cont] = string.Concat(Tabela[aux, 0]);        //Adicionando na linha2 os valores respectivos as variaveis básicas;
                        aux++;
                    }
                }
                AnaliseFase1(Tabela, ValorVariaveis);    //Chama primeiro procedimento de analise da tabela simplex;
            erro: Console.Clear();
            } while (Error == true);    //Tratamento de erros, repetirá o procedimento Dados se erro for verdade;
        }

        /// <summary>
        /// //Função que cria uma Tabela simplex, recebendo os respectivos valores informados no procedimento Dados;
        /// </summary>
        /// <param name="VetProdutos"> Vetor contendo o total de produtos (x1, x2, .., xn)</param>
        /// <param name="VetInequacao"> Vetor que recebe o tipo de inequação de cada restrição (R1: maior/igual, R2: menor/igual, etc)</param>
        /// <param name="MatRestricoes"> Matriz que recebe valores de suas restrições</param>
        /// <returns> Retorna uma Tabela Simplex com os valores informados pelo usuario</returns>
        static double[,] CriaTabela(int[] VetProdutos, string[] VetInequacao, int[,] MatRestricoes) 
        {
            double[,] Tabela = new double[VetInequacao.Length + 1, VetProdutos.Length + 1];  // +1 referem-se a linha do "f(x)" e a coluna "Membro Livre" respectivamente;
            for (int Coluna = 0; Coluna < Tabela.GetLength(1); Coluna++)      //FOR para preencher linha "f(x)" e valores dos produtos;
            {
                if (Coluna == 0)
                    Tabela[0, 0] = 0;
                else
                    Tabela[0, Coluna] = VetProdutos[Coluna - 1];
            }
            for (int Restricao = 0; Restricao < VetInequacao.Length; Restricao++)             // Preencher Variaveis basicas e não basicas; 
                if ((VetInequacao[Restricao] == "<=") || (VetInequacao[Restricao] == "=<"))   //Necessário realizar transformações dos valores das variaveis caso inequação seja menor ou igual;
                {
                    for (int cont = 0; cont < MatRestricoes.GetLength(1); cont++)
                    {
                        if (cont == 0)
                            Tabela[Restricao + 1, cont] = MatRestricoes[Restricao, MatRestricoes.GetLength(1) - 1];
                        else
                            Tabela[Restricao + 1, cont] = MatRestricoes[Restricao, cont - 1];
                    }
                }
                else
                {
                    for (int cont = 0; cont < MatRestricoes.GetLength(1); cont++)
                    {
                        if (cont == 0)
                            Tabela[Restricao + 1, cont] = -(MatRestricoes[Restricao, MatRestricoes.GetLength(1) - 1]);
                        else
                            Tabela[Restricao + 1, cont] = -(MatRestricoes[Restricao, cont - 1]);
                    }
                }
            return Tabela;     //Retorna Tabela Simplex;
        }

        /// <summary>
        /// Procedimento para a primeira analise da Tabela Simplex;
        /// </summary>
        /// <param name="Tabela"> Tabela Simplex gerada na função CriaTabela</param>
        /// <param name="ValorVariaveis"> Matriz que contém valores das respectivas variaveis (linha1: x1, x2, etc; linha2: 20, 30, etc)</param>
        private static void AnaliseFase1(double[,] Tabela, string[,] ValorVariaveis)
        {
            bool EstadoAtipico = false;    //Variavel que verifica se tabela vai gerar um estado diferente do 'caminho perfeito', ou seja, se recebera respostas diferentes da Solução Ótima; 
            int controle = 0, ControleLinha = 0, ControleColuna = 0;
            //Controle: Verificar estados da tabela (Seguir segunda etapa, Solução Impossível ou Algoritmo de troca);
            //ControleLinha: Verifica se existe valor negativo na Coluna "Menbro Livre";
            //ControleLinha(depois)/ControleColuna: Variaveis que contem valor da Linha Permissiva e Coluna Permissiva;
            double EP = 0, VerificaEP = Tabela[1, 0];
            //EP: Elemento Permitido;
            //VerificaEP: Verifica qual é o valor permissivo na tabela, recebendo maior valor na tabela para a verificação mais a frente;
            if (VerificaEP < 0)      //Caso valor recebido de VerificaEP seja negativo, necessario coloca-lo positivo para verificação;
                VerificaEP = -(VerificaEP);
            double[] VetLinhaPermitida = new double[Tabela.GetLength(1)];    //Vetor com valores da Linha Permitida (Membro Livre, Variaveis Não Basicas);
            double[] VetColunaPermitida = new double[Tabela.GetLength(0)];   //Vetor com valores da Coluna Permitida (f(x), Variaveis Basicas);
            Console.Clear();
            Console.WriteLine("**********************        1ª FASE DO MÉTODO        **********************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Pressione uma tecla para processar a primeira fase do método !!!");
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();
            for (int Linha = 1; Linha < Tabela.GetLength(0); Linha++)   //Verificar se existe valor negativo na coluna Membro Livre;
                if (Tabela[Linha, 0] < 0)
                {
                    controle = 0;
                    ControleLinha = Linha;                 //Necessário posteriormente verificar a Linha que ouver valores negativos;
                }
                else
                    controle = 1;
            if ((controle == 1) && (ControleLinha == 0))   //Se não ouver valores negativos na coluna Membro Livre, necessário passar para segunda fase de analise da Tabela Simplex;
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Não foi encontrado valores negativos na coluna de Menbros Livres !!!");
                Console.WriteLine();
                Console.Write("Pressione uma tecla para prosseguir á próxima etapa.");
                Console.ReadKey();
                Console.Clear();
                AnaliseFase2(Tabela, ValorVariaveis);      //Chama segundo procedimento de analise da Tabela Simplex; 
            }
            else
            {
                controle = 0;
                for (int Produtos = 0; Produtos < Tabela.GetLength(1) - 1; Produtos++)  //FOR para verificar se Linha contem algum valor negativo para Variaveis Não Basicas;
                {
                    if (Tabela[ControleLinha, Produtos + 1] < 0)   //Se ouver valor negativo na Linha encontrada, armazenar a Coluna Permissiva;
                    {
                        controle = 0;                              //Verificar se solução é impossivel ou necessário chamar Algoritmo de Troca;
                        for (int cont = 0; cont < VetColunaPermitida.Length; cont++)
                        {
                            VetColunaPermitida[cont] = Tabela[cont, Produtos + 1];   //Adiciona valores da Coluna Permissiva respectivas ao f(x) e Variaveis Basicas;
                            ControleColuna = (Produtos + 1);                         //Adiciona valor da Coluna Permissiva;
                        }
                    }
                    else
                        controle = controle + 2;
                }
                if (controle / (Tabela.GetLength(1) - 1) == 2)   //Se Linha não conter nenhum valor negativo para Variaveis Não Basicas, não existe Solução Otima;
                {
                    EstadoAtipico = true;    //Variavel verificadora de estado da Tabela Simplex é ativado;
                    Console.WriteLine("************        SOLUÇÃO IMPOSSÍVEL (PERMISSIVA NÃO EXISTE)        ************");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Ao procurarmos algum elemento negativo na linha que corresponde à variável\ncom membro livre negativo encontrou-se elementos positivos ou iguais a 0 !!!");
                    Console.WriteLine();
                    Console.Write("Pressione uma tecla para continuar!");
                    Console.ReadKey();
                    Console.Clear();
                    Resultado(Tabela, ValorVariaveis, EstadoAtipico);       //Chama procedimento para demonstrar Tabela Simplex obtida;
                }
                else
                {
                    for (int cont = 1; cont < Tabela.GetLength(0); cont++)  //Verificar a Linha Permitida e adicionar seus valores no vetor, além de encontrar o EP;
                    {
                        if (VerificaEP > Tabela[cont, 0] / VetColunaPermitida[cont])
                        {
                            VerificaEP = Tabela[cont, 0] / VetColunaPermitida[cont];
                            EP = VetColunaPermitida[cont];                  //Adiciona o EP (Elemento Permissivo);
                            for (int cont2 = 0; cont2 < VetLinhaPermitida.Length; cont2++)
                            {
                                VetLinhaPermitida[cont2] = Tabela[cont, cont2];  //Adiciona valores da Linha Permissiva respectivas ao Menbro Livre e Variaveis Não Basicas;
                                ControleLinha = cont;                            //Adiciona valor da Linha Permissiva;
                            }
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Algoritmo de Troca será executado.");
                    Console.WriteLine();
                    Console.Write("Precione uma tecla para prosseguir !!!");
                    Console.ReadKey();
                    AlgoritmoTroca(Tabela, VetLinhaPermitida, VetColunaPermitida, ValorVariaveis, EP, ControleLinha, ControleColuna);  //Chamar procedimento do Algoritmo de Troca da Tabela Simplex;
                    AnaliseFase1(Tabela, ValorVariaveis);    //Necessário analisar a Nova Tabela antes de passar para 2ª fase;
                }
            }
        }
        
        /// <summary>
        /// Procedimento para a primeira analise da Tabela Simplex;
        /// </summary>
        /// <param name="Tabela"> Tabela Simplex gerada na função CriaTabela</param>
        /// <param name="ValorVariaveis"> Matriz que contém valores das respectivas variaveis (linha1: x1, x2, etc; linha2: 20, 30, etc)</param>
        private static void AnaliseFase2(double[,] Tabela, string[,] ValorVariaveis)
        {
            bool EstadoAtipico = false;    //Variavel que verifica se tabela vai gerar um estado diferente do 'caminho perfeito', ou seja, se recebera respostas diferentes da Solução Ótima;
            int controle = 0, ControleLinha = 0, ControleColuna = 0;
            //Controle: Verificar estados da tabela (Seguir segunda etapa, Solução Impossível ou Algoritmo de troca);
            //ControleLinha: Verifica se existe valor negativo na Coluna "Menbro Livre";
            //ControleLinha(depois)/ControleColuna: Variaveis que contem valor da Linha Permissiva e Coluna Permissiva;
            double EP = 0, VerificaEP = Tabela[0, 0];
            //EP: Elemento Permitido;
            //VerificaEP: Verifica qual é o valor permissivo na tabela, recebendo maior valor na tabela para a verificação mais a frente;
            if (VerificaEP < 0)      //Caso valor recebido de VerificaEP seja negativo, necessario coloca-lo positivo para verificação;
                VerificaEP = -(VerificaEP);
            double[] VetLinhaPermitida = new double[Tabela.GetLength(1)];    //Vetor com valores da Linha Permitida (Membro Livre, Variaveis Não Basicas);
            double[] VetColunaPermitida = new double[Tabela.GetLength(0)];   //Vetor com valores da Coluna Permitida (f(x), Variaveis Basicas);
            Console.Clear();
            Console.WriteLine("**********************        2ª FASE DO MÉTODO        **********************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Pressione uma tecla para processar a segunda fase do método !!!");
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();
            for (int Coluna = 1; Coluna < Tabela.GetLength(1); Coluna++)     //Necessário verificar estado da Tabela;
            {
                if (Tabela[0, Coluna] > 0)         //Verificar se existe valores positivos na Linha f(x)
                {
                    controle = 0;
                    ControleColuna = Coluna;       //Necessário verificar Coluna que ouver valores negativos, além de armazenar o valor da Coluna Permissiva;
                }
                else if (Tabela[0, Coluna] == 0)   //Verificar se existe valores nulos na Linha f(x)
                    controle = -1;
                else                               //Verificar se existe valores negativos na Linha f(x)
                    controle = 1;
            }
            if ((controle == 1) && (ControleColuna == 0))    //Se existir valores negativos na linha f(x) solução ótima foi encontrada; 
            {
                Console.WriteLine("**********************        SOLUÇÃO ÓTIMA OBTIDA        **********************");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Foi encontrado a tabela que corresponde á solução ótima !!!");
                Console.WriteLine();
                Console.Write("Pressione uma tecla para continuar!");
                Console.ReadKey();
                Console.Clear();
                Resultado(Tabela, ValorVariaveis, EstadoAtipico);  //Chama procedimento para demonstrar Tabela Simplex obtida;
            }
            else if ((controle == -1) && (ControleColuna == 0))    //Se ouver valores nulos na linha f(x) então Tabela tem multiplas soluções;
            {
                EstadoAtipico = true;     //Variavel verificadora de estado da Tabela Simplex é ativado;
                Console.WriteLine("******************        MÚLTIPLAS SOLUÇÕES        ******************");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Foi encontrado elemento zero (não consideramos o membro livre) na linha F(x) !!!");
                Console.WriteLine();
                Console.Write("Pressione uma tecla para continuar!");
                Console.ReadKey();
                Console.Clear();
                Resultado(Tabela, ValorVariaveis, EstadoAtipico); //Chama procedimento para demonstrar Tabela Simplex obtida;
            }
            else
            {
                controle = 0;
                for (int cont = 0; cont < VetColunaPermitida.Length; cont++)  //FOR para preencher vetor com valores da Coluna Permissiva;
                    VetColunaPermitida[cont] = Tabela[cont, ControleColuna];
                for (int cont = 1; cont < Tabela.GetLength(0); cont++)        //FOR para verificar se Coluna Permissiva contem algum valor negativo, além de encontrar o EP;
                {
                    if (VetColunaPermitida[cont] < 0)
                        controle = controle + 2;
                    else
                    {
                        if (VerificaEP > Tabela[cont, 0] / VetColunaPermitida[cont])
                        {
                            VerificaEP = Tabela[cont, 0] / VetColunaPermitida[cont];
                            EP = VetColunaPermitida[cont];                        //Adiciona o EP (Elemento Permissivo);
                            for (int cont2 = 0; cont2 < VetLinhaPermitida.Length; cont2++)
                            {
                                VetLinhaPermitida[cont2] = Tabela[cont, cont2];   //Adiciona valores da Linha Permissiva respectivas ao Menbro Livre e Variaveis Não Basicas;
                                ControleLinha = cont;                             //Adiciona valor da Linha Permissiva;
                            }
                        }
                    }
                }
                if (controle / (Tabela.GetLength(0) - 1) == 2)    //Se ouver valores positivos no Membro Livre então Solução é Ilimitada;
                {
                    EstadoAtipico = true;    //Variavel verificadora de estado da Tabela Simplex é ativado;
                    Console.WriteLine("******************        SOLUÇÃO ILIMITADA        ******************");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Na coluna permitida encontrada, todos os elementos correspondentes ás variáveis básicas são negativas !!!");
                    Console.WriteLine();
                    Console.Write("Pressione uma tecla para continuar!");
                    Console.ReadKey();
                    Console.Clear();
                    Resultado(Tabela, ValorVariaveis, EstadoAtipico);  //Chama procedimento para demonstrar Tabela Simplex obtida;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Algoritmo de Troca será executado.");
                    Console.WriteLine();
                    Console.Write("Precione uma tecla para prosseguir !!!");
                    Console.ReadKey();
                    AlgoritmoTroca(Tabela, VetLinhaPermitida, VetColunaPermitida, ValorVariaveis, EP, ControleLinha, ControleColuna);  //Chamar procedimento do Algoritmo de Troca da Tabela Simplex;
                    AnaliseFase2(Tabela, ValorVariaveis);    //Necessário analisar a Nova Tabela para encontrar o resultado final da Tabela Simplex;
                }
            }
        }

        /// <summary>
        /// Algoritmo de Troca da Tabela Simplex;
        /// </summary>
        /// <param name="Tabela"> Tabela Simplex verificada nos procedimentos Fase1 e Fase2</param>
        /// <param name="VetLinhaPermitida"> Linha Permissiva obtida na tabela</param>
        /// <param name="VetColunaPermitida"> Coluna Permissiva obtida na tabela</param>
        /// <param name="ValorVariaveis"> Matriz com variaveis e respectivos valores</param>
        /// <param name="EP"> Elemento Permitido</param>
        /// <param name="ControleLinha"> Variavel que contem valor da Linha Permissiva</param>
        /// <param name="ControleColuna"> Variavel que contem valor da Coluna Permissiva</param>
        private static void AlgoritmoTroca(double[,] Tabela, double[] VetLinhaPermitida, double[] VetColunaPermitida, string[,] ValorVariaveis, double EP, int ControleLinha, int ControleColuna)
        {
            double[] SubCelulasColuna = new double[VetColunaPermitida.Length];             //Vetor que contem os valores da sub-células Inferiores da Coluna Permitida;
            double[,] NovaTabela = new double[Tabela.GetLength(0), Tabela.GetLength(1)];   //Matriz que receberá os novos valores advindos dos cálculos da Tabela Simplex recebida;
            for (int L = 0; L < Tabela.GetLength(0); L++)      //FOR que preenche o vetor SubCelulaColuna e parte da Nova Tabela Simplex;
            {
                for (int C = 0; C < Tabela.GetLength(1); C++)  //Preenchimento da Linha Permitida e Coluna Permitida da Nova Tabela SImplex; 
                {
                    if ((Tabela[L, C] == VetLinhaPermitida[C]) && (ControleLinha == L))         //Multiplica-se toda a linha pelo EP Inverso;
                        NovaTabela[L, C] = VetLinhaPermitida[C] * (1 / EP);
                    if ((Tabela[L, C] == VetColunaPermitida[L]) && (ControleColuna == C))       //Multiplica-se toda a coluna pelo - (EP Inverso)
                    {
                        NovaTabela[L, C] = VetColunaPermitida[L] * -(1 / EP);
                        SubCelulasColuna[L] = NovaTabela[L, C];
                    }
                    if ((Tabela[L, C] == EP) && (ControleLinha == L) && (ControleColuna == C))  //Calcula-se o inverso do Elemento Permitido;
                    {
                        NovaTabela[L, C] = 1 / EP;
                        SubCelulasColuna[L] = NovaTabela[L, C];
                    }
                }
            }
            for (int L = 0; L < Tabela.GetLength(0); L++)      //FOR que termina de preencher a Nova Tabela Simplex utilizando dos valores da SubCelula;
            {
                for (int C = 0; C < Tabela.GetLength(1); C++)
                {
                    if ((C != ControleColuna) && (L != ControleLinha))   //IF que verifica se célula da Nova Tabela ja foi preenchida anteriormente, se estiver vazia então executa os procedimento;
                    {
                        NovaTabela[L, C] = SubCelulasColuna[L] * VetLinhaPermitida[C];  //Multiplica-se a (SCS) marcada em sua respectiva coluna com a (SCI) marcada de sua respectiva linha;
                        NovaTabela[L, C] = Tabela[L, C] + NovaTabela[L, C];             //Somam-se as (SCI) com as (SCS) das demais células restantes da tabela original;
                    }
                }
            }
            for (int L = 0; L < Tabela.GetLength(0); L++)      //FOR repassa novos valores obtidos para a Tabela Simplex original;
                for (int C = 0; C < Tabela.GetLength(1); C++)
                    Tabela[L, C] = NovaTabela[L, C];

            //Após executar procedimento de troca necessário trocar de posição a variável não básica com a variável básica, ambas definidas como “Permitidas na tabela anterior;
            int aux = 1;            //Variavel auxiliar para verificar os valores das variaveis básicas na tabela simplex;
            string variavel = "";   //Variavel auxiliar que receberá a variavel da Coluna Permitida que será trocada pela variavel da Linha Permitida;
            for (int cont = 0; cont < ValorVariaveis.GetLength(1); cont++)
            {
                if (cont < (Tabela.GetLength(1) - 1))
                    ValorVariaveis[1, cont] = string.Concat(Tabela[0, cont + 1]);   //Adicionando na linha2 os valores respectivos as variaveis não-básicas;
                else
                {
                    ValorVariaveis[1, cont] = string.Concat(Tabela[aux, 0]);        //Adicionando na linha2 os valores respectivos as variaveis básicas;
                    aux++;
                }
            }
            if (Tabela[0, ControleColuna] == double.Parse(ValorVariaveis[1, ControleColuna - 1]))   //Verifica valor da variavel não basica que será trocada;
            {
                aux = ControleColuna - 1;            //'-1' para eliminar a coluna "Membro Livre", aux recebe o valor da coluna que contem a variavel que será trocada;
                variavel = ValorVariaveis[0, aux];   //Variavel auxiliar recebe a variavel não basica que será trocada;
            }
            for (int cont = 0; cont < ValorVariaveis.GetLength(1); cont++)                          //Verifica valor da variavel basica que será trocada;
            {
                if (Tabela[ControleLinha, 0] == double.Parse(ValorVariaveis[1, cont]) && (cont >= (Tabela.GetLength(1) - 1)))
                {
                    ValorVariaveis[0, aux] = ValorVariaveis[0, cont];  //Troca da variavel nao basica pela basica;
                    aux = cont;                                        //aux recebe o valor da coluna que contem a variavel que será trocada;
                }
            }
            ValorVariaveis[0, aux] = variavel;   //Troca da variavel basica pela não basica;

            //Verificar os novos valores das respectivas variaveis armazenadas na matriz ValorVariaveis;
            Console.WriteLine();
            Console.WriteLine("Impressão dos novos valores das variáveis!");
            for (int L = 0; L < ValorVariaveis.GetLength(0); L++)
            {
                Console.WriteLine();
                for (int C = 0; C < ValorVariaveis.GetLength(1); C++)
                    Console.Write(ValorVariaveis[L, C] + "   ");
            }
            Console.WriteLine();
            Console.Write("Precione uma tecla para prosseguir !!!");
            Console.ReadKey();
        }
       
        /// <summary>
        /// Procedimento que demonstra resultado obtido na Tabela;
        /// </summary>
        /// <param name="Tabela"> Tabela Simplex</param>
        /// <param name="ValorVariaveis"> Matriz que contém valores das respectivas variaveis</param>
        /// <param name="EstadoAtipico"> Variavel que verifica estado da Tabela Simplex (Solução Otima obtida ou não)</param>
        private static void Resultado(double[,] Tabela, string[,] ValorVariaveis, bool EstadoAtipico)
        {
            if (EstadoAtipico == true)  //Se nos procedimentos de analises foi encontrado algo átipico na Tabela Simplex demontrar a tabela sem resultados;
            {
                Console.WriteLine("********************        DEMONSTRAÇÃO TABELA        ********************");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Estado atípico ocorrido !!!");
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("============================================================================");
                for (int L = 0; L < Tabela.GetLength(0); L++)     //FOR que demonstra-ra o resultado final da Tabela Simplex;
                {
                    Console.WriteLine();
                    for (int C = 0; C < Tabela.GetLength(1); C++)
                        Console.Write(Tabela[L, C] + "        ");
                }

                Console.WriteLine();
                Console.WriteLine();
                bool Error = false;
                do
                {
                    Console.Write("Deseja repetir programa? (sim/s ou não/n)");
                    string repetir = Console.ReadLine();

                    //Testar variaveis informadas, caso ocorra erros repete procedimento de repetir;
                    #region Verificar
                    if ((repetir != "sim") && (repetir != "s") && (repetir != "n") && (repetir != "não"))
                    {
                        Console.WriteLine("********************         VALOR(ES) INCORRETO(S)         ********************");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write("Pressione uma tecla para voltar...");
                        Console.ReadKey();
                        Console.Clear();
                        Error = true;
                        goto erro;
                    }
                    #endregion

                    if ((repetir == "sim") || (repetir == "s") || (repetir == "S"))   //Verificar informação do usuário para repetir ou não o programa;
                        Menu();
              erro: Console.Clear();
                } while (Error == true);
            }
            else      //Se nos procedimentos de analises não foram encontrados outras soluções da Tabela Simplex, devemos demontrar a tabela e os resultados (caminho feliz);
            {
                Console.WriteLine("********************        DEMONSTRAÇÃO TABELA        ********************");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("============================================================================");

                for (int L = 0; L < Tabela.GetLength(0); L++)           //FOR que demonstra-ra o resultado final da Tabela Simplex;
                {
                    Console.WriteLine();
                    for (int C = 0; C < Tabela.GetLength(1); C++)
                        Console.Write(Tabela[L, C] + "        ");
                }

                Console.WriteLine();
                Console.WriteLine("RESPOSTAS:  Z = " + -(Tabela[0, 0]) + ";");
                Console.WriteLine();
                for (int L = 0; L < ValorVariaveis.GetLength(0); L++)   //FOR que demonstra os valores finais de cada variavel, lembrando que se valor for negativo deverá anular;
                {
                    Console.WriteLine();
                    for (int C = 0; C < ValorVariaveis.GetLength(1); C++)
                        if (int.Parse(ValorVariaveis[L, C]) < 0)
                            Console.Write(0 + "     |     ");
                        else
                            Console.Write(ValorVariaveis[L, C] + "     |     ");
                }
                Console.WriteLine();
                Console.WriteLine();
                bool Error = false;
                do
                {
                    Console.Write("Deseja repetir programa? (sim/s ou não/n)");
                    string repetir = Console.ReadLine();

                    //Testar variaveis informadas, caso ocorra erros repete procedimento de repetir;
                    #region Verificar
                    if ((repetir != "sim") && (repetir != "s") && (repetir != "n") && (repetir != "não"))
                    {
                        Console.WriteLine("********************         VALOR(ES) INCORRETO(S)         ********************");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write("Pressione uma tecla para voltar...");
                        Console.ReadKey();
                        Console.Clear();
                        Error = true;
                        goto erro;
                    }
                    #endregion

                    if ((repetir == "sim") || (repetir == "s") || (repetir == "S"))   //Verificar informação do usuário para repetir ou não o programa;
                        Menu();
              erro: Console.Clear();
                } while (Error == true);
            }
        }
        static void Main(string[] args)
        {
            bool Error = false;   //Tratamento de erros;
            do
            {
                try               //Tratamento de erros em todo o programa;
                {
                    Menu();       //Chama procedimento Menu;
                }

                #region Exception
                catch (Exception E)    //Recebe erro encontrado no programa e demonstra ao usuário;
                {
                    Console.Clear();
                    Console.WriteLine("***********************         ERROR         ***********************");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine(E.Message);     //Amostragem da mensagem de erro;
                    Console.WriteLine();
                    Console.Write("Pressione uma tecla para voltar...");
                    Console.ReadKey();
                    Error = true;
                }
                #endregion

            } while (Error == true);    //Tratamento de erros, repetirá o programa se erro for verdade;
        }
    }
}
