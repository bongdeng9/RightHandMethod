using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm1
{
    class Pos
    {
        public Pos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;
    }
    class Player
    {
        // 설계적 감 처음에는 누구나 없음 경험이 쌓이면 됨
        // 플레이어의 위치를 Board.Render 에서 접근
        public int PosY { get; private set; } // 외부에서 플레이어 위치를 get으로 질의(Read = get)는 할 수 있겠지만,  (Write = set)은 할 수 없다
        public int PosX { get; private set; } // private set : 플레이어의 좌표정보는 자기 자신만 바꿀것
        Random _random = new Random();
        Board _board;

        enum Dir
        {
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3 
        }

        int _dir = (int)Dir.Up;
        List<Pos> _points = new List<Pos>();

        public void Initialize(int posY, int posX, Board board)
        {
            // 현재 상태르 기반으로 한칸씩  / 모든 경로를 계산해놓은다음 하나씩 업데이트에서 꺼내쓰기
            PosY = posY;
            PosX = posX;
            _board = board;

            // 현재 바라보고 있는 방향을 기준으로 좌표 변화를 나타낸다.
            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 }; 
            int[] rightY = new int[] { 0, -1, 0, 1};
            int[] rightX = new int[] { 1, 0, -1, 0}; // 인덱스 : {Up 0 , Left 1,  Down 2,  Right 3 } 일 때 y, x 좌표 변화를 나타냄.

            _points.Add(new Pos(PosY, PosX));
            // 목적지 도착하기 전에는 계속 실행
            while (PosY != board.DestY || PosX != board.DestX)
            {
                // 우수법 오른손 법칙 
                // 1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는 지 확인.
                if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    // 오른쪽 방향으로 90도 회전
                    _dir = (_dir - 1 + 4) % 4; // -1 빼는거는 한 칸 위로 올리는 거 +4 더한거는 양수로 취급 %4 4로 나눈 나머지
                    // 앞으로 한 보 전진.
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));

                }
                // 2. 현재 바라보는 방향을 기준으로 전진할 수 있는 지 확인.
                else if (_board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
                {
                    // 앞으로 한 보 전진
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));

                }
                else
                {
                    // 왼쪽으로 방향으로 90도 회전
                    _dir = (_dir + 1 + 4) % 4;
                    // 턴을 넘긴다.
                }
            }
        }

        const int MOVE_TICK = 10; // 0.1초
        int _sumTick = 0;
        int _lastIndex = 0;
        public void Update(int deltaTick) // 30분의 1초마다 업데이트가 실행되는건 너무 빠르니까 deltaTick 을 도입해서 업데이트를 실행할 지 , 넘길 지 결정.
        {
            if (_lastIndex >= _points.Count)
                return;

            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                // 여기다가 0.1초마다 실행될 로직을 넣어준다.
                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;
                // 배열을 사용할 때는 인덱스가 범위를 초과하지 않는 지 잘 확인해야됨.
            }
        }
    }
}
