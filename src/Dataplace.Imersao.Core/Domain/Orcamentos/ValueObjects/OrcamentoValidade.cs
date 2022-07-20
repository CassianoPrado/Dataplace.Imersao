using Dataplace.Imersao.Core.Domain.Excepions;
using System;

namespace Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects
{
    public class OrcamentoValidade
    {
  
        public OrcamentoValidade(Orcamento orcamento, int dias)
        {
            if(dias < 0)
                throw new DomainException(nameof(dias));

            Dias = dias;
            Data = orcamento.DtOrcamento.AddDays(dias).Date;

        }

        public DateTime Data { get;  }
        public int Dias { get;  }
    }
}
