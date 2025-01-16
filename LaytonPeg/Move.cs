using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaytonPeg
{
    struct Move
    {
        public int SourceRow { get; }
        public int SourceCol { get; }
        public int DestRow { get; }
        public int DestCol { get; }
        public int RemovedRow { get; }
        public int RemovedCol { get; }

        public Move(int sourceRow, int sourceCol, int destRow, int destCol, int removedRow, int removedCol)
        {
            SourceRow = sourceRow;
            SourceCol = sourceCol;
            DestRow = destRow;
            DestCol = destCol;
            RemovedRow = removedRow;
            RemovedCol = removedCol;
        }

        public override string ToString()
        {
            return $"[{SourceRow}, {SourceCol}] -> [{DestRow}, {DestCol}]";
        }
    }
}
