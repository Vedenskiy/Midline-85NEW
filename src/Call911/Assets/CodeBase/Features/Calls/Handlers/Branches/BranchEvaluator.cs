using System;
using System.Collections.Generic;
using CodeBase.Features.Calls.External.LogicalExpression;

namespace CodeBase.Features.Calls.Handlers.Branches
{
    public class BranchEvaluator 
    {
        private readonly IVariables _variables;

        public BranchEvaluator(IVariables variables) => 
            _variables = variables;

        public string GetNextBranch(BranchesNode request) => 
            FindProcessingBranch(request.Branches).Guid;

        private Branch FindProcessingBranch(IEnumerable<Branch> branches)
        {
            foreach (var branch in branches)
            {
                if (ParseCondition(branch.Condition))
                    return branch;
            }

            throw new Exception("Invalid branches!");
        }

        private bool ParseCondition(string condition) => 
            LogicalBuilder
                .Expression(condition)
                .Evaluate(_variables);
    }
}