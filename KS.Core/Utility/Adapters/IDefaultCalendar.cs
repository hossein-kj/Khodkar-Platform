using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Core.Utility.Adapters
{
    public interface IDefaultCalendar
    {
        int GetYear(DateTime time);
        int GetMonth(DateTime time);
        int GetDayOfMonth(DateTime time);

    }
}
