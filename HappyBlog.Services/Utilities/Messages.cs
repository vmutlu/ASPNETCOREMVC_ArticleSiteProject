namespace HappyBlog.Services.Utilities
{
    public static class Messages
    {
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir kategori bulunamadı.";

                return "Böyle bir kategori bulunamadı";
            }

            public static string Add(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla eklendi.";
            }

            public static string Update(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla güncellenmiştir.";
            }

            public static string Delete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla silinmiştir.";
            }

            public static string HardDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla veri tabanından silindi..";
            }
        }

        public static class Article
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir makale bulunamadı.";

                return "Böyle bir makale bulunamadı";
            }

            public static string Add(string articleName)
            {
                return $"{articleName} adlı makale başarıyla eklendi.";
            }

            public static string Update(string articleName)
            {
                return $"{articleName} adlı makale başarıyla güncellenmiştir.";
            }

            public static string Delete(string articleName)
            {
                return $"{articleName} adlı makale başarıyla silinmiştir.";
            }

            public static string HardDelete(string articleName)
            {
                return $"{articleName} adlı makale başarıyla veri tabanından silindi..";
            }
        }
    }
}
