using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    public class Game
    {
        /// <summary>
        /// 火柴列表
        /// </summary>
        private Dictionary<string, Poker> Pokers { get; } = new Dictionary<string, Poker>();

        /// <summary>
        /// 玩家列表
        /// </summary>
        private List<Player> Players { get; } = new List<Player>();

        /// <summary>
        /// 添加一个玩家
        /// </summary>
        public void AddPlayer(Player player)
        {
            if (player.Equals(null)
                || string.IsNullOrWhiteSpace(player.Name)
                || Players.Count(p => p.Name == player.Name) > 0)
            {
                throw new ArgumentException("请添加一个正确的玩家。");
            }

            this.Players.Add(player);
        }

        /// <summary>
        /// 添加一个火柴
        /// </summary>
        public void AddPoker(Poker poker)
        {
            if (poker.Equals(null)
                || string.IsNullOrWhiteSpace(poker.Number)
                || Pokers.Values.Count(p => p.Number == poker.Number.Trim()) > 0)
            {
                throw new ArgumentException("请添加一个正确的火柴。");
            }

            this.Pokers.Add(poker.Number.Trim(), poker);
        }

        object obj = new object();

        /// <summary>
        /// 抓取火柴
        /// </summary>
        /// <returns>是否抓取成功</returns>
        private bool TakePoker(string[] pokerNumbers)
        {
            lock (obj)
            {
                bool canTake = true;

                // 验证有效性
                if (pokerNumbers.Length <= 0
                    || pokerNumbers.Count(p => string.IsNullOrWhiteSpace(p)) > 0 // 序号为空
                    || pokerNumbers.Count(p => Pokers.ContainsKey(p) && Pokers[p].Taked == false) <= 0) // 没有可以拾取的火柴
                {
                    canTake = false;
                }
                else
                {
                    int line = Pokers[pokerNumbers[0]].Line;
                    for (int i = 0; i < pokerNumbers.Length; i++)
                    {
                        if (line != Pokers[pokerNumbers[i]].Line)
                        {
                            canTake = false;
                            break;
                        }
                    }
                }                

                // 验证失败
                if (!canTake)
                    return false;

                // 开始抓取
                for (int i = 0; i < pokerNumbers.Length; i++)
                {
                    Pokers[pokerNumbers[i]].Taked = true;
                }

                return true;
            }            
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        public void Play()
        {
            StringBuilder tips = new StringBuilder();
            tips.AppendLine("抓火柴游戏现在开始，游戏规则如下：");
            tips.AppendLine("1) 抓取多根火柴的序号以英文逗号隔开。");
            tips.AppendLine("2) 每次可以抓取任意根火柴，但不能跨行抓取。");
            tips.AppendLine("3) 抓取到最后一根火柴的玩家为本次游戏的赢家。");
            Console.WriteLine(tips.ToString());

            while (true)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    Console.ResetColor();

                    // 提取各行火柴编号
                    Dictionary<int, List<string>> lineNumbers = new Dictionary<int, List<string>>();
                    foreach (Poker poker in this.Pokers.Values.ToList())
                    {
                        if (!lineNumbers.ContainsKey(poker.Line))
                            lineNumbers.Add(poker.Line, new List<string>());

                        if(poker.Taked == false)
                            lineNumbers[poker.Line].Add(poker.Number);
                    }
                    lineNumbers = lineNumbers.OrderBy(p => p.Key).ToDictionary(item => item.Key, item => item.Value);

                    // 输出火柴
                    Console.WriteLine("--------------------------火柴排列如下--------------------------");
                    foreach (var key in lineNumbers.Keys)
                    {
                        Console.WriteLine($"第{key}行：" + string.Join("---", lineNumbers[key].ToArray()));
                    }
                    Console.WriteLine($"玩家【{Players[i].Name}】开始抓取：");
                    
                    // 抓取火柴
                    if (TakePoker(Console.ReadLine().Split(',')))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("抓取成功！");

                        if (Pokers.Count(p => !p.Value.Taked) <= 0)
                        {
                            Console.WriteLine($"玩家【{Players[i].Name}】获得胜利！");
                            return;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("抓取失败，请重新抓取！");
                        i--;                       
                    }
                }
            }
        }
    }
}
