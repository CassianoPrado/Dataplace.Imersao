using Dataplace.Imersao.Core.Domain.Excepions;
using Dataplace.Imersao.Core.Domain.Orcamentos;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using Dataplace.Imersao.Core.Tests.Fixtures;
using System;
using Xunit;

namespace Dataplace.Imersao.Core.Tests.Domain.Orcamentos
{
    [Collection(nameof(OrcamentoCollection))]
    public class OrcamentoTest
    {
        private readonly OrcamentoFixture _fixture;
        private readonly OrcamentoItemFixture _fixtureItem;
        public OrcamentoTest(OrcamentoFixture fixture, OrcamentoItemFixture fixtureItem)
        {
            _fixture = fixture;
            _fixtureItem = fixtureItem;
        }

        [Fact]
        public void NovoOrcamentoDevePossuirValoresValidos()
        {
            // Arrange Act
            var orcamento = _fixture.NovoOrcamento();


            // Assert
            Assert.True(orcamento.CdEmpresa == _fixture.CdEmpresa);
            Assert.True(orcamento.CdFilial == _fixture.CdFilial);
            Assert.Equal(_fixture.NumOrcaemtp, orcamento.NumOrcamento);
            Assert.True(orcamento.Cliente.Codigo == _fixture.Cliente.Codigo);
            Assert.True(orcamento.Usuario == _fixture.UserName);
            Assert.True(orcamento.Usuario == _fixture.UserName);
            Assert.True(orcamento.Situacao == Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Aberto);
            Assert.Null(orcamento.Validade);
            Assert.NotNull(orcamento.TabelaPreco);
            Assert.Equal(_fixture.TavelaPreco.CdTabela, orcamento.TabelaPreco.CdTabela);
            Assert.Equal(_fixture.TavelaPreco.SqTabela, orcamento.TabelaPreco.SqTabela);
        }

        [Fact]
        public void FecharOrcamentoDeveRetornarStatusFechado()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();


            // Act
            orcamento.FecharOrcamento();


            // Assert
            Assert.Equal(Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Fechado, orcamento.Situacao);
            Assert.NotNull(orcamento.DtFechamento);
        }


        [Fact]
        public void TentarFecharOrcamentoJaFechadoRetornarException()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamento();
            orcamento.FecharOrcamento();

            // act & assert
            Assert.Throws<DomainException>(() => orcamento.FecharOrcamento());
        }

        [Fact]
        public void ReabrirOrcamentoDeveRetornarStatusAberto()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();


            // Act
            orcamento.FecharOrcamento();
            orcamento.ReabrirOrcamento();


            // Assert
            Assert.Equal(Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Aberto, orcamento.Situacao);
            
        }


        [Fact]
        public void TentarReabrirOrcamentoJaAbertoRetornarException()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamento();


            // act
            orcamento.FecharOrcamento();
            orcamento.ReabrirOrcamento();

            //asset
            Assert.Throws<DomainException>(() => orcamento.ReabrirOrcamento());
        }

        [Fact]
        public void CancelarOrcamentoDeveRetornarStatusCancelado()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();


            // Act
            orcamento.CancelarOrcamento();


            // Assert
            Assert.Equal(Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Cancelado, orcamento.Situacao);
            Assert.Null(orcamento.DtFechamento);
        }


        [Fact]
        public void TentarCancelarOrcamentoJaCanceladoRetornarException()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamento();
            orcamento.CancelarOrcamento();

            // act & assert
            Assert.Throws<DomainException>(() => orcamento.CancelarOrcamento());
        }

        [Fact]
        public void TentarCancelarOrcamentoFechadoRetornarException()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamento();
            orcamento.FecharOrcamento();

            // act & assert
            Assert.Throws<DomainException>(() => orcamento.CancelarOrcamento());
        }
        [Fact]
        public void DiasDeValidadeMenorQueZeroRetornarException()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamento();
            

            // act & assert
            Assert.Throws<DomainException>(() => orcamento.DefinirValidade(-10));
        }

        [Fact]
        public void OrcamentoEstaComDadosValido()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamento();
            orcamento.DefinirValidade(10);

            // act & assert
            Assert.True(orcamento.IsValid());
        }

        [Fact]
        public void OrcamentoEstaComDadosInvalidos()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamentoInvalido();
            
            // act & assert
            Assert.False(orcamento.IsValid());
        }

        [Fact]
        public void InsereItemValidoNoOrcamento()
        {
            //arrange
            var orcamento = _fixture.NovoOrcamento();
            var orcamentoItem = _fixtureItem.NovoOrcamentoItem();

            //act
            orcamento.InsereItem(orcamentoItem);

            //assert            
            Assert.True(orcamentoItem.CdEmpresa == _fixtureItem.CdEmpresa);
            Assert.True(orcamentoItem.CdFilial == _fixtureItem.CdFilial);
            Assert.Equal(orcamentoItem.NumOrcamento, _fixtureItem.NumOrcamento);
            Assert.True(orcamentoItem.Quantidade == _fixtureItem.Quantidade);
            Assert.Equal(orcamentoItem.Preco, _fixtureItem.Preco);
            Assert.Equal(orcamentoItem.Produto, _fixtureItem.Produto);
        }

        [Fact]
        public void OrcamentoItemEstaComDadosValidos()
        {
            // arrange
            var orcamentoItem = _fixtureItem.NovoOrcamentoItem();

            // act & assert
            Assert.True(orcamentoItem.IsValid());
        }

    }
}
