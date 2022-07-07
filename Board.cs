using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm1
{

    class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] Tile { get; private set; } // 배열임
        public int Size { get; private set; }

        public int DestY { get; private set; }
        public int DestX { get; private set; }


        Player _player;
        public enum TileType
        {
            Empty,
            Wall,
        }

        public void Initialize(int size, Player player)
        {
            if (size % 2 == 0)
                return;

            _player = player;

            Tile = new TileType[size, size];
            Size = size;

            DestY = Size - 2;
            DestX = Size - 2;
            // 갈 수 있는 타일과 갈 수 없는 타일 구분 enum 값으로 구분하는것이 더 낫다

            //GenerateByBinaryTree();
            GenerateBySideWinder();
        }
        
        void GenerateByBinaryTree()
        {
            // 일단 길을 다 막아버리는 작업
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            // Binary Tree Algorithm
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue; // 아래 랜덤함수를 타지 않도록 continue 로 넘어간다!
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue; // 아래 랜덤함수를 타지 않도록 continue 로 넘어간다!
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        Tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }
        void GenerateBySideWinder()
        {
            // 일단 길을 다 막아버리는 작업
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            // Binary Tree Algorithm
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                int count = 1;
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue; // 아래 랜덤함수를 타지 않도록 continue 로 넘어간다!
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue; // 아래 랜덤함수를 타지 않도록 continue 로 넘어간다!
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randomindex = rand.Next(0, count);
                        Tile[y + 1, x - randomindex * 2] = TileType.Empty;
                        count = 1;
                    }
                }
            }
        }
        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor; // 렌더링이 실행 되더라도 이전 상태 영향 안주기 위해

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {   // 여기서 플레이어 좌표를 받아와서 if else 문으로 분기를 처리하면 됨
                    // 그 좌표랑 현재 y, x가 일치하면 플레이어 전용 색상으로 표시
                    if (y == _player.PosY && x == _player.PosX)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (y == DestY && x == DestX)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = GetTileColor(Tile[y, x]);
   
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }
        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
