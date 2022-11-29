using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            // 添加玩家
            game.AddPlayer(new Player("张三"));
            game.AddPlayer(new Player("李四"));
            //game.AddPlayer(new Player("王二"));

            // 添加火柴
            for (int i = 1; i <= 15; i++)
            {
                if (i <= 3)
                    game.AddPoker(new Poker(i.ToString(), 1)); // 第一行
                else if (i <= 8)
                    game.AddPoker(new Poker(i.ToString(), 2)); // 第二行
                else if (i <= 15)
                    game.AddPoker(new Poker(i.ToString(), 3)); // 第三行
            }

            // 开始游戏
            game.Play();
        }
    }
}
