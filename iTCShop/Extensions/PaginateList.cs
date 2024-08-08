namespace iTCShop.Extensions
{
    public static class PaginateList
    {
        public static (List<T> List, int TotalPages) Paginate<T>(List<T> list, int page, int size)  where T : class
        {
            var totalItems = list.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / size);
            var items = list.Skip((page - 1) * size).Take(size).ToList();
            //list.AddRange(items);
            return (items, totalPages);
        }
    }
}
