using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GiocoCoppie
{
    public partial class Form1 : Form
    {
        List<string> images;
        Random random;
        string path = "";
        int firstClick = 0;
        int errors = 0;
        int points = 0;
        int totalPairs = 8;
        int maxError = 3;

        public Form1()
        {
            InitializeComponent();
            random = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        private void label_MouseEnter(object sender, EventArgs e)
        {
            Label box = (Label)sender;
            box.BackColor = Color.BurlyWood;
            box.Cursor = Cursors.Hand;
        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            Label box = (Label)sender;
            box.BackColor = Color.FromArgb(255, 232, 232);
            box.Cursor = Cursors.Default;
        }

        private void label_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Visible = false;

            Panel p=label.Parent as Panel;

            PictureBox box = GetPictureBox(p);

            string img = box.ImageLocation;

            if (firstClick == 0)
            {
                path = img;
                firstClick++;
            }
            else
            {
                if (path!=img)
                {
                    timerShow.Start();
                    errors++;
                    points = 0;

                    if (errors == maxError)
                    {
                        MessageBox.Show("Game Over!!", "FAIL");

                        ClearGame();

                        StartGame();
                    }
                }
                else
                {
                    points++;
                }

                firstClick = 0;
            }

            if (points == totalPairs)
            {
                MessageBox.Show("You Win!!", "SUCCESS");

                ClearGame();

                StartGame();
            }
            
        }

        private void timerStart_Tick(object sender, EventArgs e)
        {
            HideCards();

            timerStart.Stop();
        }

        private void timerShow_Tick(object sender, EventArgs e)
        {
            HideCards();
            timerShow.Stop();
        }

        public void HideCards()
        {
            foreach (Panel panel in tableLayoutCoppie.Controls)
            {
                foreach (Control c in panel.Controls)
                {
                    Label label = c as Label;

                    if (label != null)
                    {
                        label.Visible = true;
                    }
                }
            }
        }

        public PictureBox GetPictureBox(Panel p)
        {
            return p.Controls.OfType<PictureBox>().FirstOrDefault();
        }    

        public void StartGame()
        {
            var basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            images = new List<string>()
            {
                 Path.Combine(basePath, "images","ananas.png"),
                 Path.Combine(basePath, "images","arancio.png"),
                 Path.Combine(basePath, "images","banana.png"),
                 Path.Combine(basePath, "images","ciliegia.png"),
                 Path.Combine(basePath, "images","dino.png"),
                 Path.Combine(basePath, "images","fragola.png"),
                 Path.Combine(basePath, "images", "giraffa.png"),
                 Path.Combine(basePath, "images","mela.png"),

                 Path.Combine(basePath, "images","ananas.png"),
                 Path.Combine(basePath, "images","arancio.png"),
                 Path.Combine(basePath, "images","banana.png"),
                 Path.Combine(basePath, "images","ciliegia.png"),
                 Path.Combine(basePath, "images","dino.png"),
                 Path.Combine(basePath, "images","fragola.png"),
                 Path.Combine(basePath, "images", "giraffa.png"),
                 Path.Combine(basePath, "images","mela.png")
            };

            foreach (Panel panel in tableLayoutCoppie.Controls)
            {
                Label label = new Label();
                PictureBox box = new PictureBox();

                box.Dock = DockStyle.Fill;

                int indice = random.Next(images.Count);
                string img = images.ElementAt(indice);

                box.ImageLocation = img;
                box.SizeMode = PictureBoxSizeMode.StretchImage;

                images.RemoveAt(indice);

                label.Dock = DockStyle.Fill;
                label.Visible = false;
                label.BackColor = Color.FromArgb(255, 232, 232);

                label.MouseEnter += new EventHandler(label_MouseEnter);
                label.MouseLeave += new EventHandler(label_MouseLeave);
                label.Click += new EventHandler(label_Click);

                panel.Controls.Add(label);
                panel.Controls.Add(box);

            }

            timerStart.Start();
        }

        public void ClearGame()
        {
            foreach (Control control in tableLayoutCoppie.Controls)
            {
                if (control is Panel panel)
                {
                    panel.Controls.Clear();
                }
            }

            errors = 0;
            points = 0;
        }


    }
}
