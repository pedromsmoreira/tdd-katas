namespace RecentlyUsedList.Core
{
    public interface IUsedList
    {
        void Add(string item);

        string Remove();

        string Get(int index);

        int Count();
    }
}