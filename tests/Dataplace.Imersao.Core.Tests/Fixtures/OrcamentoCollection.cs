using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dataplace.Imersao.Core.Tests.Fixtures
{

    [CollectionDefinition(nameof(OrcamentoCollection))]
    public class OrcamentoCollection
        : ICollectionFixture<OrcamentoFixture>, ICollectionFixture<OrcamentoItemFixture>
    { }
}
