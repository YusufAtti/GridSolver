using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace try4
{
    public partial class Form1 : Form
    {
        // İzgara boyutunu tutar
        private int gridSize = 50;
        // Hedefe giden yolun noktalarını tutar
        private Queue<Point> path;
        // Animasyondaki mevcut adımı tutar
        private int currentStep = 0;
        // Hücre boyutunu tutar
        private int cellSize;
        // İzgara oluşturuldu mu kontrolü
        private bool isGridCreated = false;
        // Ekstra engeller oluşturuldu mu kontrolü
        private bool isExtraObstaclesCreated = false;
        // Önceki izgara boyutunu tutar
        private int prevGridSize = 0;
        private Engeller engeller;
        //private int cellSize = 10; // Her bir hücrenin piksel cinsinden boyutu
        public Karakter enkısayol;
        private List<Rectangle> obstacles = new List<Rectangle>(); // Engel listesi
        private List<Rectangle> goldObstacles = new List<Rectangle>(); // Altın sandık listesi
        private List<Rectangle> silverObstacles = new List<Rectangle>(); // Altın sandık listesi
        private List<Rectangle> diamondObstacles = new List<Rectangle>(); // Altın sandık listesi
        private List<Rectangle> copperObstacles = new List<Rectangle>(); // Altın sandık listesi
        private Animasyon animasyon;
        Lokasyon currentLocation;
        private bool isStartPointCreated = true;
        //Animasyonun başlatılıp başlatılmadığını kontrol etmek için bir bayrak



        public Form1()
        {
            //MessageBox.Show("01");
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.button1.Click += button1_Click;
            this.button2.Click += button2_Click;
            this.button3.Click += button3_Click;
            this.pictureBox1.Paint += PictureBox1_Paint;
        }



        private void button1_Click(object sender, EventArgs e)
        {


            if (int.TryParse(textBox1.Text, out int newSize) && newSize > 52 && newSize % 2 == 0)
            {
                gridSize = newSize;
                currentStep = 0;
                isGridCreated = true;
                prevGridSize = 0;
                path = null;
                isExtraObstaclesCreated = false;
                isStartPointCreated = true; // Başlangıç noktası oluşturuldu olarak işaretle
                                            // pictureBox1.Invalidate();
                InitializeGame(); // Prepare the game
            }
            else
            {
                MessageBox.Show("Geçerli bir pozitif çift sayı girin ve 52'den büyük olsun!");
            }

        }



        private void button2_Click(object sender, EventArgs e)
        {

            if (!isGridCreated)
            {
                MessageBox.Show("Lütfen önce haritayı oluşturun!");
                return;
            }
            if (!isStartPointCreated)
            {
                MessageBox.Show("Lütfen önce başlangıç noktasını oluşturun!");
                return;
            }
            CreateFogLayer();   //---->   -----------------------------------------------SİSİ KALDIRMAK İÇİN BURAYI SİLİN
            //DrawImageOnGrid(); // İzgaradaki her kareye resmi çizer ---->-----------------------------------------------  Aşağıdaki metodu aktif edebilirsiniz
            StartAnimation();

        }


        private void InitializeGame()
        {
            cellSize = Math.Min(pictureBox1.Width, pictureBox1.Height) / gridSize;
            engeller = new Engeller();

            if (!isExtraObstaclesCreated) // Bu kontrol eklendi
            {
                obstacles = engeller.engelListesi;
                goldObstacles = engeller.altinEngelListesi;
                silverObstacles = engeller.gumusEngelListesi;
                diamondObstacles = engeller.diaEngelListesi;
                copperObstacles = engeller.bakırEngelListesi;
                // Sadece engeller ve altın sandıklar ilk kez oluşturulduğunda eklenir
            }

            pictureBox1.Invalidate(); // Izgarayı ve mevcut engelleri yeniden çiz

        }


        //  -------------------------------------------------------------------------------------------------------------------------------------  İzgaradaki her kareye resmi çizer   ------------------------------------------------------------------------------------------------
        /*  private void DrawImageOnGrid()
          {
              string imagePath = "C:\\Users\\berat\\OneDrive\\Masaüstü\\Otomasyon sistem\\p1\\cllod3.png"; // Buraya resminizin yolu gelecek
              Image gridImage = Image.FromFile(imagePath);

              pictureBox1.Paint += (sender, e) =>
              {
                  for (int x = 0; x < gridSize; x++)
                  {
                      for (int y = 0; y < gridSize; y++)
                      {
                          int cellX = x * cellSize;
                          int cellY = y * cellSize;
                          e.Graphics.DrawImage(gridImage, cellX, cellY, cellSize, cellSize);
                      }
                  }
              };
              pictureBox1.Invalidate(); // PictureBox'ı yeniden çizmek için
          }
          */


        //  -------------------------------------------------------------------------------------------------------------------------------------  SİSİ KALDIRMAK İÇİN BURAYI SİLİN------------------------------------------------------------------------------------------------
        private void CreateFogLayer()
        {
            // PictureBox boyutlarında bir Bitmap oluştur
            Bitmap fogLayer = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            using (Graphics g = Graphics.FromImage(fogLayer))
            {
                // Sis efekti için saydamlığı ayarla (örneğin, %50 saydamlık)
                Brush fogBrush = new SolidBrush(Color.FromArgb(200, Color.Gray));

                // Tüm Bitmap üzerine sis efektini çiz
                g.FillRectangle(fogBrush, 0, 0, fogLayer.Width, fogLayer.Height);
            }

            // Oluşturulan sis katmanını PictureBox'un Image özelliğine ata
            // Eğer PictureBox'ta zaten bir görüntü varsa ve onu korumak istiyorsanız, önce onu çizmeniz gerekebilir
            pictureBox1.Image = fogLayer;
        }   

    


        // PictureBox1'in boyama olayını işleyen metot
        public void PictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if (!isGridCreated) return;



            DrawGrid(e.Graphics);
            DrawObstaclesAndGold(e.Graphics);


            //   MessageBox.Show("03");
            // Sol yarısını LightSteelBlue renginde, sağ yarısını Salmon renginde şeffaf yapar
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            int halfWidth = pictureBox1.Width / 2;
            Rectangle leftRect = new Rectangle(0, 0, halfWidth, pictureBox1.Height);
            Rectangle rightRect = new Rectangle(halfWidth, 0, halfWidth, pictureBox1.Height);
            Brush leftBrush = new SolidBrush(Color.FromArgb(80, Color.LightSteelBlue));
            Brush rightBrush = new SolidBrush(Color.FromArgb(80, Color.Salmon));
            e.Graphics.FillRectangle(leftBrush, leftRect);
            e.Graphics.FillRectangle(rightBrush, rightRect);

            if (!isExtraObstaclesCreated)
            {
                MessageBox.Show("Obstacles and Gold are being drawn.");
                // Create and draw obstacles and golds here
                isExtraObstaclesCreated = true;
            }

            if (!isStartPointCreated)
            {
                Point startLocation = enkısayol.GetCurrentLocation();
                int startX = startLocation.X * cellSize;
                int startY = startLocation.Y * cellSize;
                e.Graphics.FillRectangle(Brushes.Orange, startX, startY, cellSize, cellSize);
                return;
            }
            InitializeGame2();

        }




        private void InitializeGame2()
        {
            // Engeller ve altın sandıklar `Enkısayol` sınıfına bilgi olarak geçirilmeli
            enkısayol = new Karakter(gridSize, cellSize, obstacles, goldObstacles, silverObstacles, diamondObstacles, copperObstacles);
            //animasyon = new Animasyon(this, obstacles);
        }



        private void StartAnimation()
        {
            // Enkısayol sınıfında tanımlı arama algoritmasını başlat
            enkısayol.MoveAndCollectChests(); // Bu metot void döndüğü için doğrudan bir değişkene atanamaz.

            // Bu satır, enkısayol nesnesi üzerindeki GetHareketKuyrugu metotunu çağırarak karakterin hareket kuyruğunu alır.
            //Bu kuyruk, karakterin hareket ettiği konumların bir listesini içerir.
            var hareketKuyrugu = enkısayol.GetHareketKuyrugu(); // Hareket kuyruğunu almak için

            // Eğer animasyon null ise, bir yeni Animasyon nesnesi oluşturulur. 
            if (animasyon == null)
            {
                animasyon = new Animasyon(this, obstacles); // Animasyon nesnesini yarat
            }
            animasyon.AnimatePath(hareketKuyrugu, pictureBox1, cellSize, new Lokasyon(enkısayol.GetCurrentLocation().X, enkısayol.GetCurrentLocation().Y));
        }




        // Izgarayı çizen metot
        private void DrawGrid(Graphics g)
        {
            try
            {
                for (int x = 0; x <= this.pictureBox1.Width; x += cellSize)
                {
                    g.DrawLine(Pens.Black, x, 0, x, this.pictureBox1.Height);
                }
                for (int y = 0; y <= this.pictureBox1.Height; y += cellSize)
                {
                    g.DrawLine(Pens.Black, 0, y, this.pictureBox1.Width, y);
                }
                //    MessageBox.Show("Grid çizildi");
            }
            catch (Exception ex)
            {
                // Hata yakalandığında burada işlemler yapılabilir.
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }


        private void DrawObstaclesAndGold(Graphics g)
        {
            // Assuming engeller is an instance of a class that contains methods to draw obstacles and golds
            if (prevGridSize != gridSize || !isExtraObstaclesCreated)
            {
                // MessageBox.Show("04");
                // Draw obstacles

                engeller.CreateAri(g, gridSize, cellSize);
                engeller.CreateAri(g, gridSize, cellSize);
                engeller.CreateKus(g, gridSize, cellSize);
                engeller.CreateDag(g, gridSize, cellSize);
                engeller.CreateDag(g, gridSize, cellSize);
                engeller.CreateAgac(g, gridSize, cellSize);
                engeller.CreateTas(g, gridSize, cellSize);
                engeller.CreateDuvar(g, gridSize, cellSize);
                // Assuming AltınSandık is the method to draw golds
                engeller.AltınSandık(g, gridSize, cellSize);
                engeller.GumusSandık(g, gridSize, cellSize);
                engeller.DiaSandık(g, gridSize, cellSize);
                engeller.BakirSandık(g, gridSize, cellSize);
                prevGridSize = gridSize;
                isExtraObstaclesCreated = true;
            }


        }

        public void LogToRichTextBox2(string message)
        {
            if (InvokeRequired) // UI thread dışında bir thread'den çağrı yapılıyorsa
            {
                Invoke(new Action<string>(LogToRichTextBox2), new object[] { message });
            }
            else
            {
                richTextBox2.AppendText(message + Environment.NewLine);
            }
        }
        public List<Rectangle> GetChestList(string chestType)
        {
            switch (chestType)
            {
                case "Altın":
                    return goldObstacles;
                case "Gümüş":
                    return silverObstacles;
                case "Elmas":
                    return diamondObstacles;
                case "Bakır":
                    return copperObstacles;
                default:
                    return new List<Rectangle>();
            }
        }

        private void SelectChestType(string chestType)
        {

            if (enkısayol != null)
            {
                enkısayol.Name = chestType;
            }
        }

        // Bir sandığın toplanıp toplanmadığını kontrol eden metod
        private HashSet<Point> collectedChests = new HashSet<Point>();
        public bool IsChestCollected(Rectangle chest)
        {
            // Sandık zaten toplanmışsa true, değilse false döner
            return collectedChests.Contains(new Point(chest.X / cellSize, chest.Y / cellSize));
        }


        // Bir sandığı toplandı olarak işaretleyen metod
        public void MarkChestAsCollected(Rectangle chest)
        {
            // Sandığı toplanmış olarak işaretle
            collectedChests.Add(new Point(chest.X / cellSize, chest.Y / cellSize));
        }


        public void UpdateStepLabel(int step)
        {
            // MessageBox.Show("08");
            if (label1.InvokeRequired)
            {
                label1.Invoke(new Action<int>(UpdateStepLabel), step);
            }
            else
            {
                label1.Text = "Geçilen Adım Sayısı: " + step;
            }
        }



        public void AddCoordinatesToRichTextBox(Lokasyon lokasyon)
        {
            // MessageBox.Show("09");
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action<Lokasyon>(AddCoordinatesToRichTextBox), lokasyon);
            }
            else
            {
                richTextBox1.AppendText($"X: {lokasyon.X}, Y: {lokasyon.Y}\n");
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {


            // Animasyonu durdur
            if (animasyon != null)
            {
                animasyon.StopAnimation();
            }

            // Izgarayı ve tüm engelleri temizle
            richTextBox1.Clear();
            richTextBox2.Clear();
            UpdateStepLabel(0);
            isGridCreated = false;
            isExtraObstaclesCreated = false;
            obstacles.Clear(); // Engeller listesini temizle
            goldObstacles.Clear(); // Altın engeller listesini temizle
            silverObstacles.Clear(); // Gümüş engeller listesini temizle
            diamondObstacles.Clear(); // Elmas engeller listesini temizle
            copperObstacles.Clear(); // Bakır engeller listesini temizle
            pictureBox1.Invalidate(); // PictureBox'ı yeniden çizerek temizle
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}