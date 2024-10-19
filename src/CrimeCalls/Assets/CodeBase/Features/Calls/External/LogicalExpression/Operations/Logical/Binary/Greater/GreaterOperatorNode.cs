namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.Greater
{
    public class GreaterOperatorNode : BinaryBooleanOperator
    {
        public GreaterOperatorNode(INode left, INode right) 
            : base(left, right) { }

        protected override bool Evaluate(int left, int right) => 
            left > right;
    }
}