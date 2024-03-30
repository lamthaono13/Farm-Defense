// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("qew+6XNNsaV9PWO7TrjdnQdPMeS1FrXa3E8O3dpmbeTaSIzWGf2SPfT9tCneffm/ZH8QGYsCwPps3IAMv+zSYK2e3xRUzlR7RpoASVfWzJr1eiWc7jl+5n01ZQTGFiXNRss4hMtIRkl5y0hDS8tISEnY0lEN0RIOw2fKQ1vAIkJzo03rc94ME18+nQomU55e0PWQOyg/TJ1xRq9hBgJDhO5WuDz32GiWzhAtlM4eUN4TxnT7abh4xAuk466bZIUkd9c1SVTW7XF5y0hreURPQGPPAc++REhISExJSgKL3aCDbhFJKHb+IHpl9cx27lWN6CT8dZVGYOieF0S+pL7/nEc/5wMioDbn16enbVxt+Nsoqryvs/CThvxhxASyyZLqxEtKSElI");
        private static int[] order = new int[] { 8,12,3,4,11,12,6,11,13,11,13,11,13,13,14 };
        private static int key = 73;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
