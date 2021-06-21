using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static LivroRepositorio repositorioLivro = new LivroRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				try
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
						case "6":
							ListarLivro();
							break;
						case "7":
							InserirLivro();
							break;
						case "8":
							AtualizarLivro();
							break;
						case "9":
							ExcluirLivro();
							break;
						case "10":
							VisualizarLivro();
							break;
						case "C":
							Console.Clear();
							break;

						default:
							throw new ArgumentOutOfRangeException();
					}
				}
                catch(ArgumentOutOfRangeException)
                {
					Console.WriteLine("Selecione uma das opções mencionadas" + Environment.NewLine);
                }
				

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

        private static void ExcluirSerie()
		{
			try
			{
				Console.Write("Digite o id da série: ");
				int indiceSerie = int.Parse(Console.ReadLine());

				repositorio.Exclui(indiceSerie);
			}
            catch (ArgumentOutOfRangeException)
            {
				Console.WriteLine("Nenhuma série para ser excluída" + Environment.NewLine);
            }
		}

		private static void ExcluirLivro()
		{
			try
			{
				Console.Write("Digite o id do livro: ");
				int indiceLivro = int.Parse(Console.ReadLine());

				repositorioLivro.Exclui(indiceLivro);
			}
			catch (ArgumentOutOfRangeException)
			{
				Console.WriteLine("Nenhum livro para ser excluído" + Environment.NewLine);
			}
		}

		private static void VisualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

		private static void VisualizarLivro()
		{
			Console.Write("Digite o id do livro: ");
			int indiceLivro = int.Parse(Console.ReadLine());

			var livro = repositorioLivro.RetornaPorId(indiceLivro);

			Console.WriteLine(livro);
		}

		private static void AtualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());
						
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}

		private static void AtualizarLivro()
		{
			Console.Write("Digite o id do livro: ");
			int indiceLivro = int.Parse(Console.ReadLine());

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título do Livro: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Publicação do Livro: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite o/a Autor(a) do Livro: ");
			string entradaAutor = Console.ReadLine();

			Livro atualizaLivro = new Livro(id: indiceLivro,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										autor: entradaAutor);

			repositorioLivro.Atualiza(indiceLivro, atualizaLivro);
		}

		private static void ListarSeries()
		{
			Console.WriteLine("Listar séries");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(),
					serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

		private static void ListarLivro()
		{
			Console.WriteLine("Listar Livros");

			var lista = repositorioLivro.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhum livro cadastrado.");
				return;
			}

			foreach (var livro in lista)
			{
				var excluido = livro.retornaExcluido();

				Console.WriteLine("#ID {0}: - {1} {2}", livro.retornaId(),
					livro.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

		private static void InserirSerie()
		{
			Console.WriteLine("Inserir nova série");
						
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}

			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Serie novaSerie = new Serie(id: repositorio.ProximoId(),
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Insere(novaSerie);

			Console.Write("Série Adicionada com sucesso!" + Environment.NewLine);
		}

		private static void InserirLivro()
		{
			Console.WriteLine("Inserir novo livro");

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}

			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título do Livro: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Publicação do Livro: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite o/a Autor(a) do Livro: ");
			string entradaAutor = Console.ReadLine();

			Livro novoLivro = new Livro(id: repositorioLivro.ProximoId(),
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										autor: entradaAutor);

			repositorioLivro.Insere(novoLivro);

			Console.Write("Livro Adicionado com sucesso!" + Environment.NewLine);
		}

		private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Entretenimento a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("6- Listar livros");
			Console.WriteLine("7- Inserir novo livro");
			Console.WriteLine("8- Atualizar livro");
			Console.WriteLine("9- Excluir livro");
			Console.WriteLine("10- Visualizar livro");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
