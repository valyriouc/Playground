using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASimpleStack
{
    public class FixedSizeStack<TEntity> : Stack<TEntity>
    {
        private TEntity[] Entities { get; set; }

        public int Offset { get; private set; } 

        public int Size { get; }

        public override bool IsEmpty => Offset < 0;

        public FixedSizeStack(int size)
        {
            Entities = new TEntity[size];
        
            Offset = -1;
            Size = size;
        }

        public bool TryPush(TEntity entity)
        {
            try
            {
                Push(entity);
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

            return true;
        }

        public override void Push(TEntity entity)
        {  
            if (Entities.Length == Offset)
            {
                throw new IndexOutOfRangeException(
                    "Stack is full");
            }

            Offset += 1;
            Entities[Offset] = entity;
        }

        public bool TryPop(out TEntity entity)
        {
            entity = default;

            try
            {
                entity = Pop();
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

            return true;
        }

        public override TEntity Pop()
        {
            ThrowIfOffsetAlreadyNull();

            return Entities[Offset--];
        }

        public bool TryTop(out TEntity entity)
        {
            entity = default;

            try
            {
                entity = Top();
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

            return true;
        }

        public override TEntity Top()
        {
            ThrowIfOffsetAlreadyNull();

            return Entities[Offset];
        }

        private void ThrowIfOffsetAlreadyNull()
        {
            if (Offset < 0)
            {
                throw new IndexOutOfRangeException(
                    "Stack has no elements");
            }
        }
    }
}
