using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

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
            ResetGame();
            textBox1.Text = game.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var uiThread = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                game.PlayGame();
                return game;
            }).ContinueWith((q) =>
            {
                textBox1.Text = q.Result.ToString();
                chart1.Series.Clear();
                chart1.Series.Add(q.Result.ToSeries());
            }, uiThread);
        }
        private void button4_Click(object sender, EventArgs e)
        {
                var uiThread = TaskScheduler.FromCurrentSynchronizationContext();
                Task.Run(() =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        Task.Run(() =>
                        {
                            game.PlayRound();
                            Task.Delay(100).Wait();
                        }).ContinueWith((q) =>
                        {
                            try
                            {
                                chart1.Series.Clear();
                                chart1.Series.Add(game.ToSeries());
                                chart1.Update();
                            }
                            catch (Exception) { }
                        }, uiThread).Wait();
                    }
                }).ContinueWith((q) => textBox1.Text = game.ToString(), uiThread);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            game.PlayRound();
            textBox1.Text = game.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetGame();
            textBox1.Text = game.ToString();
        }
        private void ResetGame() => game = new PrisonerGame(new List<IPrisoner>(typeof(IPrisoner).Assembly.GetExportedTypes()
                .Where(q => (q.GetInterfaces().Any(r => r == typeof(IPrisoner))))
                .Select(r => (IPrisoner)Activator.CreateInstance(r))), 100);

    }
}
