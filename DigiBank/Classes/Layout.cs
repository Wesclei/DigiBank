using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public class Layout
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();
        private static int opcao = 0;
        public static void TelaPrincipal() 
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
                        
            Console.Clear();

            Console.WriteLine("                                                             ");
            Console.WriteLine("                   Digite a Opção desejada:                  ");
            Console.WriteLine("                  ==============================             \n");
            Console.WriteLine("                   1 - Criar Conta                           ");
            Console.WriteLine("                  ==============================             ");
            Console.WriteLine("                   2 - Entrar com o CPF e Senha              ");
            Console.WriteLine("                  ==============================             ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaCriarConta();
                    break;
                case 2:
                    TelaLogin();
                    break;
                default:
                    Console.WriteLine("Opção Inválida !");
                    break;
            }
        }

        private static void TelaCriarConta()
        {
            Console.Clear();

            Console.WriteLine("                                                             ");
            Console.WriteLine("                   Digite seu nome:                          ");
            string nome = Console.ReadLine();
            Console.WriteLine("                  ==============================             ");
            Console.WriteLine("                   Digite seu CPF:                           ");
            string cpf = Console.ReadLine();
            Console.WriteLine("                  ==============================             ");
            Console.WriteLine("                   Digite sua senha:                         ");
            string senha = Console.ReadLine();
            Console.WriteLine("                  ==============================             ");

            //Console.WriteLine(nome);
            //Console.WriteLine(cpf);
            //Console.WriteLine(senha);

            // Criar a conta
            ContaCorrente contaCorrente = new ContaCorrente();
            Pessoa pessoa = new Pessoa();

            pessoa.SetNome(nome);
            pessoa.SetCPF(cpf);
            pessoa.SetSenha(senha);
            pessoa.Conta = contaCorrente;

            pessoas.Add(pessoa);

            Console.Clear();

            Console.WriteLine("                   Conta cadastrada com sucesso !              ");
            Console.WriteLine("                  ================================             ");

            // espera 1 segundo pra ir para a TelaContaLogada
            Thread.Sleep(100);

            TelaContaLogada(pessoa);

        }

        private static void TelaLogin()
        {
            Console.Clear();

            Console.WriteLine("                                                             ");
            Console.WriteLine("                   Digite seu CPF:                           ");
            string cpf = Console.ReadLine();
            Console.WriteLine("                  ==============================             ");
            Console.WriteLine("                   Digite sua senha:                         ");
            string senha = Console.ReadLine();
            Console.WriteLine("                  ==============================             ");

            // Logar no sistema
            Pessoa pessoa = pessoas.FirstOrDefault(x => x.CPF == cpf && x.Senha == senha);

            if(pessoa != null)
            {
                TelasBoasVindas(pessoa);
                TelaContaLogada(pessoa);

            }
            else
            {
                Console.Clear();

                Console.WriteLine("                   Pessoa Não Cadastrada                     ");                
                Console.WriteLine("                  ==============================             " + "\n\n");


            }
        }

        private static void TelasBoasVindas(Pessoa pessoa)
        {
            string msgTelaBoasVindas = $"{pessoa.Nome} | Banco: {pessoa.Conta.GetCodBanco()} " +
                                       $"| Agência: {pessoa.Conta.GetNumAgencia()} | Conta: {pessoa.Conta.GetNumConta()}";

            Console.WriteLine("\n\n");
            Console.WriteLine($"                  Seja bem vindo {msgTelaBoasVindas}");            
            Console.WriteLine("\n");

        }

        private static void TelaContaLogada(Pessoa pessoa)
        {
            Console.Clear();

            TelasBoasVindas(pessoa);

            Console.WriteLine("                   Digite a Opção desejada:                  ");
            Console.WriteLine("                  ==============================             \n");
            Console.WriteLine("                   1 - Realizar Deposito                     ");
            Console.WriteLine("                  ==============================             ");
            Console.WriteLine("                   2 - Realizar Saque                        ");
            Console.WriteLine("                  ==============================             ");
            Console.WriteLine("                   3 - Consultar Saldo                       ");
            Console.WriteLine("                  ==============================             ");
            Console.WriteLine("                   4 - Extrato                               ");
            Console.WriteLine("                  ==============================             ");
            Console.WriteLine("                   5 - Sair                                  ");
            Console.WriteLine("                  ==============================             ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaDeposito(pessoa);
                    break;
                case 2:
                    TelaDeSaque(pessoa);
                    break;
                case 3:
                    TelaSaldo(pessoa);
                    break;
                case 4:
                    TelaExtrato(pessoa);
                    break;
                case 5:
                    TelaPrincipal();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\n                  Opção inválida !                          ");
                    Console.WriteLine("                  ==============================             ");
                    break;
            }

        }

        private static void TelaDeposito(Pessoa pessoa)
        {
            Console.Clear();

            TelasBoasVindas(pessoa);

            Console.WriteLine("\n                   Digite o valor do deposito:               ");
            double valorDep = double.Parse(Console.ReadLine());
            Console.WriteLine("                  ==============================            \n ");

            pessoa.Conta.Deposita(valorDep);

            Console.Clear();

            TelasBoasVindas(pessoa);

            Console.WriteLine("\n\n                  Depósito realizado com sucesso !               ");
            Console.WriteLine("                 ==================================             \n\n");

            OpcaoVoltarLogado(pessoa);

        }

        private static void TelaDeSaque(Pessoa pessoa)
        {
            Console.Clear();

            TelasBoasVindas(pessoa);

            Console.WriteLine(" \n                  Digite o valor do saue:                   ");
            double valorSa = double.Parse(Console.ReadLine());
            Console.WriteLine("                  ==============================             \n");

            bool okSaque = pessoa.Conta.Saca(valorSa);

            Console.Clear();

            TelasBoasVindas(pessoa);

            Console.WriteLine("\n\n");

            if (okSaque)
            {
                Console.WriteLine("                   Saque realizado com sucesso !              ");
                Console.WriteLine("                  ===============================             \n");
            }
            else
            {
                Console.WriteLine("                   Saldo em conta insuficiente para essa transação !              ");
                Console.WriteLine("                  ===================================================             \n");
            }
            

            OpcaoVoltarLogado(pessoa);

        }

        private static void TelaSaldo(Pessoa pessoa)
        {
            Console.Clear();

            TelasBoasVindas(pessoa);

            Console.WriteLine($"                Seu saldo é: {pessoa.Conta.ConsultaSaldo()}                          ");
            Console.WriteLine("               ==============================            \n\n ");

            OpcaoVoltarLogado(pessoa);

        }

        private static void TelaExtrato(Pessoa pessoa)
        {
            Console.Clear();

            TelasBoasVindas(pessoa);

            if (pessoa.Conta.Extrato().Any()) // Verifica se há algo no extrato .Any()
            {
                // MOstrar extrato
                double totalEx = pessoa.Conta.Extrato().Sum(x => x.Valor);

                foreach (Extrato extrato in pessoa.Conta.Extrato()) // busca as movimentações atraves da List
                {
                    Console.WriteLine("\n");
                    Console.WriteLine($"                Data de moviemntação: {extrato.Data.ToString("dd/MM/yyyy HH:mm:ss")}     ");
                    Console.WriteLine($"                Tipo de moviemntação: {extrato.Descricao}     ");
                    Console.WriteLine($"                Valor: {extrato.Valor}     ");
                    Console.WriteLine("                ================================================  ");
                }

                Console.WriteLine("\n");
                Console.WriteLine($"                Sub Total: {totalEx}                          ");
                Console.WriteLine("                ================================================ \n\n ");

            }
            else
            {
                // Mostrar uma mensagem que não há extrato
                Console.WriteLine("                Não há extrato a ser exibido:               ");
                Console.WriteLine("               ================================         \n \n");
            }


            OpcaoVoltarLogado(pessoa);

        }
        private static void OpcaoVoltarLogado(Pessoa pessoa)
        {
            Console.WriteLine("                   Entre com uma opção abaixo:               ");
            Console.WriteLine("                  =============================              \n");
            Console.WriteLine("                   1 - Voltar para minha conta               ");
            Console.WriteLine("                  =============================              ");
            Console.WriteLine("                   2 - Sair                                  ");
            Console.WriteLine("                  =============================              ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1) TelaContaLogada(pessoa);
            else TelaPrincipal();
        }
        private static void OpcaoVoltarDeslogado()
        {
            Console.WriteLine("                   Entre com uma opção abaixo:                    ");
            Console.WriteLine("                  ==================================              \n");
            Console.WriteLine("                   1 - Voltar para o menu principal               ");
            Console.WriteLine("                  ==================================              ");
            Console.WriteLine("                   2 - Sair                                       ");
            Console.WriteLine("                  ==================================              ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1) TelaPrincipal();
            else
            {
                Console.WriteLine("                   Opção inválida !                               ");
                Console.WriteLine("                  ==================================              ");
            }
        }
    }
}
