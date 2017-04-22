namespace Wielomiany
{
    class Polynomial
    {
        private readonly Node head;
        private Node tail;
        public int Length { get; private set; }

        public Polynomial()
        {
            head = tail = new Node(0, -1);
            tail.Next = head;
            Length = 0;
        }

        public Polynomial AddComponent(int factor, int exponent)
        {
            var newExpresion = new Node(factor, exponent);
            tail.Next = newExpresion;
            tail = tail.Next;
            tail.Next = head;

            Length++;
            return this;
        }

        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
        {
            var addedPolynomial = AddingPolynomials(lhs, rhs);
            return AddNodes(addedPolynomial);
        }

        public static Polynomial operator -(Polynomial lhs, Polynomial rhs)
        {
            var addedPolynomial = AddingPolynomials(lhs, rhs, true);
            return AddNodes(addedPolynomial);
        }

        public static Polynomial operator *(Polynomial lhs, Polynomial rhs)
        {
            var multipliedPolynomial = MultiplyPolynomials(lhs, rhs);
            return SimplifyPolynomial(multipliedPolynomial);
        }


        #region Helpers
        public static Polynomial MultiplyPolynomials(Polynomial lhs, Polynomial rhs)
        {
            var multipliedPolynomial = new Polynomial();

            for (int i = 1; i <= lhs.Length; i++)
            {
                var firstNodeToMultiply = lhs[i];
                for (int j = 1; j <= rhs.Length; j++)
                {
                    var secondNodeToMultiply = rhs[j];
                    var factor = firstNodeToMultiply.Factor * secondNodeToMultiply.Factor;
                    var exponent = firstNodeToMultiply.Exponent + secondNodeToMultiply.Exponent;
                    multipliedPolynomial.AddComponent(factor, exponent);
                }
            }
            return multipliedPolynomial;
        }

        public static Polynomial SimplifyPolynomial(Polynomial multipliedPolynomial)
        {
            return AddNodes(multipliedPolynomial);
        }

        public static Polynomial AddNodes(Polynomial joinedPolynomial)
        {
            var result = new Polynomial();
            var readedNodes = new bool[joinedPolynomial.Length];
            for (int i = 1; i <= joinedPolynomial.Length; i++)
            {
                var firstNodeToAdd = joinedPolynomial[i];
                if (firstNodeToAdd != null && firstNodeToAdd.Exponent != -1 && readedNodes[i - 1] == false)
                {
                    for (int j = i + 1; j <= joinedPolynomial.Length; j++)
                    {
                        var secondNodeToAdd = joinedPolynomial[j];
                        if (secondNodeToAdd != null && readedNodes[j - 1] == false && secondNodeToAdd.Exponent != -1 && secondNodeToAdd.Exponent == firstNodeToAdd.Exponent)
                        {
                            if (firstNodeToAdd.Factor + secondNodeToAdd.Factor != 0)
                            {
                                result.AddComponent(firstNodeToAdd.Factor + secondNodeToAdd.Factor, firstNodeToAdd.Exponent);
                            }

                            readedNodes[j - 1] = true;
                            readedNodes[i - 1] = true;
                        }
                    }
                }
            }

            for (int i = 1; i <= joinedPolynomial.Length; i++)
            {
                if (readedNodes[i - 1] == false)
                {
                    var nodeToAdd = joinedPolynomial[i];
                    result.AddComponent(nodeToAdd.Factor, nodeToAdd.Exponent);
                }
            }

            return result;
        }
        public Node this[int i]
        {
            get
            {
                Node tempNode = this.head;
                for (int j = 0; j <= this.Length; j++)
                {
                    if (j == i)
                    {
                        return tempNode;
                    }
                    tempNode = tempNode.Next;
                }
                return null;
            }
        }
        public static Polynomial AddingPolynomials(Polynomial lhs, Polynomial rhs, bool isSubtracted = false)
        {
            Polynomial result = new Polynomial();

            for (int i = 1; i <= lhs.Length; i++)
            {
                var tempNode = lhs[i];
                result.AddComponent(tempNode.Factor, tempNode.Exponent);
            }

            for (int i = 1; i <= rhs.Length; i++)
            {
                var tempNode = rhs[i];
                result.AddComponent(isSubtracted ? -tempNode.Factor : tempNode.Factor, tempNode.Exponent);
            }
            return result;
        }

        public static Polynomial SortPolynomial(Polynomial inputPolynomial)
        {
            Polynomial result = new Polynomial();
            var readedNodes = new bool[inputPolynomial.Length];
            for (int i = 1; i <= inputPolynomial.Length; i++)
            {
                if (readedNodes[i - 1] == false)
                {
                    var max = inputPolynomial[i];
                    var index = i;
                    if (i != inputPolynomial.Length)
                    {
                        for (int j = i + 1; j <= inputPolynomial.Length; j++)
                        {
                            if (readedNodes[j - 1] == false && inputPolynomial[j].Exponent > max.Exponent)
                            {
                                max = inputPolynomial[j];
                                index = j;
                            }
                        }
                    }

                    result.AddComponent(max.Factor, max.Exponent);
                    readedNodes[index - 1] = true;
                    if (result.Length == inputPolynomial.Length)
                    {
                        break;
                    }
                    i = 0;
                }
            }
            return result;
        }
        public override string ToString()
        {
            if (this.Length == 0)
            {
                return "0";
            }

            var nodes = SortPolynomial(this);
            var result = "";
            for (int i = 1; i <= this.Length; i++)
            {
                var node = nodes[i];

                var isFirst = i == 1;
                var isPositive = node.Factor > 0;
                var useMinusMark = node.Factor < 0;
                var usePlusMark = !isFirst && !useMinusMark;
                var useX = node.Exponent > 0;
                var useExponent = node.Exponent > 1;

                result += usePlusMark ? " + " : "";
                result += useMinusMark && isFirst ? "-" : "";
                result += useMinusMark && !isFirst ? " - " : "";
                result += isPositive? node.Factor.ToString() : (-node.Factor).ToString();
                result += useX ? "x" : "";
                result += useExponent ? "^" +node.Exponent.ToString() : "";
            }
            return result;
        }
        #endregion
    }
}
