using Dataplace.Imersao.Core.Domain.Excepions;
using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using System;
using System.Collections.Generic;

namespace Dataplace.Imersao.Core.Domain.Orcamentos
{
    public class Orcamento
    {
        private Orcamento(string cdEmpresa, string cdFilial, int numOrcamento, OrcamentoCliente cliente, 
            OrcamentoUsuario usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
        {

            CdEmpresa = cdEmpresa;
            CdFilial = cdFilial;
            Cliente = cliente;
            NumOrcamento = numOrcamento;
            Usuario = usuario;
            Vendedor = vendedor;
            TabelaPreco = tabelaPreco;

            // default
            Situacao = OrcamentoStatusEnum.Aberto;
            DtOrcamento = DateTime.Now;
            ValotTotal = 0;
            Itens = new List<OrcamentoItem>();

        }

        public string CdEmpresa { get; private set; }
        public string CdFilial { get; private set; }
        public int NumOrcamento { get; private set; }
        public OrcamentoCliente Cliente { get; private set; }
        public DateTime DtOrcamento { get; private set; }
        public decimal ValotTotal { get; private set; }
        public OrcamentoValidade Validade { get; private set; }
        public OrcamentoTabelaPreco TabelaPreco { get; private set; }
        public DateTime? DtFechamento { get; private set; }
        public OrcamentoVendedor Vendedor { get; private set; }
        public OrcamentoUsuario Usuario { get; private set; }
        public OrcamentoStatusEnum Situacao { get; private set; }
        public ICollection<OrcamentoItem> Itens { get; private set; }


        public void FecharOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Fechado)
                throw new DomainException("Orçamento já está fechado!");

            Situacao = OrcamentoStatusEnum.Fechado;
            DtFechamento = DateTime.Now.Date;
        }

        public void CancelarOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Cancelado)
                throw new DomainException("Orçamento já está cancelado!");

            if (Situacao == OrcamentoStatusEnum.Fechado)
                throw new DomainException("Orçamento já está fechado!");

            Situacao = OrcamentoStatusEnum.Cancelado;
        }

        public void ReabrirOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Aberto)
                throw new DomainException("Orçamento já está fechado!");

            Situacao = OrcamentoStatusEnum.Aberto;
            DtFechamento = null;
        }

        public void InsereItem(OrcamentoItem item)
        {
            if (!item.IsValid())
                throw new DomainException($"Foram encontradas inconsistências nos dados dos itens do orçamento, o mesmo não será inserido. " +
                    $"Mensagem: {string.Join(",", item.Validations.ToArray())}");
            Itens.Add(item);
        }

        public void DefinirValidade(int diasValidade)
        {
            this.Validade = new OrcamentoValidade(this, diasValidade);
        }

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

            if (Cliente.Codigo == "")
                Validations.Add("O cliente deve ser informado!");

            if (DtOrcamento < DateTime.Now.AddDays(-7))
                Validations.Add($"A data do orçamento informada não pode ser menor que {DateTime.Now.AddDays(-7)}");

            if (Validations.Count > 0)
                return false;
            else
                return true;
        }

        #endregion

        #region factory methods
        public static class Factory
        {

            public static Orcamento Orcamento(string cdEmpresa, string cdFilial, int numOrcamento, OrcamentoCliente cliente , OrcamentoUsuario usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, cliente, usuario, vendedor, tabelaPreco);
            }
            public static Orcamento OrcamentoRapido(string cdEmpresa, string cdFilial, int numOrcamento, OrcamentoUsuario usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, null, usuario, vendedor, tabelaPreco);
            }
        }

        #endregion
    }
}
