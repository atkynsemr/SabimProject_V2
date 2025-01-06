using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabim.Domain.DTOs.GorevlendirilmeTuruDtos
{
    public record UpdateGorevlendirilmeTuruDto:GorevlendirilmeTuruBaseDto
    {
        public short GorevlendirilmeTuruID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
