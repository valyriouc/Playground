using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASimpleStack;
using Xunit;
using Xunit.Sdk;

namespace StackTests
{
    public class FixedSizeStackTests
    {
        [Fact]
        public void IsEmptyShouldReturnTrueForEmptyStack()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(4);

            Assert.True(stack.IsEmpty);
        }

        [Fact]
        public void IsEmptyShouldReturnFalseForStackWithEntry()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(4);

            stack.Push(1);

            Assert.False(stack.IsEmpty);
        }

        [Fact]
        public void TryPushReturnsTrueWhenAbleToAddEntryAndAdsEntry()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(2);

            Assert.True(stack.TryPush(1));
            Assert.Equal(1, stack.Pop());
        }

        [Fact]
        public void TryPushReturnsFalseWhenNotAbleToAddEntry()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(2);

            stack.Push(1);
            stack.Push(2);

            Assert.False(stack.TryPush(1));
        }

        [Fact]
        public void PushAddsEntryToTheStack()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(2);

            stack.Push(1);
            Assert.Equal(1, stack.Pop());
        }

        [Fact]
        public void PushThrowsExceptionWhenNotAbleToAdd()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(2);

            stack.Push(1);
            stack.Push(2);
            Assert.Throws<IndexOutOfRangeException>(() => stack.Push(3));
        }

        [Fact]
        public void TryPopReturnsTrueWhenAbleToPopAndReturnsTheCorrectEntry()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(2);

            stack.Push(1);
            Assert.True(stack.TryPop(out int entry));
            Assert.Equal(1, entry);
        }

        [Fact]
        public void TryPopReturnsFalseWhenNotAbleToPop()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(2);

            Assert.False(stack.TryPop(out int entity));
        }

        [Fact]
        public void PopThrowsExceptionIfNotAbleToPop()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(2);

            Assert.Throws<IndexOutOfRangeException>(() => stack.Pop());
        }

        [Fact]
        public void PopReturnsCorrectEntity()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(2);

            stack.Push(1);
            Assert.Equal(1, stack.Pop());   
        }

        [Fact]
        public void TryTopReturnsTrueAndAppropriateEntry()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(2);

            stack.Push(1);

            Assert.True(stack.TryTop(out int entry));
            Assert.Equal(1, entry);
        }

        [Fact]
        public void TryTopReturnsFalse()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(2);

            Assert.False(stack.TryTop(out int entry));
        }

        [Fact]
        public void TopReturnsCorrectEntry()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(2);

            stack.Push(1);

            Assert.Equal(1, stack.Top());
        }

        [Fact]
        public void TopThrowsExceptionWhenNotAbleToGetTopEntry()
        {
            FixedSizeStack<int> stack = new FixedSizeStack<int>(2);

            Assert.Throws<IndexOutOfRangeException>(() => stack.Top());
        }
    }
}
