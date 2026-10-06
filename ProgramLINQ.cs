using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

namespace LINQ
{
    internal class Program
    {
        class Produto
        {
                public int Id { get; set; }
                public string Nome { get; set; }
                public string Categoria { get; set; }
                public decimal Preco { get; set; }
                public int Stock { get; set; }
        }

        class Aluno
        {
                public string Nome { get; set; }
                public int Idade { get; set; }
                public string Turma { get; set; }
                public List<int> Notas { get; set; }

        }

        static void Main(string[] args)
        {

            List<Produto> produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Teclado", Categoria = "Periféricos",
                Preco = 29.90m, Stock = 12 },

                new Produto { Id = 2, Nome = "Rato", Categoria = "Periféricos",
                Preco = 15.50m, Stock = 0 },

                new Produto { Id = 3, Nome = "Monitor", Categoria = "Ecrãs",
                Preco = 189.99m, Stock = 4 },

                new Produto { Id = 4, Nome = "Portatil", Categoria =
                "Computadores",Preco = 899.00m, Stock = 2 },

                new Produto { Id = 5, Nome = "Webcam", Categoria = "Periféricos",
                Preco = 45.00m, Stock = 0 },

                new Produto { Id = 6, Nome = "Monitor 4K",Categoria = "Ecrãs",
                Preco = 349.50m, Stock = 7 },

                new Produto { Id = 7, Nome = "Desktop", Categoria =
                "Computadores",Preco = 649.00m, Stock = 5 }
            };

            List<Aluno> alunos = new List<Aluno>
            {
                new Aluno { Nome = "Ana", Idade = 19, Turma = "A", Notas = new
                List<int> { 14, 16, 11 } },
                new Aluno { Nome = "Bruno", Idade = 22, Turma = "B", Notas = new
                List<int> { 8, 12, 10 } },
                new Aluno { Nome = "Carlos", Idade = 19, Turma = "A", Notas = new
                List<int> { 18, 17, 19 } },
                new Aluno { Nome = "Diana", Idade = 25, Turma = "B", Notas = new
                List<int> { 9, 7, 13 } },
                new Aluno { Nome = "Eduardo",Idade = 21, Turma = "A", Notas = new
                List<int> { 15, 15, 14 } }


            };

            //1.Liste os nomes de todos os produtos com preço inferior a 100 €.
            var produtosAbaixo100 = produtos.Where(p => p.Preco < 100).Select(p => p.Nome);
            Console.WriteLine("Produtos com preço inferior a 100 :");
            foreach (var produto in produtosAbaixo100)
            {
                Console.WriteLine(produto);
            }

            //2.Liste os produtos sem stock(Stock == 0), mostrando apenas o nome.
            var produtosSemStock = produtos.Where(p => p.Stock == 0).Select(p => p.Nome);
            Console.WriteLine("Produtos sem stock:");
            foreach (var produto in produtosSemStock)
            {
                Console.WriteLine(produto);
            }

            //3.Liste todos os produtos ordenados por preço, do mais caro para o mais barato.
            var produtosOrdenadosPorPreco = produtos.OrderByDescending(p => p.Preco);
            Console.WriteLine("Produtos ordenados por preço (do mais caro para o mais barato): ");
            foreach (var produto in produtosOrdenadosPorPreco)
            {
                Console.WriteLine($"{produto.Nome} — {produto.Preco:F2} ");
            }

            //4.Crie uma lista de strings no formato"Teclado — 29,90 €"
            //para todos os produtos dacategoria "Periféricos"
            var produtosPerifericos = produtos.Where(p => p.Categoria == "Periféricos").Select(p => $"{p.Nome} — {p.Preco:F2} ");
            Console.WriteLine("Produtos da categoria 'Periféricos': ");
            foreach (var produto in produtosPerifericos)
            {
                Console.WriteLine(produto);
            }

            //5.Calcule o valor total do stock em armazém(preço × stock, somado).
            var valorTotalStock = produtos.Sum(p => p.Preco * p.Stock);
            Console.WriteLine($"Valor total do stock em armazém: {valorTotalStock:F2} ");

            //6.Quantos produtos custam mais de 200 €?
            var produtosMaisDe200 = produtos.Count(p => p.Preco > 200);
            Console.WriteLine($"Número de produtos que custam mais de 200 : {produtosMaisDe200}");

