using VRCFTModuleWrapper.Tests;

namespace VRCFaceTracking.Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VRCFTOSC listener = new VRCFTOSC();
            for (int i = 0; i < 10; i++) 
            {
                Console.WriteLine(Expressions.VRCFTExpressionMap.ToString());
                Thread.Sleep(1000);
            }
        }
    }
}