using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos.Enums
{
    public enum OrcamentoItemStatusEnum
    {
        Aberto,
        Fechado,
        Cancelado,
        NaoDefinido
    }

    public static class OrcamentoItemStatusEnumExtensions
    {
        public static string ToDataValue(this OrcamentoItemStatusEnum value)
        {            
            switch (value)
            {
                case OrcamentoItemStatusEnum.Aberto:
                    return "P";
                case OrcamentoItemStatusEnum.Fechado:
                    return "F";
                case OrcamentoItemStatusEnum.Cancelado:
                    return "C";
                default:
                    return null;
            }
        }
        public static OrcamentoItemStatusEnum ToOrcamentoItemStatusEnum(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return OrcamentoItemStatusEnum.Aberto;

            switch (value)
            {
                case "F":
                    return OrcamentoItemStatusEnum.Fechado;
                case "C":
                    return OrcamentoItemStatusEnum.Cancelado;
                case "P":
                    return OrcamentoItemStatusEnum.Aberto;
                default:
                    return OrcamentoItemStatusEnum.NaoDefinido;
            }
        }
    }
}
