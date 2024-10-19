namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Unary
{
    public abstract class UnaryOperatorNode : INode
    {
        private readonly INode _node;

        protected UnaryOperatorNode(INode node) => 
            _node = node;

        public int Evaluate(IVariables variables) => 
            Evaluate(_node.Evaluate(variables)) ? 1 : 0;

        protected abstract bool Evaluate(int value);
    }
}