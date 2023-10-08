using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASimpleStack
{
    public abstract class Stack<TEntity>
    {
        /// <summary>
        /// Property which indicates if the stack is empty 
        /// </summary>
        public abstract bool IsEmpty { get; }

        /// <summary>
        /// Tries to add a new entity to the stack 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract void Push(TEntity entity);

        /// <summary>
        /// Takes the current element from the stack 
        /// </summary>
        /// <returns></returns>
        public abstract TEntity Pop();
        
        /// <summary>
        /// Takes the top most element from the stack 
        /// </summary>
        /// <returns></returns>
        public abstract TEntity Top();

    }
}
