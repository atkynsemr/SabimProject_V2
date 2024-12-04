using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabim.Domain.DTOs.BolumDtos
{
    public record ResultBolumDto : BolumBaseDto
    {
        public short BolumID { get; init; }
    }
}
