using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsComm
{
    /// <summary>
    /// Some implementation regarding Threads, Task, async-await
    /// </summary>
    public class MultiThreading_CLS
    {
        static string SomeBlockingMethod()
        {
            Thread.Sleep(3000);
            //_Parameter = "Some value";
            return "Some value";
        }

        /// <summary>
        /// A Task is essentially a promise that a certain operation (the task's code) will complete in the future. 
        /// <para>The Task object provides methods and properties to monitor the progress, completion, or failure of that operation.</para>
        /// <para>A Task represents an asynchronous operation, meaning that the work is often done in the background 
        /// (on a separate thread or using asynchronous I/O) while allowing the main thread to continue running.</para>
        /// </summary>
        public static void Using_TaskRun()
        {
            string sLocalVar = null;
            // Deferred Invocation/Execution
            // The method SomeMethod is simply passed as a delegate and will be invoked later
            // on a thread pool thread when the task is actually executed.
            // Task.Run(SomeBlockingMethod);

            // Immediate Invocation/Execution
            // Use if method is parameterised
            // The method SomeMethod() is invoked immediately when the task is started (in the lambda).
            Task.Run(() => sLocalVar = SomeBlockingMethod());
            Debug.WriteLine("Task initiated...");
            while (string.IsNullOrEmpty(sLocalVar))
            {
                // wait in while loop until value is updated.
            }
            Debug.WriteLine("Value updated...");
            // Both expressions run the method on separate thread
        }

        public static void Using_Thread()
        {
            string sLocalVar = null;
            Thread th = new Thread(() => sLocalVar = SomeBlockingMethod());
            th.Start();
            while (string.IsNullOrEmpty(sLocalVar))
            {

            }
        }
    }
}
