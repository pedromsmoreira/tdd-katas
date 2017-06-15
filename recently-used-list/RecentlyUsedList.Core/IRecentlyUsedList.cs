namespace RecentlyUsedList.Core
{
    public interface IRecentlyUsedList
    {
        void Add(string item);

        string Remove();

        string Get(int index);

        int Count();
    }
}