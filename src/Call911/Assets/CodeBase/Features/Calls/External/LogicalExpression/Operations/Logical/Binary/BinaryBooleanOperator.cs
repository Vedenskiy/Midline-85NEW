namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary
{
    public abstract class BinaryBooleanOperator : INode
    {
        private readonly INode _left;
        private readonly INode _right;

        protected BinaryBooleanOperator(INode left, INode right)
        {
            _left = left;
            _right = right;
        }

        public int Evaluate(IVariables variables) => 
            Evaluate(_left.Evaluate(variables), _right.Evaluate(variables)) ? 1 : 0;

        protected abstract bool Evaluate(int left, int right);
    }
}