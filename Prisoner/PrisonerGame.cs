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
            var playersToGo = new List<IPrisoner>(players);
            var donePlayers = new List<IPrisoner>();
            while(playersToGo.Count != 0)
            {
                var player0 = playersToGo[RandomInt(playersToGo.Count)];
                playersToGo.Remove(player0);
                var player1 = playersToGo[RandomInt(playersToGo.Count)];
                var score = new Tuple<int, int>(0, 0);

                var move0 = player0.GetFirstMove();
                var move1 = player1.GetFirstMove();


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
