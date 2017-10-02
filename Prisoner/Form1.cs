using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prisoner
{
    public partial class Form1 : Form
    {
        private PrisonerGame game;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var players = new List<IPrisoner>() { new CooperateBot(), new DefectBot(), new ForgivingBot(), new GreedyBot(), new TitForTatBot() };
            game = new PrisonerGame(players, 1000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var uiThread = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                game.PlayGame();
                return game.ToString();
            }).ContinueWith((q) => textBox1.Text = q.Result, uiThread);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            game.PlayRound();
            textBox1.Text = game.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var players = new List<IPrisoner>() { new CooperateBot(), new DefectBot(), new ForgivingBot(), new GreedyBot(), new TitForTatBot() };
            game = new PrisonerGame(players, 1000);
            textBox1.Text = game.ToString();
        }
    }
}
