using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm1
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Player player = new Player();
            board.Initialize(25, player);
            player.Initialize(1, 1, board);
            // 설계 고민 플레이어가 움직이는 것 구현 즉 길 찾는 ai 를 어디서 구현해야될까?
            // 플레이어 좌표는 플레이어 로직 안에서 구현하는게 맞음
            // 갈 수 있는 지 없는 지 판단하기 위해서는 board 에 대한 정보가 필요함.

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;

            int lastTick = 0;
            while (true)
            {
                #region 프레임관리
                // FPS 프레임 (60 프레임) 이 루프가 1초에 몇번 업데이트 하는지 
                int currentTick = System.Environment.TickCount;

                // 만약에 경과한 시간이 1/30초 보다 작다면,
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;
                #endregion

                // 입력

                // 로직 데이터가 변하는 부분
                player.Update(deltaTick); // 1000 / 30 초마다 업데이트가 실행됨.

                // 렌더링 그림만 그리는 부분
                Console.SetCursorPosition(0, 0);
                board.Render();
            }

        }
    }
}
