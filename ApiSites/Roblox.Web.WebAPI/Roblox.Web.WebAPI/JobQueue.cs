// This is temporary until we can find a better solution using some sort of external program

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roblox.Web.WebAPI
{
    public static class JobQueue
    {
        static JobQueue()
        {
            
        }

        /// <summary>
        /// Schedule a job to be ran as soon as possible
        /// </summary>
        /// <param name="category">A category (for debugging)</param>
        /// <param name="cb">The function to call</param>
        public static void Schedule(string category, Func<Task> cb)
        {
            var id = Guid.NewGuid();
            Task.Run(async () =>
            {
                try
                {
                    await cb();
                }
                catch (Exception e)
                {
                    Console.WriteLine("[error] JobQueue background task (ID = {0}, Name = {1} failed: {2}\n{3}", id, category, e.Message, e.StackTrace);
                }
            });
        }
    }
}