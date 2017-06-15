namespace RecentlyUsedList.Core
{
    using System;
    using System.Collections.Generic;

    public class RecentlyUsedList : IUsedList
    {
        public RecentlyUsedList(int upperLimit = 5)
        {
            this.List = new Stack<string>();
            this.UpperLimit = upperLimit;
        }

        public Stack<string> List { get; }

        public int UpperLimit { get; }

        public int Count()
        {
            return this.List.Count;
        }

        public void Add(string item)
        {
            GuardAgainsNullArgumentException(item);

            if (this.UpperLimitReached())
            {
                return;
            }

            if (this.List.Contains(item))
            {
                this.MoveDuplicatedItem(item);
            }
            else
            {
                this.List.Push(item);
            }
        }

        public string Remove()
        {
            return this.List.Pop();
        }

        public string Get(int index)
        {
            if (index < 0 || index > this.UpperLimit)
            {
                throw new IndexOutOfRangeException();
            }

            var array = this.List.ToArray();

            return array[index];
        }

        private bool UpperLimitReached()
        {
            if (this.Count() == this.UpperLimit)
            {
                return true;
            }
            return false;
        }

        private static void GuardAgainsNullArgumentException(string item)
        {
            if (item.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException();
            }
        }

        private void MoveDuplicatedItem(string item)
        {
            var tempStack = new Stack<string>();
            var secondList = this.List;
            while (secondList.Count > 0)
            {
                var tmpItem = secondList.Pop();
                if (!tmpItem.Equals(item))
                {
                    tempStack.Push(tmpItem);
                }
                else
                {
                    this.ReorganizeList(item, tempStack);
                    break; // should not be needed
                }
            }
        }

        private void ReorganizeList(string item, Stack<string> tempStack)
        {
            while (tempStack.Count > 0)
            {
                var tmpItemTempStack = tempStack.Pop();
                if (tmpItemTempStack.Equals(item))
                {
                    continue;
                }
                this.List.Push(tmpItemTempStack);
            }

            this.List.Push(item);
        }
    }
}