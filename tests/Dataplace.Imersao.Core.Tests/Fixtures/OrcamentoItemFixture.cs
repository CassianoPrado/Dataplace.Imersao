using Dataplace.Imersao.Core.Domain.Orcamentos;
using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Tests.Fixtures
{
    public class OrcamentoItemFixture
    {
        internal string CdEmpresa = "IMS";
        internal string CdFilial = "01";
        internal int NumOrcamento = 1000;
        internal OrcamentoProduto Produto = new OrcamentoProduto(TpRegistroEnum.ProdutoFinal, "1000");
        internal int Quantidade = 10;
        internal OrcamentoItemPrecoTotal Preco = new OrcamentoItemPrecoTotal(15, 20);

        public OrcamentoItem NovoOrcamentoItem()
        {
            return new OrcamentoItem(CdEmpresa, CdFilial, NumOrcamento, Produto, Quantidade, Preco);
        }
    }
}
