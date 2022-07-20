using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos
{
    public class OrcamentoItem
    {

        public OrcamentoItem(string cdEmpresa, string cdFilial, int numOrcamento, OrcamentoProduto produto, decimal quantidade, OrcamentoItemPrecoTotal preco)
        {
            CdEmpresa = cdEmpresa;
            CdFilial = cdFilial;
            NumOrcamento = numOrcamento;
            Produto = produto;
            Quantidade = quantidade;
            AtrubuirPreco(preco);

        }

        public int Seq { get; private set; }
        public string CdEmpresa { get; private set; }
        public string CdFilial { get; private set; }
        public int NumOrcamento { get; private set; }
        public OrcamentoProduto Produto { get; private set; }
        public decimal Quantidade { get; private set; }
        public decimal Total { get; private set; }
        public OrcamentoItemPrecoTotal Preco { get; private set; }
        public OrcamentoItemStatusEnum Situacao { get; private set; }



        #region setters
        private void AtrubuirPreco(OrcamentoItemPrecoTotal preco)
        {
            Preco = preco;
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            if (Quantidade < 0)
                throw new ArgumentOutOfRangeException(nameof(Quantidade));

            if (Preco.PrecoVenda < 0)
                new ArgumentOutOfRangeException(nameof(Preco.PrecoVenda));

            Total = Quantidade * Preco.PrecoVenda;
        }
        #endregion


        #region validations

        public List<string> Validations;
        public bool IsValid()
        {
            Validations = new List<string>();

            if (string.IsNullOrEmpty(CdEmpresa))
                Validations.Add("Código da empresa é requirido!");

            if (string.IsNullOrEmpty(CdFilial))
                Validations.Add("Código da filial é requirido!");

            if (NumOrcamento <= 0)
                Validations.Add("O número do orçamento informado deve ser maior que zero!");

            if (Produto.CdProduto == "")
                Validations.Add("O código do produto deve ser informado!");

            if (Validations.Count > 0)
                return false;
            else
                return true;
        }

        #endregion

    }


}
