namespace Core.Tests
{
    using System;
    using ClassFixtures;
    using FluentAssertions;
    using RecentlyUsedList.Core;
    using Xunit;

    public class RecentlyUsedListTests
    {
        /*
         Recently Used List

            Develop a recently-used-list class to hold strings uniquely in Last-In-First-Out order.

            The most recently added item is first, the least recently added item is last. (done)

            Items can be looked up by index, which counts from zero. (done)

            Items in the list are unique, so duplicate insertions are moved rather than added. (done)

            A recently-used-list is initially empty. (done)

            Optional extras:

            Null insertions (empty strings) are not allowed. (done)

            A bounded capacity can be specified, so there is an upper limit to the number of items contained, with the least recently added items dropped on overflow.

            More tests:

            While getting items by index, supplied index-value should be within the bounds of List [eg. if maximum item counts of list is 5 then supplied index is less than 4 as index starts from 0 (zero)] (done)
            Negative index value not allowed [>0] (done)
            Size limit is must if not supplied make 5 as default [0-4] (done)
            List can be non-sizable means without upper limit list can be created [Hint-try property or constructor initializers] (done)
         */

        [Fact]
        public void RecentlyUsedList_IsCreated()
        {
            // Arrange
            var sut = new RecentlyUsedListStackFixture();

            // Act
            var list = sut.Create();

            // Assert
            list.Should().BeOfType<RecentlyUsedListStack>();
        }

        [Fact]
        public void RecentlyUsedList_ListCount_ShouldBe0()
        {
            // Arrange
            var sut = new RecentlyUsedListStackFixture();

            // Act
            var list = sut.Create();

            // Assert
            list.Count().Should().Be(0);
        }

        [Fact]
        public void AddItem_AddedOneItemToList_CountShouldBe1()
        {
            // Arrange
            var list = new RecentlyUsedListStack();

            // Act
            list.Add("first item");

            // Assert
            list.Count().Should().Be(1);
        }

        [Fact]
        public void Remove_LastItemAdded_ShouldBeFirstItemWhenRemovingFromList()
        {
            // Arrange
            var sut = new RecentlyUsedListStackFixture();
            sut.AddManyToList();
            var list = sut.Create();

            // Act
            var item = list.Remove();

            // Assert
            item.Should().Be("second item");
            list.Count().Should().Be(1);
        }

        [Fact]
        public void RecentlyUsedList_DoesNotAddDuplicateItems_CountShouldBe2()
        {
            // Arrange
            var sut = new RecentlyUsedListStackFixture();
            sut.AddManyToList();
            var list = sut.Create();

            // Act
            list.Add("first item");

            // Assert
            list.Count().Should().Be(2);
        }

        [Fact]
        public void RecentlyUsedList_DoesNotAddDuplicateItems_FirstitemOutShouldBeNamedSecondItem()
        {
            // Arrange
            var sut = new RecentlyUsedListStackFixture();
            sut.AddManyToList();
            var list = sut.Create();

            // Act

            list.Add("third item");
            list.Add("first item");
            list.Add("fourth item");
            list.Add("second item");

            // Assert
            list.Count().Should().Be(4);
            list.Get(0).Should().Be("second item");
        }

        [Fact]
        public void GetItemByIndex_NegativeIndex_ShouldThrowIndexOutOfRangeException()
        {
            // Arrange
            var sut = new RecentlyUsedListStackFixture();
            sut.AddManyToList();
            var list = sut.Create();

            // Act
            Action act = () =>
            {
                var result = list.Get(-1);
            };

            // Assert
            act.ShouldThrow<IndexOutOfRangeException>();
        }

        [Theory]
        [InlineData(1, "first item")]
        public void GetItemByIndex_IndexArgument_ShouldReturnSecondItemInList(int index, string expected)
        {
            // Arrange
            //var sut = new RecentlyUsedListStackFixture();
            //sut.AddManyToList();
            //var list = sut.Create();

            var list = new RecentlyUsedListStack();
            list.Add("first item");
            list.Add("second item");

            // Act
            var act = list.Get(index);

            // Assert
            act.Should().Be(expected);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void Add_EmptyNullAndWhiteSpaceArgument_ShouldThrowArgumentNullException(string argument)
        {
            // Arrange
            var sut = new RecentlyUsedListStackFixture();
            var list = sut.Create();

            // Act
            Action act = () =>
            {
                list.Add(string.Empty);
            };

            // Assert
            act.ShouldThrow<ArgumentNullException>();
        }

        //A bounded capacity can be specified, so there is an upper limit to the number of items contained, with the least recently added items dropped on overflow.
        [Fact]
        public void CreateList_WithUpperLimit_ShouldBeFiveByDefault()
        {
            // Act
            var sut = new RecentlyUsedListStackFixture();
            var list = sut.Create();

            // Assert
            list.UpperLimit.Should().Be(5);
        }

        [Theory]
        [InlineData(10, 10)]
        public void CreateList_SetUpperLimitTo10_ShouldBe10(int upperLimit, int expected)
        {
            // Act
            var sut = new RecentlyUsedListStackFixture(upperLimit);
            var list = sut.Create();

            // Assert
            list.UpperLimit.Should().Be(expected);
        }

        [Fact]
        public void Add_ItemAboveDefinedUpperLimitOf5_ShouldStay5()
        {
            // Arrange
            var sut = new RecentlyUsedListStackFixture();
            var list = sut.Create();

            // Act
            for (int i = 1; i <= 6; i++)
            {
                list.Add(i.ToString());
            }

            // Assert
            list.Count().Should().Be(5);
        }

        [Fact]
        public void Get_IndexAboveUpperLimit_ShouldThrowIndexOutOfRangeException()
        {
            // Arrange
            var sut = new RecentlyUsedListStackFixture();
            sut.AddManyToList();
            var list = sut.Create();

            // Act
            Action act = () =>
            {
                list.Get(6);
            };

            // Assert
            act.ShouldThrow<IndexOutOfRangeException>();
        }

        [Fact]
        public void Get_IndexDoesNotHaveValue_ShouldThrowIndexOutOfRangeException()
        {
            // Arrange
            var sut = new RecentlyUsedListStackFixture();
            sut.AddManyToList();
            var list = sut.Create();

            // Act
            Action act = () =>
            {
                list.Get(3);
            };

            // Assert
            act.ShouldThrow<IndexOutOfRangeException>();
        }
    }
}