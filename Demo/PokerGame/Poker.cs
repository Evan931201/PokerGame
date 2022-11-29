using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    /// <summary>
    /// 火柴
    /// </summary>
    public class Poker
    {
        /// <summary>
        /// 火柴的编号
        /// </summary>
        public string Number { get; }

        /// <summary>
        /// 火柴所在行
        /// </summary>
        public int Line { get; }

        /// <summary>
        /// 是否已经被抓取
        /// </summary>
        public bool Taked { get; set; } = false;

        /// <summary>
        /// 初始化火柴
        /// </summary>
        /// <param name="pokerNumber">火柴编号</param>
        /// <param name="pokerLine">火柴所在的行号</param>
        public Poker(string pokerNumber, int pokerLine)
        {
            this.Number = pokerNumber.Trim();
            this.Line = pokerLine;
        }
    }
}
