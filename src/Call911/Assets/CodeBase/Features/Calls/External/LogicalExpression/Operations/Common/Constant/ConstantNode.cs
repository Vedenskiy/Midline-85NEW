namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Common.Constant
{
    public class ConstantNode : INode
    {
        private readonly int _constant;

        public ConstantNode(int constant) => 
            _constant = constant;

        public int Evaluate(IVariables variables) => 
            _constant;
    }
}