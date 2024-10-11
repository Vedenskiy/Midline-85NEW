using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Features.Calls.Infrastructure;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.EditMode.Features.Calls.Infrastructure
{
    public class PipelineTests
    {
        [Test]
        public async Task WhenExecuteNode_WithoutHandler_ThenShouldThrowException()
        {
            // assign
            var handlers = new Dictionary<Type, IRequestHandler>();
            var pipeline = new Pipeline(handlers);
            
            // act
            Func<Task> act = async () => await pipeline.Execute(new Node());

            // assert
            await act.Should().NotThrowAsync();
        }
    }
}