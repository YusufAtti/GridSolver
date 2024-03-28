using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace try4
{
    public class Animasyon
    {

        private Form1 form;
        private Timer timer = new Timer();
        private Queue<Point> path;
        private int currentStep = 0;
        private PictureBox pictureBox;
        private int cellSize;
        private List<Rectangle> engelListesi; // List of obstacles
        private Lokasyon currentLocation; // Lokasyon nesnesini ekleyin

        public Animasyon(Form1 form, List<Rectangle> engelListesi)
        {
            this.form = form;
            this.engelListesi = engelListesi; // Set the obstacles list
            currentLocation = null;
            this.timer.Interval = 40; // Her 20 milisaniyede bir Tick olayı tetiklenecek
            this.timer.Tick += Timer_Tick;
        }

        // Yolu animasyonla takip eden metot
        public void AnimatePath(Queue<Point> path, PictureBox pictureBox, int cellSize, Lokasyon currentLocation)
        {
            this.path = path;
            this.pictureBox = pictureBox;
            this.cellSize = cellSize;
            this.currentLocation = currentLocation; // Lokasyon bilgisini güncelle

            if (path == null || path.Count == 0)
            {
                MessageBox.Show("ULAŞILACAK HEDEF YOK");
                return;
            }

            StartAnimation();
        }

        // Animasyonu başlatan metot
        private void StartAnimation()
        {
            this.currentStep = 0; // Adım sayısını sıfırla
            timer.Start();
        }

        // Timer'ın tick olayını işleyen metot
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (currentStep < path.Count)
            {
                Point p = path.ElementAt(currentStep);
                int x = p.X * cellSize;
                int y = p.Y * cellSize;

                // Karakterin yeni konumunu çiz
                pictureBox.CreateGraphics().FillRectangle(Brushes.Green, x, y, cellSize, cellSize);

                // Sandıkları kontrol et ve logla
                foreach (var chestType in new[] { "Altın", "Gümüş", "Elmas", "Bakır" })
                {
                    var chestList = form.GetChestList(chestType);
                    foreach (var chest in chestList)
                    {
                        if (chest.Contains(new Point(x, y)) && !form.IsChestCollected(chest))
                        {
                            // Sandık toplama işlemini logla
                            form.LogToRichTextBox2($"X: {p.X}, Y: {p.Y} konumunda {chestType} sandık toplandı");

                            // Toplandı olarak işaretle
                            form.MarkChestAsCollected(chest);
                        }
                    }
                }

                // Koordinatları ve adım sayısını güncelle
                currentStep++;
                form.UpdateStepLabel(currentStep);
                form.AddCoordinatesToRichTextBox(new Lokasyon(p.X, p.Y));

                // Özel animasyonu çağır
                AnimateCustomAri();
                AnimateCustomKus();
            }
            else
            {
                timer.Stop();
                MessageBox.Show("ANIMASYON SONLANDI!");
            }
        }



        // Engellerle veya toplanabilir nesnelerle etkileşimleri animasyonla göster
        public void AnimateObstacleInteraction(Rectangle obstacle)
        {
            Graphics g = pictureBox.CreateGraphics();
            Brush interactionBrush = new SolidBrush(Color.Magenta); // Etkileşim rengi
            g.FillRectangle(interactionBrush, obstacle);
        }

        public void AnimateCollection(Point location)
        {
            int x = location.X * cellSize;
            int y = location.Y * cellSize;
            Graphics g = pictureBox.CreateGraphics();
            g.FillEllipse(Brushes.Gold, x, y, cellSize, cellSize); // Toplama animasyonu için altın rengi
        }

        private void AnimateCustomAri()
        {
            //  MessageBox.Show("23");
            Color startColor = Color.Yellow;
            Color endColor = Color.Red;
            int animationDuration = 20; // Animasyon süresi (ms)
            int steps = 3; // Animasyon adımları sayısı
            Engeller engeller = new Engeller();
            for (int i = 0; i < steps; i++)
            {
                float ratio = (float)i / steps;
                int r = (int)(startColor.R + (endColor.R - startColor.R) * ratio);
                int g = (int)(startColor.G + (endColor.G - startColor.G) * ratio);
                int b = (int)(startColor.B + (endColor.B - startColor.B) * ratio);
                Color currentColor = Color.FromArgb(r, g, b);

                for (int index = 0; index < engelListesi.Count; index++)
                {
                    Rectangle obstacleRect = engelListesi[index];
                    if (engeller.IsCustomAri(obstacleRect, cellSize))
                    {
                        int obstacleX = obstacleRect.X;
                        int obstacleY = obstacleRect.Y;

                        for (int j = 0; j < 2; j++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                int currentX = obstacleX + k * cellSize;
                                int currentY = obstacleY + j * cellSize;

                                pictureBox.CreateGraphics().FillRectangle(new SolidBrush(currentColor), currentX, currentY, cellSize, cellSize);
                            }
                        }
                    }
                }

                System.Threading.Thread.Sleep(animationDuration / steps);
            }
        }

        private void AnimateCustomKus()
        {
            //  MessageBox.Show("23");
            Color startColor = Color.Yellow;
            Color endColor = Color.Red;
            int animationDuration = 20; // Animasyon süresi (ms)
            int steps = 3; // Animasyon adımları sayısı
            Engeller engeller = new Engeller();
            for (int i = 0; i < steps; i++)
            {
                float ratio = (float)i / steps;
                int r = (int)(startColor.R + (endColor.R - startColor.R) * ratio);
                int g = (int)(startColor.G + (endColor.G - startColor.G) * ratio);
                int b = (int)(startColor.B + (endColor.B - startColor.B) * ratio);
                Color currentColor = Color.FromArgb(r, g, b);

                for (int index = 0; index < engelListesi.Count; index++)
                {
                    Rectangle obstacleRect = engelListesi[index];
                    if (engeller.IsCustomKus(obstacleRect, cellSize)) // Buradaki koşulu düzelttim
                    {
                        int obstacleX = obstacleRect.X;
                        int obstacleY = obstacleRect.Y;

                        for (int j = 0; j < 12; j++)
                        {
                            for (int k = 0; k < 2; k++)
                            {
                                int currentX = obstacleX + k * cellSize;
                                int currentY = obstacleY + j * cellSize;

                                pictureBox.CreateGraphics().FillRectangle(new SolidBrush(currentColor), currentX, currentY, cellSize, cellSize);
                            }
                        }
                    }
                }

                System.Threading.Thread.Sleep(animationDuration / steps);
            }
        }




        public void StopAnimation()
        {
            timer.Stop();
        }

    }
}