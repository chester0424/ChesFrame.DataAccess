using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ChesFrame.Utility.DataAccess
{
    public class PageInfo
    {
        public int PageIndex { get; set; }

        public int PageSze { get; set; }

        public int AllCount { get; set; }

        public string OrderBy
        {
            get
            {
                return string.Format(" {0}{1}{2} ", OrderType.ToString(), " ", OrderByFild);
            }
            set
            {
                var values = value.Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (values != null && values.Count() >= 2)
                {
                    OrderType orderType = DataAccess.OrderType.Asc;
                    if (Enum.TryParse(values[0], out orderType))
                    {
                        OrderType = orderType;

                        OrderByFild = values[1];
                    }
                }
            }
        }

        public OrderType OrderType { get; set; }

        public string OrderByFild { get; set; }
    }
    public enum OrderType
    {
        Asc,
        Desc
    }
}
