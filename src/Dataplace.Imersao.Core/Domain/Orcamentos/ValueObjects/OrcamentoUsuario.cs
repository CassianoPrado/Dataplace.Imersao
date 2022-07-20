using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects
{
    public class OrcamentoUsuario
    {
        public OrcamentoUsuario(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; private set; }
    }
}
