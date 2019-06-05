using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronous_breakfast
{
    class Toast { };

    class Egg { };
    class Bacon { };
    class Coffee { };
    class Program
    {
        public static async Task Main(string[] args)
        {
            // Making Toast, Applying Butter, Applying Jam
            async Task<Toast> ToastBreadAsync(int number)
            {
                Toast plaintoast = new Toast();
                Console.WriteLine("Begin Making Toast! 1/3");
                await Task.Delay(number);
                Console.WriteLine("Finished Making Toast! 1/3");
                return plaintoast;
            }

            async Task<Toast> ToastBreadButterAsync(int number, Toast plaintoast)
            {
                Toast buttertoast = plaintoast;
                Console.WriteLine("Begin applying butter to Toast! 2/3");
                await Task.Delay(number);
                Console.WriteLine("Finished applying butter to Toast! 2/3");
                return buttertoast;
            }

            async Task<Toast> ToastBreadJamAsync(int number, Toast buttertoast)
            {
                Toast jambuttertoast = buttertoast;
                Console.WriteLine("Begin applying jam to Toast! 3/3");
                await Task.Delay(number);
                Console.WriteLine("Finished applying jam to Toast! 3/3");
                return jambuttertoast;
            }

            async Task<Toast> makeToastWithButterAndJamAsync(int toastnumber, int butternumber, int jamnumber)
            {
                var plaintoast = await ToastBreadAsync(toastnumber);
                var buttertoast = await ToastBreadButterAsync(butternumber, plaintoast);
                var jambuttertoast = await ToastBreadJamAsync(jamnumber, buttertoast);
                return jambuttertoast;
            }

            //Coffee task

            async Task<Coffee> pourCoffeeAsync(int number)
            {
                Coffee coffee = new Coffee();
                Console.WriteLine("Begin Making Coffee!");
                await Task.Delay(number);
                Console.WriteLine("Finished Making Coffee!");
                return coffee;
            }

            // Egg task 

            async Task<Egg> fryEggsAsync(int number)
            {
                Egg egg = new Egg();
                Console.WriteLine("Begin Frying Egg!");
                await Task.Delay(number);
                Console.WriteLine("Finished Frying Egg!");
                return egg;
            }

            //Bacon Task

            async Task<Bacon> fryBaconAsync(int number)
            {
                Bacon bacon = new Bacon();
                Console.WriteLine("Begin Frying Bacon!");
                await Task.Delay(number);
                Console.WriteLine("Finished Frying Bacon!");
                return bacon;
            }

            // List tasks
            var eggsTask = fryEggsAsync(3000);
            var baconTask = fryBaconAsync(1000);
            var coffeeTask = pourCoffeeAsync(7000);
            var toastTask = makeToastWithButterAndJamAsync(5000, 5000, 5000);
            var allTasks = new List<Task> { coffeeTask, eggsTask, baconTask, toastTask };
            
            while (allTasks.Any())
            {
               // WhenAny returns a Task<Task> that completes when any of its arguments completes
               Task finished = await Task.WhenAny(allTasks);
                if (finished == coffeeTask)
                {
                    Console.WriteLine("Coffee is ready");
                    allTasks.Remove(coffeeTask);
                }
                else if (finished == eggsTask)
                {
                    Console.WriteLine("Eggs are ready");
                    allTasks.Remove(eggsTask);
                }
                else if (finished == baconTask)
                {
                    Console.WriteLine("Bacon is ready");
                    allTasks.Remove(baconTask);
                }
                else if (finished == toastTask)
                {
                    Console.WriteLine("Toast is ready");
                    allTasks.Remove(toastTask);
                }
                else
                    allTasks.Remove(finished);
            }
            Console.WriteLine("Breakfast is ready!");
        }
    }
}
