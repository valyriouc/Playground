using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASimpleStack
{
    public class DynamicSizeStack<TEntity> : Stack<TEntity>
    {
        private int Offset { get; set; }

        private List<TEntity> Buffer { get; set; }

        public int Size => Buffer.Count;

        public int Capacity => Buffer.Capacity;

        public override bool IsEmpty => Offset == 0;

        public DynamicSizeStack()
        {
            Offset = 0;
            Buffer = new List<TEntity>();
        }

        public override void Push(TEntity entity)
        {
            if (Buffer is null)
            {
                throw new Exception(
                    "Stack not initialized!");
            }

            Offset += 1;
            Buffer.Add(entity);
        }

        public override TEntity Pop() =>
            GetInternal();

        public override TEntity Top() => 
            GetInternal();

        private TEntity GetInternal()
        {
            if (Offset == 0)
            {
                throw new IndexOutOfRangeException(
                    "Stack is empty");
            }

            Offset -= 1;
            TEntity entity = Buffer[Offset];
            Buffer.Remove(entity);
            Buffer.Capacity -= 1;
            return entity;
        }
    }
}
