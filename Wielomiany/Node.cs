namespace Wielomiany
{
    class Node
    {
        public int Factor { get; set; }
        public int Exponent { get; set; }
        public Node Next { get; set; }
        public Node(int factor, int exponent)
        {
            Factor = factor;
            Exponent = exponent;
        }
    }
}