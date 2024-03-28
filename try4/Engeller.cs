using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace try4
{
    public class Engeller
    {

        public List<Rectangle> engelListesi;
        public List<Rectangle> altinEngelListesi;
        public List<Rectangle> gumusEngelListesi;
        public List<Rectangle> diaEngelListesi;
        public List<Rectangle> bakırEngelListesi;// Yeni liste
        private Random random = new Random();

        public Engeller()
        {
            engelListesi = new List<Rectangle>();
            altinEngelListesi = new List<Rectangle>(); // Yeni liste oluşturuldu
            gumusEngelListesi = new List<Rectangle>(); // Yeni liste oluşturuldu
            diaEngelListesi = new List<Rectangle>(); // Yeni liste oluşturuldu
            bakırEngelListesi = new List<Rectangle>(); // Yeni liste oluşturuldu
        }



        //   -----------------------------SANDIK BÖLÜMÜ -------------------------------------
        //   ----------------------------------------------------------------------------------------

        public void AltınSandık(Graphics g, int gridSize, int cellSize)
        {
            // 5 adet 1x1 boyutunda altın renginde engel oluştur
            for (int i = 0; i < 5; i++)
            {
                int obstacleX, obstacleY;
                bool isOverlap = false;

                do
                {
                    isOverlap = false;

                    // Sandıkların sınır hücrelerinden en az 1 hücre içeride oluşturulmasını sağla
                    obstacleX = random.Next(2, gridSize - 2) * cellSize; // Kenarlardan en az 1 hücre içeride
                    obstacleY = random.Next(2, gridSize - 2) * cellSize; // Kenarlardan en az 1 hücre içeride

                    // Yeni engelin koordinatları
                    Rectangle newObstacleRect = new Rectangle(obstacleX, obstacleY, cellSize, cellSize);

                    // Tüm mevcut engellerle çakışıp çakışmadığını kontrol et
                    foreach (Rectangle obstacleRect in engelListesi)
                    {
                        if (newObstacleRect.IntersectsWith(obstacleRect))
                        {
                            isOverlap = true;
                            break;
                        }
                    }
                } while (isOverlap);

                // Engeli ekle
                // Altın engel oluşturulduğunda yeni listeye ekle
                altinEngelListesi.Add(new Rectangle(obstacleX, obstacleY, cellSize, cellSize));


                // Engeli çiz
                DrawImageInCell(g, obstacleX, obstacleY, cellSize, "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\hazineler\\gold.jpg");
            }
        }



        public void GumusSandık(Graphics g, int gridSize, int cellSize)
        {
            // 5 adet 1x1 boyutunda altın renginde engel oluştur
            for (int i = 0; i < 5; i++)
            {
                int obstacleX, obstacleY;
                bool isOverlap = false;

                do
                {
                    isOverlap = false;

                    obstacleX = random.Next(1, gridSize - 1) * cellSize;
                    obstacleY = random.Next(1, gridSize - 1) * cellSize;

                    // Yeni engelin koordinatları
                    Rectangle newObstacleRect = new Rectangle(obstacleX, obstacleY, cellSize, cellSize);

                    // Tüm mevcut engellerle çakışıp çakışmadığını kontrol et
                    foreach (Rectangle obstacleRect in engelListesi)
                    {
                        if (newObstacleRect.IntersectsWith(obstacleRect))
                        {
                            isOverlap = true;
                            break;
                        }
                    }
                } while (isOverlap);

                // Engeli ekle
                gumusEngelListesi.Add(new Rectangle(obstacleX, obstacleY, cellSize, cellSize));
                // Engeli çiz
                DrawImageInCell(g, obstacleX, obstacleY, cellSize, "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\hazineler\\silver.jpg");
            }
        }

        public void DiaSandık(Graphics g, int gridSize, int cellSize)
        {
            // 5 adet 1x1 boyutunda altın renginde engel oluştur
            for (int i = 0; i < 5; i++)
            {
                int obstacleX, obstacleY;
                bool isOverlap = false;

                do
                {
                    isOverlap = false;

                    obstacleX = random.Next(1, gridSize - 1) * cellSize;
                    obstacleY = random.Next(1, gridSize - 1) * cellSize;

                    // Yeni engelin koordinatları
                    Rectangle newObstacleRect = new Rectangle(obstacleX, obstacleY, cellSize, cellSize);

                    // Tüm mevcut engellerle çakışıp çakışmadığını kontrol et
                    foreach (Rectangle obstacleRect in engelListesi)
                    {
                        if (newObstacleRect.IntersectsWith(obstacleRect))
                        {
                            isOverlap = true;
                            break;
                        }
                    }
                } while (isOverlap);

                // Engeli ekle
                diaEngelListesi.Add(new Rectangle(obstacleX, obstacleY, cellSize, cellSize));
                // Engeli çiz
                DrawImageInCell(g, obstacleX, obstacleY, cellSize, "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\hazineler\\dia.jpg");
            }
        }

        public void BakirSandık(Graphics g, int gridSize, int cellSize)
        {
            // 5 adet 1x1 boyutunda altın renginde engel oluştur
            for (int i = 0; i < 5; i++)
            {
                int obstacleX, obstacleY;
                bool isOverlap = false;

                do
                {
                    isOverlap = false;

                    obstacleX = random.Next(1, gridSize - 1) * cellSize;
                    obstacleY = random.Next(1, gridSize - 1) * cellSize;

                    // Yeni engelin koordinatları
                    Rectangle newObstacleRect = new Rectangle(obstacleX, obstacleY, cellSize, cellSize);

                    // Tüm mevcut engellerle çakışıp çakışmadığını kontrol et
                    foreach (Rectangle obstacleRect in engelListesi)
                    {
                        if (newObstacleRect.IntersectsWith(obstacleRect))
                        {
                            isOverlap = true;
                            break;
                        }
                    }
                } while (isOverlap);

                // Engeli ekle
                bakırEngelListesi.Add(new Rectangle(obstacleX, obstacleY, cellSize, cellSize));
                // Engeli çiz
                DrawImageInCell(g, obstacleX, obstacleY, cellSize, "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\hazineler\\bakir.jpg");
            }
        }



        //   -----------------------------HAREKETSİZ ENGELLER  -------------------------------------
        //   ------------------------------------------------------------------------------------------------



        public void CreateTas(Graphics g, int gridSize, int cellSize)
        {
            // 4 adet 2x2 lik resimli engel
            for (int i = 0; i < 4; i++)
            {
                int obstacleX, obstacleY;
                bool isOverlap = false;

                do
                {
                    isOverlap = false;

                    obstacleX = random.Next(1, gridSize - 1) * cellSize;
                    obstacleY = random.Next(1, gridSize - 1) * cellSize;

                    // Yeni engelin koordinatları
                    Rectangle newObstacleRect = new Rectangle(obstacleX, obstacleY, 2 * cellSize, 2 * cellSize);

                    // Tüm mevcut engellerle çakışıp çakışmadığını kontrol et
                    foreach (Rectangle obstacleRect in engelListesi)
                    {
                        if (newObstacleRect.IntersectsWith(obstacleRect))
                        {
                            isOverlap = true;
                            break;
                        }
                    }
                } while (isOverlap);

                // Engeli ekle
                engelListesi.Add(new Rectangle(obstacleX, obstacleY, 2 * cellSize, 2 * cellSize));

                // Engelin sol tarafta olup olmadığını kontrol et ve uygun şekilde çizim yap
                if (obstacleX < gridSize / 2 * cellSize)
                {
                    // Sol taraftaki engeli sarı renkle göster
                    DrawImageInCell(g, obstacleX, obstacleY, 2 * cellSize, "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\9dbb14a92a7611794f3e5a0c80e4bad6.jpg");
                }
                else
                {
                    // Sağ taraftaki engeli resimle göster
                    DrawImageInCell(g, obstacleX, obstacleY, 2 * cellSize, "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\pink-marble.jpg");
                }
            }

            // 2 adet 3x3 lük resimli engel
            for (int i = 0; i < 2; i++)
            {
                int biggerObstacleX, biggerObstacleY;
                bool isBiggerOverlap = false;

                do
                {
                    isBiggerOverlap = false;

                    biggerObstacleX = random.Next(1, gridSize - 2) * cellSize;
                    biggerObstacleY = random.Next(1, gridSize - 2) * cellSize;

                    // Yeni engelin koordinatları
                    Rectangle newBiggerObstacleRect = new Rectangle(biggerObstacleX, biggerObstacleY, 3 * cellSize, 3 * cellSize);

                    // Tüm mevcut engellerle çakışıp çakışmadığını kontrol et
                    foreach (Rectangle obstacleRect in engelListesi)
                    {
                        if (newBiggerObstacleRect.IntersectsWith(obstacleRect))
                        {
                            isBiggerOverlap = true;
                            break;
                        }
                    }
                } while (isBiggerOverlap);

                // Engeli ekle
                engelListesi.Add(new Rectangle(biggerObstacleX, biggerObstacleY, 3 * cellSize, 3 * cellSize));

                // Engelin sol tarafta olup olmadığını kontrol et ve uygun şekilde çizim yap
                if (biggerObstacleX < gridSize / 2 * cellSize)
                {
                    // Sol taraftaki engeli sarı renkle göster
                    DrawImageInCell(g, biggerObstacleX, biggerObstacleY, 3 * cellSize, "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\9dbb14a92a7611794f3e5a0c80e4bad6.jpg");
                }
                else
                {
                    // Sağ taraftaki engeli resimle göster
                    DrawImageInCell(g, biggerObstacleX, biggerObstacleY, 3 * cellSize, "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\pink-marble.jpg");
                }
            }
        }


        public void CreateAgac(Graphics g, int gridSize, int cellSize)
        {
            // 5 adet 2x2 lik resimli engel
            for (int i = 0; i < 5; i++)
            {
                int obstacleX, obstacleY;
                bool isOverlap = false;

                do
                {
                    isOverlap = false;

                    obstacleX = random.Next(1, gridSize - 1) * cellSize;
                    obstacleY = random.Next(1, gridSize - 1) * cellSize;

                    // Yeni engelin koordinatları
                    Rectangle newObstacleRect = new Rectangle(obstacleX, obstacleY, 2 * cellSize, 2 * cellSize);

                    // Tüm mevcut engellerle çakışıp çakışmadığını kontrol et
                    foreach (Rectangle obstacleRect in engelListesi)
                    {
                        if (newObstacleRect.IntersectsWith(obstacleRect))
                        {
                            isOverlap = true;
                            break;
                        }
                    }
                } while (isOverlap);

                // Engeli ekle
                engelListesi.Add(new Rectangle(obstacleX, obstacleY, 2 * cellSize, 2 * cellSize));

                // Engelin sol yarısında mı kontrol et
                if (obstacleX < (gridSize / 2) * cellSize)
                {
                    // Sol taraftaki engeli başka bir resimle göster
                    DrawImageInCell(g, obstacleX, obstacleY, 2 * cellSize, "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\62297076-noel-ağacı-çam-ağacı-yıldız-simgesi.jpg");
                }
                else
                {
                    // Sağ taraftaki engeli aynı resimle göster
                    DrawImageInCell(g, obstacleX, obstacleY, 2 * cellSize, "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\tree-1464728_640.jpg");
                }
            }

            // 1 adet 4x4 lük resimli engel
            int biggerObstacleX, biggerObstacleY;
            bool isBiggerOverlap = false;

            do
            {
                isBiggerOverlap = false;

                biggerObstacleX = random.Next(1, gridSize - 3) * cellSize;
                biggerObstacleY = random.Next(1, gridSize - 3) * cellSize;

                // Yeni engelin koordinatları
                Rectangle newBiggerObstacleRect = new Rectangle(biggerObstacleX, biggerObstacleY, 4 * cellSize, 4 * cellSize);

                // Tüm mevcut engellerle çakışıp çakışmadığını kontrol et
                foreach (Rectangle obstacleRect in engelListesi)
                {
                    if (newBiggerObstacleRect.IntersectsWith(obstacleRect))
                    {
                        isBiggerOverlap = true;
                        break;
                    }
                }
            } while (isBiggerOverlap);

            // Engeli ekle
            engelListesi.Add(new Rectangle(biggerObstacleX, biggerObstacleY, 4 * cellSize, 4 * cellSize));

            // Engelin sol yarısında mı kontrol et
            if (biggerObstacleX < (gridSize / 2) * cellSize)
            {
                // Sol taraftaki engeli başka bir resimle göster
                DrawImageInCell(g, biggerObstacleX, biggerObstacleY, 4 * cellSize, "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\62297076-noel-ağacı-çam-ağacı-yıldız-simgesi.jpg");
            }
            else
            {
                // Sağ taraftaki engeli aynı resimle göster
                DrawImageInCell(g, biggerObstacleX, biggerObstacleY, 4 * cellSize, "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\tree-1464728_640.jpg");
            }
        }


        public void CreateDag(Graphics g, int gridSize, int cellSize)
        {
            int obstacleX, obstacleY;
            bool isOverlap = false;

            // Engelin boyutları
            int obstacleSize = 15 * cellSize; // 15x15 boyutunda bir engel oluşturmak için

            do
            {
                isOverlap = false;

                // Rastgele bir konum belirle
                obstacleX = random.Next(1, gridSize - 15) * cellSize;
                obstacleY = random.Next(1, gridSize - 15) * cellSize;

                // Yeni engelin koordinatları
                Rectangle newObstacleRect = new Rectangle(obstacleX, obstacleY, obstacleSize, obstacleSize);

                // Tüm mevcut engellerle çakışıp çakışmadığını kontrol et
                foreach (Rectangle obstacleRect in engelListesi)
                {
                    if (newObstacleRect.IntersectsWith(obstacleRect))
                    {
                        isOverlap = true;
                        break;
                    }
                }
            } while (isOverlap);

            // Engeli ekle
            engelListesi.Add(new Rectangle(obstacleX, obstacleY, obstacleSize, obstacleSize));

            // Sol veya sağ taraf için resmi belirle ve yükle
            string leftObstacleImage = "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\images2.jpg";
            string rightObstacleImage = "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\p2.jpg";
            Bitmap obstacleImage;
            if (obstacleX < (gridSize / 2) * cellSize)
            {
                // Sol taraftaki engel için resim
                obstacleImage = new Bitmap(leftObstacleImage);
            }
            else
            {
                // Sağ taraftaki engel için resim
                obstacleImage = new Bitmap(rightObstacleImage);
            }

            // Resmi çizin
            g.DrawImage(obstacleImage, obstacleX, obstacleY, obstacleSize, obstacleSize);
        }

        public void CreateDuvar(Graphics g, int gridSize, int cellSize)
        {
            for (int i = 0; i < 7; i++)
            {
                int obstacleX, obstacleY;
                bool isOverlap = false;
                bool isVertical = random.Next(2) == 0; // 0 ise dikey, 1 ise yatay engel oluştur

                do
                {
                    isOverlap = false; // Başlangıçta çakışma olmadığını varsayalım

                    if (isVertical)
                    {
                        // Dikey engel oluştur
                        obstacleX = random.Next(1, gridSize - 1) * cellSize; // Sınırları dikkate alarak rastgele bir konum seçin
                        obstacleY = random.Next(1, gridSize - 10) * cellSize; // Sınırları dikkate alarak rastgele bir konum seçin
                    }
                    else
                    {
                        // Yatay engel oluştur
                        obstacleX = random.Next(1, gridSize - 10) * cellSize; // Sınırları dikkate alarak rastgele bir konum seçin
                        obstacleY = random.Next(1, gridSize - 1) * cellSize; // Sınırları dikkate alarak rastgele bir konum seçin
                    }

                    // Yeni engelin koordinatları
                    Rectangle newObstacleRect = new Rectangle(obstacleX, obstacleY, isVertical ? cellSize : 10 * cellSize, isVertical ? 10 * cellSize : cellSize);

                    // Tüm mevcut engellerle çakışıp çakışmadığını kontrol edin
                    foreach (Rectangle obstacleRect in engelListesi)
                    {
                        if (newObstacleRect.IntersectsWith(obstacleRect))
                        {
                            isOverlap = true; // Çakışma varsa işaretle
                            break;
                        }
                    }
                } while (isOverlap); // Çakışma varsa tekrar yeni koordinatlar üret

                // Engeli ekle
                engelListesi.Add(new Rectangle(obstacleX, obstacleY, isVertical ? cellSize : 10 * cellSize, isVertical ? 10 * cellSize : cellSize));

                // Engeli çiz
                if (obstacleX < (gridSize / 2) * cellSize)
                {
                    Image engelResmi = Image.FromFile("C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\43348066-grey-brick-wall.jpg");
                    int engelWidth = isVertical ? cellSize : 10 * cellSize;
                    int engelHeight = isVertical ? 10 * cellSize : cellSize;
                    g.DrawImage(engelResmi, obstacleX, obstacleY, engelWidth, engelHeight);
                }
                else
                {
                    Image engelResmi = Image.FromFile("C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\images.jpg");
                    int engelWidth = isVertical ? cellSize : 10 * cellSize;
                    int engelHeight = isVertical ? 10 * cellSize : cellSize;
                    g.DrawImage(engelResmi, obstacleX, obstacleY, engelWidth, engelHeight);
                }
            }
        }



        //   ----------------------------- DİNAMİK ENGELLER  -------------------------------------
        //   ---------------------------------------------------------------------------------------------


        public void CreateAri(Graphics g, int gridSize, int cellSize)
        {
            int obstacleX, obstacleY;
            int obstacleWidth = 8 * cellSize;
            int obstacleHeight = 2 * cellSize;
            bool isOverlap = false;

            do
            {
                isOverlap = false;
                obstacleX = random.Next(1, gridSize - 9) * cellSize;
                obstacleY = random.Next(1, gridSize - 2) * cellSize;
                Rectangle newObstacleRect = new Rectangle(obstacleX, obstacleY, obstacleWidth, obstacleHeight);

                // Engellerin birbirleriyle çakışıp çakışmadığını kontrol et
                foreach (Rectangle obstacleRect in engelListesi)
                {
                    if (newObstacleRect.IntersectsWith(obstacleRect))
                    {
                        isOverlap = true;
                        break;
                    }
                }
            } while (isOverlap);

            // Engelin merkezini hesapla ve fotoğrafı yerleştireceğin alanı belirle
            int photoCenterX = obstacleX + (obstacleWidth - 2 * cellSize) / 2;
            int photoCenterY = obstacleY + (obstacleHeight - 2 * cellSize) / 2;

            // Fotoğrafın yolu
            string photoPath = "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\images.png"; // Doğru yolu belirtin

            // Fotoğrafı yükle
            Image photo = Image.FromFile(photoPath);

            // Döngü içinde, engelin her bir hücresini sarı ile doldururken, fotoğrafın kaplayacağı alanı atla
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int currentX = obstacleX + j * cellSize;
                    int currentY = obstacleY + i * cellSize;

                    // Fotoğrafın yerleştirileceği alanı kontrol et
                    if (!(currentX >= photoCenterX && currentX < photoCenterX + 2 * cellSize &&
                          currentY >= photoCenterY && currentY < photoCenterY + 2 * cellSize))
                    {
                        g.FillRectangle(Brushes.Yellow, currentX, currentY, cellSize, cellSize);
                    }
                }
            }

            // Fotoğrafı belirlenen alana çiz
            g.DrawImage(photo, photoCenterX, photoCenterY, 2 * cellSize, 2 * cellSize);

            // Engel listesine ekle
            engelListesi.Add(new Rectangle(obstacleX, obstacleY, obstacleWidth, obstacleHeight));
        }



        public void CreateKus(Graphics g, int gridSize, int cellSize)
        {
            int obstacleX, obstacleY;
            int obstacleWidth = 2 * cellSize;
            int obstacleHeight = 12 * cellSize;
            bool isOverlap = false;

            do
            {
                isOverlap = false;
                obstacleX = random.Next(1, gridSize - 2) * cellSize;
                obstacleY = random.Next(1, gridSize - 13) * cellSize;
                Rectangle newObstacleRect = new Rectangle(obstacleX, obstacleY, obstacleWidth, obstacleHeight);

                // Engellerin birbirleriyle çakışıp çakışmadığını kontrol et
                foreach (Rectangle obstacleRect in engelListesi)
                {
                    if (newObstacleRect.IntersectsWith(obstacleRect))
                    {
                        isOverlap = true;
                        break;
                    }
                }
            } while (isOverlap);

            // Engelin merkezini hesapla ve fotoğrafı yerleştireceğin alanı belirle
            int photoCenterX = obstacleX + (obstacleWidth - 2 * cellSize) / 2;
            int photoCenterY = obstacleY + (obstacleHeight - 2 * cellSize) / 2;

            // Fotoğrafın yolu
            string photoPath = "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\Yeni klasor\\bird.jpg"; // Doğru yolu belirtin

            // Fotoğrafı yükle
            Image photo = Image.FromFile(photoPath);

            // Döngü içinde, engelin her bir hücresini sarı ile doldururken, fotoğrafın kaplayacağı alanı atla
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int currentX = obstacleX + j * cellSize;
                    int currentY = obstacleY + i * cellSize;

                    // Fotoğrafın yerleştirileceği alanı kontrol et
                    if (!(currentX >= photoCenterX && currentX < photoCenterX + 2 * cellSize &&
                          currentY >= photoCenterY && currentY < photoCenterY + 2 * cellSize))
                    {
                        g.FillRectangle(Brushes.Yellow, currentX, currentY, cellSize, cellSize);
                    }
                }
            }

            // Fotoğrafı belirlenen alana çiz
            g.DrawImage(photo, photoCenterX, photoCenterY, 2 * cellSize, 2 * cellSize);

            // Engel listesine ekle
            engelListesi.Add(new Rectangle(obstacleX, obstacleY, obstacleWidth, obstacleHeight));
        }



        //   ----------------------------- KONTROL GRUBU  -------------------------------------
        //   -----------------------------------------------------------------------------------------


        public bool IsCustomAri(Rectangle obstacleRect, int cellSize)
        {
            int obstacleX = obstacleRect.X;
            int obstacleY = obstacleRect.Y;

            int obstacleWidth = obstacleRect.Width;
            int obstacleHeight = obstacleRect.Height;

            int customAriWidth = 8 * cellSize;
            int customAriHeight = 2 * cellSize;

            return obstacleWidth == customAriWidth && obstacleHeight == customAriHeight;

        }


        public bool IsCustomKus(Rectangle obstacleRect, int cellSize)
        {
            int obstacleX = obstacleRect.X;
            int obstacleY = obstacleRect.Y;

            int obstacleWidth = obstacleRect.Width;
            int obstacleHeight = obstacleRect.Height;

            int customKusWidth = 2 * cellSize;
            int customKusHeight = 12 * cellSize;

            return obstacleWidth == customKusWidth && obstacleHeight == customKusHeight;

        }

        private void DrawImageInCell(Graphics g, int x, int y, int cellSize, string imagePath)
        {
            Image image = Image.FromFile(imagePath);
            Bitmap bmp = new Bitmap(image);

            for (int i = 0; i < cellSize; i++)
            {
                for (int j = 0; j < cellSize; j++)
                {
                    Color pixelColor = bmp.GetPixel(i, j);
                    if (pixelColor.R < 100 && pixelColor.G < 100 && pixelColor.B > 150)
                    {
                        bmp.SetPixel(i, j, Color.Transparent);
                    }
                }
            }

            g.DrawImage(bmp, x, y, cellSize, cellSize);
        }
    }

}
