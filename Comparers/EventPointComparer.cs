using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Teme_Geometrie_Computationala.Comparers
{
    public  class EventPointComparer : IComparer<Helper.PointEvent>
    {
        public int Compare(Helper.PointEvent a, Helper.PointEvent b)
        {
            if (a.point.Y < b.point.Y)
                return 1;
            else if (a.point.Y == b.point.Y && a.point.X < b.point.X) return 1;
            return -1;
        }
    }
}
