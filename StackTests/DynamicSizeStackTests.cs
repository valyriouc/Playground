using ASimpleStack;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace StackTests
{
    public class DynamicSizeStackTests
    {
        [Fact]
        public void PushAddsEntityToStack()
        {
            DynamicSizeStack<int> stack = new DynamicSizeStack<int>();

            stack.Push(1);

            Assert.Equal(1, stack.Pop());
        }

        [Fact]
        public void PopThrowsExceptionWhenStackSizeIsZero() 
        {
            DynamicSizeStack<int> stack = new DynamicSizeStack<int>();

            Assert.Throws<IndexOutOfRangeException>(() => stack.Pop());
        }

        [Fact]
        public void PopReturnsCorrectEntryAndDecrementListSize()
        {
            DynamicSizeStack<int> stack = new DynamicSizeStack<int>();

            stack.Push(1);

            Assert.Equal(1, stack.Size);
            Assert.Equal(1, stack.Capacity);

            int item = stack.Pop();
            Assert.Equal(1, item);
            Assert.Equal(0, stack.Size);
            Assert.Equal(0, stack.Capacity);
        }

        [Fact]
        public void TopThrowsExceptionWhenStackSizeIsZero()
        {
            DynamicSizeStack<int> stack = new DynamicSizeStack<int>();

            Assert.Throws<IndexOutOfRangeException>(() => stack.Top());
        }
    }
}
