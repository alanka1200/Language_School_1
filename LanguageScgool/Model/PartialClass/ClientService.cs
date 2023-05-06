using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageScgool.Model
{
    public partial class ClientService
    {
        public string CollorEnd
        {
            get
            {
                if ((StartTime - DateTime.Now) < TimeSpan.FromHours(5))
                {
                    return $"#FFE7FABF";
                }
                else
                {
                    return "";
                }

            }

        }
        public string TimeStart
        {
            get
            {
                var time = StartTime - DateTime.Now;
                return $"Начало в {StartTime.ToString("F")}.  осталось: {time.ToString(@"dd")} дней {time.ToString(@"hh")} ч {time.ToString(@"mm")} мин.";
            }
        }

        public string ColorRed
        {
            get
            {
                if (StartTime - DateTime.Now < TimeSpan.FromHours(1))
                {
                    return "red"; 
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
