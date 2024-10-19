namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.Or
{
    public class OrOperatorNode : BinaryBooleanOperator
    {
        public OrOperatorNode(INode left, INode right) 
            : base(left, right) { }

        protected override bool Evaluate(int left, int right) => 
            left > 0 || right > 0;
    }
}