namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Unary.Not
{
    public class NotOperatorNode : UnaryOperatorNode
    {
        public NotOperatorNode(INode node) 
            : base(node) { }

        protected override bool Evaluate(int value) => 
            value <= 0;
    }
}