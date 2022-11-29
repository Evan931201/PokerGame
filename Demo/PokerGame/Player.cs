using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    /// <summary>
    /// 玩家
    /// </summary>
    public class Player
    {
        public string Name { get; }

        public Player(string name)
        {
            this.Name = name.Trim();
        }
    }
}
