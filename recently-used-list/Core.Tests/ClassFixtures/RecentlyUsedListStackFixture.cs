namespace Core.Tests.ClassFixtures
{
    using RecentlyUsedList.Core;

    internal class RecentlyUsedListStackFixture
    {
        public RecentlyUsedListStackFixture(int upperLimit = 5)
        {
            this.Fake = new RecentlyUsedListStack(upperLimit);
        }

        internal IRecentlyUsedList Fake { get; }

        internal void AddManyToList()
        {
            this.Fake.Add("first item");
            this.Fake.Add("second item");
        }

        internal RecentlyUsedListStack Create()
        {
            return this.Fake as RecentlyUsedListStack;
        }
    }
}