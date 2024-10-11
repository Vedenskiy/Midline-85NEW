namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.And
{
    public class AndOperatorNode : BinaryBooleanOperator
    {
        public AndOperatorNode(INode left, INode right) 
            : base(left, right) { }

        protected override bool Evaluate(int left, int right) => 
            left > 0 && right > 0;
    }
}