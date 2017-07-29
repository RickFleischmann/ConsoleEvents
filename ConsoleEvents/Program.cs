using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEventTutorial
{
    public delegate int dgPointer(int a, int b);

    class Program
    {
        static void Main()
        {
            Adder a = new Adder();
            
            // Subscribe to event
            a.OnMultipleOfFiveReached += a_MultipleOfFiveReached;

            dgPointer pAdder = new dgPointer(a.Add);
            int iAnswer = pAdder(4, 3);
            Console.WriteLine("iAnswer = {0}", iAnswer);
            iAnswer = pAdder(4, 6);
            Console.WriteLine("iAnswer = {0}", iAnswer);
            Console.ReadKey();
        }

        static void a_MultipleOfFiveReached(object sender, MultipleOfFiveEventArgs e)
        {
            Console.WriteLine("Multiple of five reached: {0}", e.Total);
        }
    }

    public class Adder
    {

        //use the EventHandler delegate. When we raise the event, we must follow along with 
        //the delegate definition and pass in the required parameter values. 
        //Note how we pass our current instance of adder for the first parameter (sender) 
        //and since we are not passing back any event arguments, we use EventArgs. Empty for “e”.

        public event EventHandler<MultipleOfFiveEventArgs> OnMultipleOfFiveReached;
        public int Add(int x, int y)
        {
            int iSum = x + y;
            
            if ((iSum % 5 == 0) && (OnMultipleOfFiveReached != null))
            {
                Console.WriteLine("x,y,iSum: {0},{1}, {2}", x.ToString(), y.ToString(), iSum.ToString());
                OnMultipleOfFiveReached(this, new MultipleOfFiveEventArgs(iSum));
            }
            return iSum;
        }
    }

    //In order to use the other delegate and pass the grand total back to the event hander method, 
    // we first need to define a custom class called MultipleOfFiveEventArgs for passing 
    // back a custom value, such as Total.It must inherit from the EventArgs class.
    public class MultipleOfFiveEventArgs : EventArgs
    {
        public MultipleOfFiveEventArgs(int iTotal)
        { Total = iTotal; }
        public int Total { get; set; }
    }

}
