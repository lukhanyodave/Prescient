using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescient.File.Domain.Models.Base
{
    public abstract class BaseModel
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
    }
}
