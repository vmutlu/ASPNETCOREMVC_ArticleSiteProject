using System;

namespace HappyBlog.Shared.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// alınan tarihi _ ekleyip formatlayıp return edecek. Resim yüklerken resim isim adları tarih formatı şeklinde olacak.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FullDateAndTimeStringWithUnderscore(this DateTime dateTime)
        {
            return $"{dateTime.Millisecond}_{dateTime.Second}_{dateTime.Minute}_{dateTime.Hour}_{dateTime.Date.ToShortDateString()}_{dateTime.Month}_{dateTime.Year}";
        }
    }
}
