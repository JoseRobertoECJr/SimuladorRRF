using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SimuladorRRF
{
    public class Utils
    {
        public enum BlockTipoEnum
        {
            [Description("white")]
            NonExec = 0,

            [Description("#4c4c4c")]
            AltaPrQueue = 1,

            [Description("blue")]
            Processo = 2,

            #region Tipos IO

            [Description("red")]
            Disco = 3,

            [Description("orange")]
            FitaMagnetica = 4,


            [Description("yellow")]
            Impressora = 5,

            #endregion

            [Description("#000000")]
            IOQueue = 6,

            [Description("#adadad")]
            BaixaPrQueue = 7
        }

        public static String GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
