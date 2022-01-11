using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaCandidato.Data.CustomValidations
{
    public class DatetimeCompare : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            var date = Convert.ToDateTime(value);

            DateTime dataNascimento = new DateTime(date.Year, date.Month, date.Day);

            var datetime = DateTime.Now;

            if (dataNascimento >= DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
