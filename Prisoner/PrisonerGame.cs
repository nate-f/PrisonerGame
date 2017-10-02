using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Prisoner
{
    public class PrisonerGame
    {
        private const int ROUNDS_PER_GENERATION = 100; 
        private const int ELIMS_PER_GENERATION = 10; //should depend on size of pool
        private const int BOTS_PER_AI = 200;


        private const int PTS_PER_COOP = 3;
        private const int PTS_PER_WIN = 5;
        private const int PTS_PER_LOSS = 1;
        


        private List<IPrisoner> players;
        private Random random = new Random();

        private int generations;
        private int numAI;

        public PrisonerGame(List<IPrisoner> bots, int generations)
        {
            players = CreatePlayers(bots).ToList();
            this.generations = generations;
            this.numAI = bots.Count;
        }
        public void PlayGame()
        {
            for (int i = 0; i < generations; i++)
            {
                var results = PlayGeneration(players).ToList();
                var newGeneration = Eliminate(results).ToList();
                this.players = newGeneration.ToList();
            }
        }
        public void PlayRound()
        {
            var results = PlayGeneration(players).ToList();
            var newGeneration = Eliminate(results).ToList();
            this.players = newGeneration.ToList();
        }

        private IEnumerable<IPrisoner> CreatePlayers(IEnumerable<IPrisoner> prisoners)
        {
            foreach (var bot in prisoners)
                for (int i = 0; i < BOTS_PER_AI; i++)
                    yield return (IPrisoner) Activator.CreateInstance(bot.GetType());
        }

        private IEnumerable<PlayerResult> PlayGeneration(IEnumerable<IPrisoner> players)
        {
            var playersToGo = new List<IPrisoner>(players);
            var playerResults = new List<PlayerResult>();
            for(int i = 0; i < (BOTS_PER_AI * numAI) / 2; i++)
            {
                var player0 = playersToGo[random.Next(playersToGo.Count)];
                playersToGo.Remove(player0);
                var player1 = playersToGo[random.Next(playersToGo.Count)];
                playersToGo.Remove(player1);
                var p0Score = 0;
                var p1Score = 0;

                var move0 = player0.GetFirstMove();
                var move1 = player1.GetFirstMove();

                for (int j = 0; j < ROUNDS_PER_GENERATION; j++)
                {
                    if(move0 == Move.COOP && move1 == Move.COOP)
                    {
                        p0Score += PTS_PER_COOP;
                        p1Score += PTS_PER_COOP;
                    }
                    else if(move0 == Move.COOP && move1 == Move.DEFECT)
                    {
                        p0Score += PTS_PER_LOSS;
                        p1Score += PTS_PER_WIN;
                    }
                    else if(move0 == Move.DEFECT && move1 == Move.COOP)
                    {
                        p0Score += PTS_PER_WIN;
                        p1Score += PTS_PER_LOSS;
                    }
                    else if(move0 == Move.DEFECT && move1 == Move.DEFECT)
                    {
                        p0Score += PTS_PER_LOSS;
                        p1Score += PTS_PER_LOSS;
                    }
                    var oldMove = move0;
                    move0 = player0.GetMove(move1);
                    move1 = player1.GetMove(move0);
                }
                yield return new PlayerResult(player0, p0Score, p1Score);
                yield return new PlayerResult(player1, p1Score, p0Score);
            }
        }
        private IEnumerable<IPrisoner> Eliminate(IEnumerable<PlayerResult> results) => 
            results.OrderBy(q => q.score)
            .Take((BOTS_PER_AI * numAI) - ELIMS_PER_GENERATION)
            .Select(w => w.player)
            .Concat(results.OrderBy(q => q.score)
            .Take(ELIMS_PER_GENERATION)
            .Select(e => (IPrisoner)Activator.CreateInstance(e.player.GetType())));

        private class PlayerResult
        {
            public IPrisoner player;
            public int score;
            public int oppScore;
            public PlayerResult(IPrisoner player, int score, int opp)
            {
                this.player = player;
                this.score = score;
                this.oppScore = opp;
            }
        }
        public string ToString() => players.GroupBy(e => e.GetName()).OrderByDescending(e => e.Count()).Aggregate("", (q, w) => q + w.First().GetName() + "\t" + w.Count() + "\r\n");
    }
}