            //7.Qual é o preço médio dos produtos da categoria "Ecrãs" ?
            var precoMedioEcrans = produtos.Where(p => p.Categoria == "Ecrãs").Average(p => p.Preco);
            Console.WriteLine($"Preço médio dos produtos da categoria 'Ecrãs': {precoMedioEcrans:F2} ");

            //8.Verifique se existe algum produto com stock superior a 10, e se todos os produtos têm
            //preço acima de 10 €.
            var existeProdutoComStockSuperiorA10 = produtos.Any(p => p.Stock > 10);
            var todosProdutosTêmPrecoAcimaDe10 = produtos.All(p => p.Preco > 10);
            Console.WriteLine($"Existe algum produto com stock superior a 10: {existeProdutoComStockSuperiorA10}");
            Console.WriteLine($"Todos os produtos têm preço acima de 10  {todosProdutosTêmPrecoAcimaDe10}");

            //9.Obtenha o produto mais caro(o objeto, não o preço). Resolva de duas formas:
            //com OrderByDescending e com Max + Nível 3 — agrupar e achatarFirst.
            var produtoMaisCaroOrderBy = produtos.OrderByDescending(p => p.Preco).FirstOrDefault();
            var produtoMaisCaroMax = produtos.FirstOrDefault(p => p.Preco == produtos.Max(p2 => p2.Preco));

            //10.Para cada categoria, mostre o nome da categoria, quantos produtos tem e o preço médio.
            var categorias = produtos.GroupBy(p => p.Categoria);
            foreach (var categoria in categorias)
            {
                var quantidade = categoria.Count();
                var precoMedio = categoria.Average(p => p.Preco);
                Console.WriteLine($"Categoria: {categoria.Key}, Quantidade: {quantidade}, Preço Médio: {precoMedio:F2} ");
            }

            //11.Liste os alunos da turma A ordenados pela média das notas, em ordem decrescente, no formato"Carlos: 18,0"
            var alunosTurmaAOrdenadosPorMedia = alunos.Where(a => a.Turma == "A").OrderByDescending(a => a.Notas.Average());
            Console.WriteLine("Alunos da turma ordenados pela média decrescente:");
            foreach (var aluno in alunosTurmaAOrdenadosPorMedia)
            {
                var mediaNotas = aluno.Notas.Average();
                Console.WriteLine($"{aluno.Nome}: {mediaNotas:F1}");


            }

            //12.Obtenha todas as notas de todos os alunos numa única lista, sem repetidos e ordenadas.
            var todasNotas = alunos.SelectMany(a => a.Notas)
                .Distinct()
                .OrderBy(n => n);
            Console.WriteLine("Todas as notas de todos os alunos, sem repetidos e ordenadas:");

            foreach (var nota in todasNotas)
            {
                Console.WriteLine(nota);

            }

            //13.Para cada turma, indique quantos alunos tiveram média igual ou superior a 10.
            var alunosPorTurma = alunos.GroupBy(a => a.Turma);
            foreach (var turma in alunosPorTurma)
            {
                var quantidadeAlunos = turma.Count(a => a.Notas.Average() >= 10);
                Console.WriteLine($"Turma {turma.Key}: {quantidadeAlunos} alunos com média igual ou superior a 10");
            }

            //14.Sem executar, diga o que este código imprime e porquê

            //Resposta: Imprimi 4 pois estou add produto na lista, entao o count ja conta com o que eu ja adicionei.

            //15.O código seguinte lança uma exceção.Identifique qual, explique porquê e corrija-o de duas maneiras diferentes:
            //Aparece este erro InvalidOperationException, pois o c# bloqueia a execucao para evitar erro no codigo,uma maneira
            //de resolver é usando o Remove. 

            //Primeira opção:
            //foreach (var p in produtos.Where(p => p.Stock == 0).ToList())
            //produtos.Remove(p);

            //Segunda opção: produtos.RemoveAll(p => p.Stock == 0);

            //16.Reescreva a consulta abaixo na sintaxe de query e explique por que é que a ordem Where → Select é
            //preferível à ordem inversa.

            //Resposta: A ordem Where → Select é preferível à ordem inversa, pois o Where filtra os elementos que
            //nao sao necessario e retira. Com o Select, resultando em melhor ,mais rapido e otimizar consumo de memória.













        }


    }
}
