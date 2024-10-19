namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Arithmetic.Subtract
{
    public class SubtractOperatorNode : INode
    {
        private readonly INode _left;
        private readonly INode _right;

        public SubtractOperatorNode(INode left, INode right)
        {
            _left = left;
            _right = right;
        }
        public int Evaluate(IVariables variables) => 
            _left.Evaluate(variables) - _right.Evaluate(variables);
    }
}