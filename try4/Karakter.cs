using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace try4
{
    public class Karakter
    {
  
        public Dictionary<string, int> chestTypeToID = new Dictionary<string, int>()
        { {"Altın", 0},  {"Gümüş", 1},  {"Elmas", 2}, {"Bakır", 3}  };
        public int ID { get; set; }
        public string Name { get; set; }
        private HashSet<Point> visitedPoints = new HashSet<Point>();
        private HashSet<Point> collectedGolds = new HashSet<Point>();
        private readonly int gridSize;
        private int cellSize;
        private readonly List<Rectangle> obstacles;
        private readonly List<Rectangle> goldObstacles;
        private readonly List<Rectangle> silverObstacles;
        private readonly List<Rectangle> diamondObstacles;
        private readonly List<Rectangle> copperObstacles;
        private Point currentLocation;
        private Form1 form;
        private readonly Random random = new Random();
        private Queue<Point> hareketKuyrugu = new Queue<Point>();

        private Dictionary<Point, Point> cameFrom = new Dictionary<Point, Point>();
        private Dictionary<Point, double> gScore = new Dictionary<Point, double>();
        private Dictionary<Point, double> fScore = new Dictionary<Point, double>();


        public Karakter(int gridSize, int cellSize, List<Rectangle> obstacles, List<Rectangle> goldObstacles, List<Rectangle> silverObstacles, List<Rectangle> diamondObstacles, List<Rectangle> copperObstacles)
        {
            this.gridSize = gridSize;
            this.cellSize = cellSize;
            this.obstacles = obstacles;
            this.goldObstacles = goldObstacles;
            this.silverObstacles = silverObstacles;
            this.diamondObstacles = diamondObstacles;
            this.copperObstacles = copperObstacles;
            InitializeCharacterLocation();
        }

        //Bu metot, karakterin başlangıç konumunu belirler ve engellerle çakışmadığından emin olur
        private void InitializeCharacterLocation()
        {
            //Bu değişken, başlangıç noktasının güvenli olup olmadığını kontrol etmek için kullanılır.
            bool isSafeStartPoint = false;
            //Döngü, başlangıç noktası güvenli hale gelene kadar devam eder.
            while (!isSafeStartPoint)
            {
                // Karakterin sınır hücrelerden en az 3 hücre içeride başlamasını sağla
                currentLocation = new Point(random.Next(3, gridSize - 3), random.Next(3, gridSize - 3));

                // Başlangıç noktasının güvenli olduğunu varsayıyoruz
                isSafeStartPoint = true;

                // Her bir engelle başlangıç noktasının çakışıp çakışmadığını kontrol et
                foreach (var obstacle in obstacles)
                {
                    if (obstacle.Contains(new Point(currentLocation.X * cellSize, currentLocation.Y * cellSize)))
                    {
                        // Eğer başlangıç noktası herhangi bir engelle çakışıyorsa, tekrar dene
                        isSafeStartPoint = false;
                        break;
                    }
                }
            }

            //Başlangıç noktası, ziyaret edilen noktalar listesine eklenir.
            visitedPoints.Add(currentLocation);
            //Başlangıç noktası, karakterin hareketlerini takip etmek için kullanılan kuyruğa eklenir.
            hareketKuyrugu.Enqueue(currentLocation); // Başlangıç konumunu kuyruğa ekle
        }




        public void MoveAndCollectChests()
        {
            // Sandık türlerini ve onların listelerini bir sözlükte sakla
            var chestTypes = new Dictionary<string, List<Rectangle>>()
    {
        {"Altın", goldObstacles},  // AltınSandık listesi varsayımı
        {"Gümüş", silverObstacles}, // GümüşSandık listesini ekleyin
        {"Elmas", diamondObstacles}, // DiaSandık listesini ekleyin
        {"Bakır", copperObstacles} // BakirSandık listesini ekleyin
    };

            foreach (var chestType in chestTypes)
            {
                // Belirli bir türdeki sandıklar için toplama işlemi
                CollectChestsOfType(chestType.Key, chestType.Value);
            }
        }

        private void CollectChestsOfType(string chestTypeName, List<Rectangle> chestList)
        {
            Console.WriteLine($"{chestTypeName} sandıkları toplanıyor...");
            int collectedChestsCount = 0; // Toplanan sandık sayısını sıfırla

            while (collectedChestsCount < chestList.Count)
            {
                Point? closestChest = null;
                double closestDistance = double.MaxValue;

                // En yakın sandığı bul
                foreach (var chest in chestList.Where(c => !collectedGolds.Contains(new Point(c.X / cellSize, c.Y / cellSize))))
                {
                    double distance = Distance(currentLocation, new Point(chest.X / cellSize, chest.Y / cellSize));
                    if (distance < closestDistance)
                    {
                        closestChest = new Point(chest.X / cellSize, chest.Y / cellSize);
                        closestDistance = distance;
                    }
                }

                // En yakın sandığa git
                if (closestChest.HasValue)
                {
                    Queue<Point> pathToChest = CalculatePath(currentLocation, closestChest.Value);
                    while (pathToChest.Count > 0)
                    {
                        Point nextStep = pathToChest.Dequeue();
                        currentLocation = nextStep;
                        hareketKuyrugu.Enqueue(currentLocation);
                        visitedPoints.Add(currentLocation);
                        if (chestList.Any(c => new Point(c.X / cellSize, c.Y / cellSize) == currentLocation))
                        {
                            collectedGolds.Add(currentLocation); // Toplanan sandığı kaydet
                            collectedChestsCount++; // Toplanan sandık sayısını artır
                        }
                    }
                }
                else
                {
                    // Bu türde daha fazla sandık kalmadıysa döngüyü kır
                    break;
                }
            }

            Console.WriteLine($"{chestTypeName} sandıkları toplandı.");
        }





        //Metodun dönüş tipi List<Point> olarak belirlenmiştir, yani bu metod altınların konumunu içeren bir liste döndürecektir.
        private List<Point> GetVisibleGolds()
        {

            return goldObstacles
                .Where(gold => Math.Abs(gold.X / cellSize - currentLocation.X) <= 3 && Math.Abs(gold.Y / cellSize - currentLocation.Y) <= 3)
                .Select(gold => new Point(gold.X / cellSize, gold.Y / cellSize))
                .ToList();
        }


        //Bu metod, karakterin hareket edebileceği bir sonraki güvenli konumu bulmak için kullanılır
        private Point FindNextSafeLocation()
        {
            // Bu satır, karakterin hareket edebileceği dört olası yönu belirler
            var possibleDirections = new List<Point> { new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1) };

            //Bu döngü, karakterin hareket edebileceği olası yönlere karışık bir sıra ile erişmek için kullanılır. Yani, karakter rastgele bir yönde hareket eder.
            foreach (var direction in possibleDirections.OrderBy(x => random.Next()))
            {
                //Bu satır, karakterin bir sonraki adımı atacağı konumu hesaplar. Mevcut konumun X ve Y koordinatlarına,
                //olası yönde belirlenen X ve Y koordinatları eklenerek bir sonraki konum hesaplanır.
                var nextPoint = new Point(currentLocation.X + direction.X, currentLocation.Y + direction.Y);

                //  Bu koşul ifadesi, bir sonraki konumun güvenli olup olmadığını kontrol eder. IsPointSafe metodu, bir konumun güvenli olup olmadığını kontrol ederken, 
                //IsWithinBounds metodu ise bir konumun izgara sınırları içinde olup olmadığını kontrol eder.
                if (IsPointSafe(nextPoint) && IsWithinBounds(nextPoint))
                {
                    //Eğer bir sonraki konum güvenli ve izgara sınırları içinde ise, bu konum doğrudan döndürülür.
                    return nextPoint;
                }
            }
            // Eğer hiçbir güvenli nokta bulunamazsa, mevcut konum döndürülür
            return currentLocation; // Güvenli bir nokta bulunamazsa mevcut konumu döndür
        }

        //belirtilen bir noktanın güvenli olup olmadığını kontrol ede
        private bool IsPointSafe(Point point)
        {
            return !obstacles.Any(obstacle => obstacle.Contains(new Point(point.X * cellSize, point.Y * cellSize)));
        }

        // karakterin mevcut konumunu döndürmek için kullanılır
        public Point GetCurrentLocation()
        {
            return currentLocation;
        }

        //karakterin hareket ettiği konumların bir kuyruğunu döndürmek için kullanılır. 
        // bu metod bir noktalar kuyruğu (Queue<Point>) döndürecektir.
        public Queue<Point> GetHareketKuyrugu()
        {
            lock (hareketKuyrugu)
            {
                return new Queue<Point>(hareketKuyrugu);
            }
        }



        

        private Queue<Point> CalculatePath(Point start, Point goal)
        {
            var openSet = new HashSet<Point> { start };
            cameFrom.Clear();
            gScore.Clear();
            fScore.Clear();

            gScore[start] = 0;
            fScore[start] = HeuristicCostEstimate(start, goal);

            while (openSet.Count > 0)
            {
                Point current = openSet.OrderBy(p => fScore.ContainsKey(p) ? fScore[p] : double.MaxValue).First();

                if (current == goal)
                {
                    return ReconstructPath(cameFrom, current);
                }

                openSet.Remove(current);

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (IsObstacle(neighbor) || !IsWithinBounds(neighbor))
                    {
                        continue;
                    }

                    double tentativeGScore = gScore[current] + Distance(current, neighbor);

                    if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, goal);

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }

            return new Queue<Point>();
        }

        private double HeuristicCostEstimate(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        private Queue<Point> ReconstructPath(Dictionary<Point, Point> cameFrom, Point current)
        {
            var path = new List<Point>();
            while (cameFrom.ContainsKey(current))
            {
                path.Add(current);
                current = cameFrom[current];
            }
            path.Reverse();
            return new Queue<Point>(path);
        }

        private IEnumerable<Point> GetNeighbors(Point p)
        {
            var offsets = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
            foreach (var (dx, dy) in offsets)
            {
                var neighbor = new Point(p.X + dx, p.Y + dy);
                yield return neighbor;
            }
        }

        private bool IsObstacle(Point p)
        {
            // Engellerin piksel bazlı konumunu hücre bazına çevir
            return obstacles.Any(obstacle => obstacle.Contains(p.X * cellSize, p.Y * cellSize));
        }

        private bool IsWithinBounds(Point p)
        {
            // Izgaranın sınır karelerini de güvenli olmayan kareler olarak değerlendir.
            return p.X > 0 && p.X < gridSize - 1 && p.Y > 0 && p.Y < gridSize - 1;
        }

        private double Distance(Point a, Point b)
        {
            // Basit Manhattan mesafesi; gerçek projede farklı mesafe hesaplamaları gerekebilir.
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }



    }
}