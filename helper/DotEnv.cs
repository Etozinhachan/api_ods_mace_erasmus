using System;
using System.IO;
namespace api_ods_mace_erasmus.helper
{
    public static class DotEnv
    {
        public static void Load(string filePath)
        {

            Console.WriteLine("meow");

            

            if (!File.Exists(filePath))
            {
                Console.WriteLine("sad meow :c");
                return;
            }


            Console.WriteLine("Happy Meow");

            foreach (var line in File.ReadAllLines(filePath))
            {
                Console.WriteLine("AAAAAAAAAAA      " + line);
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}