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
        private const int ROUNDS_PER_GENERATION = 10;
        private const int PTS_PER_COOP = 3;
        private const int PTS_PER_WIN = 5;
        private const int PTS_PER_LOSS = 1;


        private List<IPrisoner> players;
        private RandomNumberGenerator random = new RNGCryptoServiceProvider();
        public PrisonerGame(List<IPrisoner> bots, int generations)
        {
            players = new List<IPrisoner>();
            foreach(var bot in bots)
            {
                for(int i = 0; i < 10; i++)
                {
                    var botType = bot.GetType();
                    var newBot = (IPrisoner) Activator.CreateInstance(botType);
                    players.Add(newBot);
                }
            }
            //need code to add default bots here
        }
        public void PlayGame()
        {
            
        }

        private List<PlayerResult> PlayGeneration()
        {
            var playersToGo = new List<IPrisoner>(players);
            var playerResults = new List<PlayerResult>();
            while (playersToGo.Count != 0)
            {
                var player0 = playersToGo[RandomInt(playersToGo.Count)];
                playersToGo.Remove(player0);
                var player1 = playersToGo[RandomInt(playersToGo.Count)];
                var p0Score = 0;
                var p1Score = 0;

                var move0 = player0.GetFirstMove();
                var move1 = player1.GetFirstMove();

                for (int i = 0; i < ROUNDS_PER_GENERATION; i++)
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
                playerResults.Add(new PlayerResult(player0, p0Score, p1Score));
                playerResults.Add(new PlayerResult(player1, p1Score, p0Score));
            }
            return playerResults;
        }
        public void ScoreGeneration()
        {

        }
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
        public string ToString()
        {
            throw new NotImplementedException();
        }
        private int RandomInt(int limit)
        {
            var bytes = new byte[4];
            random.GetBytes(bytes);
            int number = BitConverter.ToInt32(bytes, 0);
            //make this correct later
            return number;
        }
    }
}
