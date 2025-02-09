﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos.Enums
{
    public enum OrcamentoStatusEnum
    {
        Aberto,
        Fechado,
        Cancelado,
        NaoDefinido
    }

    public static class OrcamentoStatusEnumExtensions
    {
        public static string ToDataValue(this OrcamentoStatusEnum value)
        {            
            switch (value)
            {
                case OrcamentoStatusEnum.Aberto:
                    return "P";
                case OrcamentoStatusEnum.Fechado:
                    return "F";
                case OrcamentoStatusEnum.Cancelado:
                    return "C";
                default:
                    return null;
            }
        }
        public static OrcamentoStatusEnum ToOrcamentoStatusEnum(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return OrcamentoStatusEnum.Aberto;

            switch (value)
            {
                case "F":
                    return OrcamentoStatusEnum.Fechado;
                case "C":
                    return OrcamentoStatusEnum.Cancelado;
                case "P":
                    return OrcamentoStatusEnum.Aberto;
                default:
                    return OrcamentoStatusEnum.NaoDefinido;
            }
        }
    }
}
