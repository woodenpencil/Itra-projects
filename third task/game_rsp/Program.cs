using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace game_rsp
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = args.Length;
            if (n % 2 == 0  || n<3)
            {
                Console.WriteLine("Please input odd number of moves greater than 2.");
                Console.WriteLine("For example: rock scissors paper");
                return;
            }
            for(int i = 0; i < n-1; i++)
            {
                for(int j = i + 1; j < n; j++)
                {
                    if (args[i] == args[j])
                    {
                        Console.WriteLine("Some names of moves match up. Please write correct names.");
                        return;
                    }
                }
            }
            var rnd_key = RandomNumberGenerator.Create();
            byte[] key = new byte[16];
            rnd_key.GetBytes(key);
            HMACSHA256 hmac_gen = new HMACSHA256(key);
            var rand = new Random();
            
            int comp_move = rand.Next(n);
            var hmac = hmac_gen.ComputeHash(BitConverter.GetBytes(comp_move));
            Console.Write("HMAC ");
            Console.WriteLine(BitConverter.ToString(hmac).Replace("-", ""));
            int player_move;
            do
            {
                Console.WriteLine("Available moves:");
                for(int i = 0; i < n; i++)
                {
                    Console.WriteLine("{0} - {1}", i + 1, args[i]);
                }
                Console.WriteLine("0 - exit");
                Console.Write("Your move is: ");
                string raw_ans = Console.ReadLine();
                try
                {
                    player_move = Int32.Parse(raw_ans);
                }
                catch
                {
                    player_move = n + 1;
                }
                if(player_move>n)
                    Console.WriteLine("Please input number from 0 to {0}", n);
            } while (player_move>n);
            if (player_move == 0)
                return;
            player_move -= 1;
            Console.WriteLine("Your move:{0}", args[player_move]);
            Console.WriteLine("Computer move:{0}", args[comp_move]);
            if (comp_move == player_move)
            {
                Console.WriteLine("Draw!");
                return;
            }else if (comp_move > player_move)
            {
                if (comp_move - player_move <= n / 2)
                    Console.WriteLine("You win!");
                else
                    Console.WriteLine("You lose!");
            }
            else
            {
                if (player_move-comp_move <= n / 2)
                    Console.WriteLine("You lose!");
                else
                    Console.WriteLine("You win!");
            }
            Console.Write("HMAC key ");
            Console.WriteLine(BitConverter.ToString(key).Replace("-", ""));
        }
    }
}
