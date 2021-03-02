using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerAndConsumer
{
    public class BufferQueue<T> : Queue<T>
    {
        //Set a limit om the queue
        public int BufferSize { get; set; }

        public BufferQueue(int maxsize) : base(maxsize)
        {
            BufferSize = maxsize;
        }

        //make a new Enqueue Method 
        public new void Enqueue(T item)
        {
            // if count smaller then maxsize then enqueue else do nothing
            if (this.Count < BufferSize)
            {
                base.Enqueue(item);
            }
            
        }

        public bool IsLimitReached()
        {
            return this.Count >= this.BufferSize ? true : false;
        }
    }
}
