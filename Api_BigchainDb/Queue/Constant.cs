using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BigchainDb.Queue
{
    public class Constant
    {
        //https://helpex.vn/question/chia-danh-sach-thanh-cac-danh-sach-con-voi-linq-5cb718f7ae03f62598dd6c36
        public static List<List<T>> Chunk<T>(List<T> theList, int chunkSize)
        {
            List<List<T>> result = theList
                .Select((x, i) => new {
                    data = x,
                    indexgroup = i / chunkSize
                })
                .GroupBy(x => x.indexgroup, x => x.data)
                .Select(g => new List<T>(g))
                .ToList();

            return result;
        }
    }
}
