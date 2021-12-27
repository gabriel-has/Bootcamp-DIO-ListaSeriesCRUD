using System;

namespace DIO.Series
{

    class Program
    {

        static SerieRepositorio repositorio = new SerieRepositorio();

        static void Main (string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    
                    case "2":
                        InserirSerie();
                        break;
                    
                    case "3":
                        AtualizarSerie();
                        break;
                    
                    case "4":
                        ExcluirSerie();
                        break;

                    case "5":
                        VisualizarSerie();
                        break;

                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();

                }

                opcaoUsuario = ObterOpcaoUsuario();

            }

            Console.WriteLine("Obrigado por utilizar a aplicação!\n");

        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar Series");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada na plataforma");
                return;
            }

            foreach(var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                Console.WriteLine("#ID {0}: {1}{2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? " - *Excluido*" : ""));
            }

        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir Série:");
            //  https://docs.microsoft.com.br/pt-br/dotnet/api/sistem.enum.getvalues?view=netcore-3.1
            //  https://docs.microsoft.com.br/pt-br/dotnet/api/sistem.enum.getname?view=netcore-3.1

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.Write("Informe o gênero dentre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Informe o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Informe o ano de lançamento: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite uma descrição para a série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                         genero: (Genero)entradaGenero,
                                         titulo: entradaTitulo,
                                         ano: entradaAno,
                                         descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
        }


        private static void ExcluirSerie()
        {
            Console.Write("Informe o Id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(indiceSerie);

        }

        private static void VisualizarSerie()
        {
            Console.Write("Informe o Id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.WriteLine(serie);
        }

        private static void AtualizarSerie()
        {
            Console.Write("Informe o Id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.Write("Informe o gênero dentre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Informe o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Informe o ano de lançamento: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite uma descrição para a série: ");
            string entradaDescricao = Console.ReadLine();

            Serie atualizarSerie = new Serie(id: repositorio.ProximoId(),
                                         genero: (Genero)entradaGenero,
                                         titulo: entradaTitulo,
                                         ano: entradaAno,
                                         descricao: entradaDescricao);

            repositorio.Atualiza(indiceSerie, atualizarSerie);
        }

        private static string ObterOpcaoUsuario ()
        {
            Console.WriteLine();
            Console.WriteLine("Cadastro de séries - DigitalInnovationOne");
            Console.WriteLine("Informe a opção desejada: ");

            Console.WriteLine("1 - Listar séries;");
            Console.WriteLine("2 - Inserir nova série; ");
            Console.WriteLine("3 - Atualizar série; ");
            Console.WriteLine("4 - Excluir série; ");
            Console.WriteLine("5 - Visualizar série; ");
            Console.WriteLine("C - Limpar tela; ");
            Console.WriteLine("X - Sair da aplicação; ");
            Console.Write("Sua opção: ");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;

        }

    }

}