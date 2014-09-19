namespace UnitTestProject1
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;
    using OldUglyTrivia;
    using UglyTrivia;
    using Game = UglyTrivia.Game;


    [TestClass]
    public class GameRunnerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var oldRunner = new GameRunner<OldUglyTrivia.Game>();
            var runner = new GameRunner<UglyTrivia.Game>();
            var multiRunner = new MultiGame(oldRunner, runner);

            var joueurs = new[] {"Chet", "Pat", "Sue"};

            for (int i = 0; i < 1000; i++)
            {
                var result = multiRunner.Run(i, s => { }, joueurs);

                Check.That(result[0]).ContainsExactly(result[1]);
            }
        }
    }

    public class MultiGame
    {
        private readonly IEnumerable<IGameRunner> _runners;

        public MultiGame(params IGameRunner[] runners)
        {
            _runners = runners;
        }

        public List<List<string>> Run(int randInitializer, Action<string> writeLine, params string[] joueurs)
        {
            var outputs = new List<List<string>>();
            foreach (var runner in _runners)
            {
                var lines = new List<string>();
                runner.Run(randInitializer, lines.Add, joueurs);
                outputs.Add(lines);
            }

            return outputs;
        }
    }

    public interface IGameRunner
    {
        void Run(int randInitializer, Action<string> writeLine, params string[] joueurs);
    }

    public class GameRunner<T> : IGameRunner where T : IGame
    {
        public void Run(int randInitializer, Action<string> writeLine, params string[] joueurs)
        {
            var aGame = (T)Activator.CreateInstance(typeof(T), new[] { writeLine });

            foreach (var joueur in joueurs)
            {
                aGame.AddPlayer(joueur);
            }

            var rand = new Random(randInitializer);

            bool notAWinner;
            do
            {

                aGame.roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    notAWinner = aGame.wrongAnswer();
                }
                else
                {
                    notAWinner = aGame.wasCorrectlyAnswered();
                }



            } while (notAWinner);

        }
    }
}
