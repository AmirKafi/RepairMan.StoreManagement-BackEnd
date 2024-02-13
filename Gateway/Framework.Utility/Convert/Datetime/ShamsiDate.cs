using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Convert.Datetime
{
    /// <summary>
    /// only cary values from  PersianCalendar methods with added value to use in custom areas
    /// </summary>
    internal class ShamsiDate
    {

        /// <summary>
        /// this constructor uses PersianCalendar and store its method returned values in a more friendler structor 
        /// and properties of this class
        /// </summary>
        /// <param name="date"></param>
        internal ShamsiDate(DateTime date)
        {
            EquivalentGoergianDate = date;
            var pDate = new System.Globalization.PersianCalendar();

            Saal = pDate.GetYear(date);
            Mah = pDate.GetMonth(date);
            RoozeMah = pDate.GetDayOfMonth(date);
            //time  
            Saat = pDate.GetHour(date);
            Daghighe = pDate.GetMinute(date);
            Saniyeh = pDate.GetSecond(date);

            RoozeHafteh = ConvertDate.MapWeekDayToNum(pDate.GetDayOfWeek(date));
        }

        internal ShamsiDate(int saal, int mah, int rooz)
        {

            Saal = saal;
            Mah = mah;
            RoozeMah = rooz;

            var pDate = new System.Globalization.PersianCalendar();
            EquivalentGoergianDate = pDate.ToDateTime(saal, mah, rooz, 0, 0, 0, 0);

            Saat = pDate.GetHour(EquivalentGoergianDate);
            Daghighe = pDate.GetMinute(EquivalentGoergianDate);
            Saniyeh = pDate.GetSecond(EquivalentGoergianDate);

            RoozeHafteh = ConvertDate.MapWeekDayToNum(pDate.GetDayOfWeek(EquivalentGoergianDate));
        }


        #region properties

        internal DateTime EquivalentGoergianDate { get; set; }

        internal string RoozeHaftehName
        {
            get
            {
                if (RoozeHafteh >= 0 && RoozeHafteh <= 6)
                    return ConvertDate.MapWeekDayToName(RoozeHafteh);
                return "";
            }
        }
        internal string MahName
        {
            get
            {
                if (Mah > 0 && Mah < 13)
                    return ConvertDate.MapFarsiMonthNumToName(Mah);
                return "";
            }
        }

        internal string TimeSymbol
        {
            get
            {
                return (Saat / 12 == 1 ? "عصر" : "صبح");
            }
        }
        internal string TimeSymbolShort
        {
            get
            {
                return (Saat / 12 == 1 ? "ص" : "ع");
            }
        }

        internal string ShortDate
        {
            get
            {
                var shortSal = Saal.ToString().Remove(0, 2);

                return string.Format("{0}/{1:00}/{2:00}", shortSal, Mah, RoozeMah);

            }
        }
        internal string FullDate
        {
            get
            {

                return string.Format("{0}/{1:00}/{2:00}", Saal, Mah, RoozeMah);

            }
        }
        internal string ShortTime
        {
            get
            {
                return string.Format("{0:00}:{1:00}", Saat, Daghighe);

            }
        }
        internal string LongDate
        {
            get
            {
                return string.Format("{0}, {1:00} {2} {3}", RoozeHaftehName, RoozeMah, MahName, Saal);
            }
        }
        internal string LongTime
        {
            get
            {
                return string.Format("{0:00}:{1:00}:{2:00}", Saat, Daghighe, Saniyeh);

            }
        }

        internal int RoozeHafteh { get; private set; }
        internal int RoozeMah { get; private set; }
        /// <summary>
        /// maheh sal be adad masalan 12
        /// </summary>
        internal int Mah { get; private set; }
        /// <summary>
        /// adade sal masalan 1392
        /// </summary>
        internal int Saal { get; private set; }


        internal int Saat { get; private set; }
        internal int Daghighe { get; private set; }
        internal int Saniyeh { get; private set; }
        #endregion
    }
}
